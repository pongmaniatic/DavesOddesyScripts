using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CraftingRecipes")]
public class Recipes : ScriptableObject
{
    [Header("Name of recipie")]
    [Tooltip("The name of the recipe")]
    public string name;

    [Header("The objects that will be crafted together")]
    [Tooltip("The name of the objects that will combine")]
    public string itemA;
    public string itemB;

    public enum typeOfResult { None = 0, Event = 1, Item = 2 };
    public typeOfResult type;

    [Header("The result of the Crafting")]
    [Tooltip("The game object that results from the combination")]
    public GameObject result;

    [Header("What the robot says after crafting")]
    [TextArea]
    [Tooltip("text that appears after succesful or failed craft")]
    public string text;

}
