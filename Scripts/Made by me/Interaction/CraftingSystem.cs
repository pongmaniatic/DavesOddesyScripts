using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    // This array holds all the recepies for combining and crafting
    [SerializeField]
    public List<Recipes> AllRecipes = new List<Recipes>();
    //all the recepies of the game
    public DialogueTrigger dialogueTrigger;

    private PickUpSystem pickUpSystem;

    private void Awake(){pickUpSystem = GetComponent<PickUpSystem>();}


    public bool Craft(string ItemA, string ItemB)
    {
        foreach (Recipes recipe in AllRecipes)
        {
            if (recipe.itemA == ItemA && recipe.itemB == ItemB)
            {
                dialogueTrigger.dialogue.sentences[0] = recipe.text;
                dialogueTrigger.TriggerDialogue();
                pickUpSystem.CreateNewItem(recipe.result.name); return true;
            }
            else if (recipe.itemA == ItemB && recipe.itemB == ItemA)
            {
                dialogueTrigger.dialogue.sentences[0] = recipe.text;
                dialogueTrigger.TriggerDialogue();
                pickUpSystem.CreateNewItem(recipe.result.name); return true;
            }  
        }
        return false;
    }

}

