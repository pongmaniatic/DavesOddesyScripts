using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public AudioSource audioSource;
    public float speedOfLetters = 0.025f;
    public TMP_FontAsset font;
    public static DialogueManager dialogueManager { get; private set; }
    public TextMeshProUGUI dialogueText;
    public Animator animator;
    public bool dialogueActive = false;
    public float timeLeft;
    private int numberOfSentence;
    public GameObject dialogueSprite;

    private Queue<string> sentences;
    private Queue<DialogueGenerator> allDialogues = new Queue<DialogueGenerator>();

    private List<DialogueGenerator> priority1 = new List<DialogueGenerator>();
    private List<DialogueGenerator> priority2 = new List<DialogueGenerator>();
    private List<DialogueGenerator> priority3 = new List<DialogueGenerator>();
    private List<DialogueGenerator> priority4 = new List<DialogueGenerator>();
    private List<DialogueGenerator> priority5 = new List<DialogueGenerator>();


    private void Awake(){ dialogueManager = this;}

    void Start()
    {
        sentences = new Queue<string>();
    }
    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        { }

            if (allDialogues.Count != 0 && !dialogueActive)
        {
            dialogueActive = true;
            StartDialogue(allDialogues.Dequeue());
        }
    }

    public void AddDialogueToQueue(DialogueGenerator dialogue)
    {
        allDialogues.Enqueue(dialogue);
        ReOrginizeQueue();
    }

    public void ReOrginizeQueue( )
    { 
        priority1.Clear();
        priority2.Clear();
        priority3.Clear();
        priority4.Clear();
        priority5.Clear();

        int allDialogueLenght = allDialogues.Count;
        for (int i = 0; i < allDialogueLenght; i++)
        {
            DialogueGenerator temporaryDialogue = allDialogues.Dequeue();
            if (temporaryDialogue != null)
            {
                switch (temporaryDialogue.priority)
                {
                    case 1:
                        priority1.Add(temporaryDialogue);
                        break;
                    case 2:
                        priority2.Add(temporaryDialogue);
                        break;
                    case 3:
                        priority3.Add(temporaryDialogue);
                        break;
                    case 4:
                        priority4.Add(temporaryDialogue);
                        break;
                    case 5:
                        priority5.Add(temporaryDialogue);
                        break;
                    default:
                        priority1.Add(temporaryDialogue);
                        break;
                }
            }

            
        }
        allDialogues.Clear();
        foreach (DialogueGenerator dialogue in priority1) { allDialogues.Enqueue(dialogue); }
        foreach (DialogueGenerator dialogue in priority2) { allDialogues.Enqueue(dialogue); }
        foreach (DialogueGenerator dialogue in priority3) { allDialogues.Enqueue(dialogue); }
        foreach (DialogueGenerator dialogue in priority4) { allDialogues.Enqueue(dialogue); }
        foreach (DialogueGenerator dialogue in priority5) { allDialogues.Enqueue(dialogue); }
    }


    public void StartDialogue(DialogueGenerator dialogue)
    {
        numberOfSentence = 0;
        animator.SetBool("IsOpen", true);
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence(dialogue);
    }

    public void DisplayNextSentence(DialogueGenerator dialogue)
    {
        
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        dialogueSprite.GetComponent<Image>().sprite = dialogue.sprites[numberOfSentence];
        audioSource.PlayOneShot(dialogue.audioClips[numberOfSentence]);
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        StartCoroutine(LifeSpawnOfSentence(dialogue.sentenceTimer[numberOfSentence], dialogue));
        numberOfSentence += 1;
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            yield return new WaitForSeconds(speedOfLetters);
            dialogueText.font = font;
            dialogueText.text += letter;
            yield return null;
        }
    }
    IEnumerator LifeSpawnOfSentence(float lifeSpawn, DialogueGenerator dialogue)
    {
        yield return new WaitForSeconds(lifeSpawn);
        DisplayNextSentence(dialogue);
        yield return null;


    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        dialogueActive = false;
    }
}
