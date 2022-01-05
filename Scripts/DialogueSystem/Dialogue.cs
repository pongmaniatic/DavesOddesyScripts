using UnityEngine;

[System.Serializable]
public struct Dialogue
{
    [Header("Name of Dialogue")]
    public string dialogueName;

    [Header("The order priority, 1 gratest and 5 is lowest priority")]
    public int priority;

    [Header("LifeSpawn of sentences")]
    public float[] sentenceTimer;

    [Header("The Dialogue, screen by screen")]
    [TextArea]
    public string[] sentences;

    [Header("The sprite shown in the Dialogue box")]
    public Sprite[] sprites;

    [Header("The sprite shown in the Dialogue box")]
    public AudioClip[] audioClips;
}
