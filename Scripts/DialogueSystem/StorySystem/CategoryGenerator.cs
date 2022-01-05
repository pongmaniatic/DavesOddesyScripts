using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Categories")]
public class CategoryGenerator : ScriptableObject
{
    [Header("Name of Category")]
    public string nameOfQueue;

    [Header("If this Category is active or not")]
    public bool categoryActive = true;

    [Header("The queues on this Category")]
    public QueueGenerator[] queues;
    public void OnEnable()
    {
        categoryActive = true;
    }    
}
