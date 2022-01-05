using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Queues")]
public class QueueGenerator : ScriptableObject
{
    [Header("Name of Queue")]

    public string nameOfQueue;

    [Header("If this Category is active or not")]
    public bool queueActive = true;

    [Header("The Dialogues on this queue")]

    public DialogueGenerator[] dialogues;

    public List<DialogueGenerator> dialoguesHidden;

    public void OnEnable()
    {
        dialoguesHidden.Clear();
        queueActive = true;
        foreach (var item in dialogues)
        {
            dialoguesHidden.Add(item);
            //Debug.Log(item);
        }
    }
}
