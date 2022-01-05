using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    // Array of categories.
    public CategoryGenerator[] categories;
    // Amount of seconds between dialogue activation.
    public float paceTimer = 10f;
    // Bool if the Zone has any Dialogue left or not.
    public bool ZoneEmpty = false;
    public float timeLeft;
    // Bool if player is inside Zone.
    public bool insideZone = false;

    // Set Up the timer.
    private void Awake(){ResetTimer();}

    private void Update()
    {
        // Continues the countdown as long as player is in Zone.
        if (insideZone)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0){ ActivateCategory(categories); ResetTimer();}
        }
    }
    // Resets timer.
    void ResetTimer(){timeLeft = paceTimer;// Debug.Log("timer reset"); 
    }

    // Randomly selects an available category.
    void ActivateCategory(CategoryGenerator[] categories)
    {
        // Temporary list with all available categories.
        List<CategoryGenerator> temporaryCategories = new List<CategoryGenerator>();
        foreach (var category in categories)
        {
            if (category.categoryActive){temporaryCategories.Add(category);}
        }
        // if all Categories are unavailable then the Zone is set as empty aswell.
        if (temporaryCategories.Count != 0)
        {
            int randomNumb = Random.Range(0, temporaryCategories.Count -1);
            //Debug.Log(temporaryCategories[randomNumb]);
            ActivateQueue(temporaryCategories[randomNumb]);
        }
        else {ZoneEmpty = true; //Debug.Log("zone empty");
        }
    }

    // Randomly selects an available Queue.
    void ActivateQueue(CategoryGenerator category)
    {
        // Temporary list with all available queues.
        List<QueueGenerator> temporaryQueues = new List<QueueGenerator>();
        foreach (var queue in category.queues)
        {
            if (queue.queueActive) { temporaryQueues.Add(queue); }
        }
        // if all queues are unavailable then this category becomes unavailable and another category is searched.
        if (temporaryQueues.Count != 0)
        {
            int randomNumb = Random.Range(0, temporaryQueues.Count-1);
            //Debug.Log(temporaryQueues[randomNumb]);
            ActivateDialogue(temporaryQueues[randomNumb]);
        }
        else
        {
            //Debug.Log("no queues"); 
            category.categoryActive = false;
            ActivateCategory(categories);
        }
    }
    //Checks if the queue is empty, if it is, it makes it unavailable and restarts de search, if not, it sends the dialogue to the queue in dialogue manager. 
    void ActivateDialogue(QueueGenerator Queue)
    {
        if (Queue.dialoguesHidden.Count != 0)
        {
            DialogueGenerator dialogueNewest = Queue.dialoguesHidden[0];
            Queue.dialoguesHidden.Remove(Queue.dialoguesHidden[0]);
            //Debug.Log("-------------------------------------Dialogue reached "+ dialogueNewest);
            DialogueManager.dialogueManager.AddDialogueToQueue(dialogueNewest);
        }
        else
        {
            //Debug.Log("no dialogues left");
            Queue.queueActive = false;
            ActivateCategory(categories);
        }
    }

    // this 2 check if the player is on the zone or not.
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");
        Debug.Log(other);
        if (other.gameObject.tag == "Player"){insideZone = true;}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player"){insideZone = false;}
    }
}
