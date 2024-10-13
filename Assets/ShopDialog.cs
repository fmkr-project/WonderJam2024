using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Ships;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShopDialog : MonoBehaviour
{
    private bool done = false;
   private IEnumerator Start()
    {
        Debug.Log(GameManager.CurrentRun);
        if (GameManager.CurrentRun == 1)
        {
            if (!done)
            {
                done = true;
                StartCoroutine(DialogueManager.OpenDialogueBox());
                // Before and after each operation, insert this line to wait until the player clicks on the screen
                while (DialogueManager._blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
                // Push a new dialogue, specifying who is speaking (enemy or player / narration)
                DialogueManager.PushDialogue("Welcome in our small store. Here you can find how to upgrade your ship.", DialogueParts.Player);
                while (DialogueManager._blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
                DialogueManager.PushDialogue("On the upper left, you can find new weapons to add to your arsenal!", DialogueParts.Player);
                while (DialogueManager._blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
                DialogueManager.PushDialogue("In the right corner, you will find ways to improve your weapons, making them more efficient", DialogueParts.Player);
                while (DialogueManager._blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
                DialogueManager.PushDialogue("Beware, you won't obtain anything directly, but theses buffs could lead you to have powerful weapons", DialogueParts.Player);
                while (DialogueManager._blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
                DialogueManager.PushDialogue("In the middle, you can trade your coins to recruit some crewmates, or use  your scraps to repair your ship", DialogueParts.Player);
                while (DialogueManager._blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
                DialogueManager.PushDialogue("Finally, the last line is where you can sell your own weapons to get a 50% refund", DialogueParts.Player);
                while (DialogueManager._blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime); 
                StartCoroutine(DialogueManager.CloseDialogueBox());
            }
            
        }
    }
       
}
