using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Ships;
using UnityEngine;
using Random = UnityEngine.Random;

public class FantomDialog : MonoBehaviour
{
   private IEnumerator Start()
    {
        if (!GameManager.hasBeatenBoss)
        {
            GameManager.hasBeatenBoss = true;
            StartCoroutine(DialogueManager.OpenDialogueBox());
            // Before and after each operation, insert this line to wait until the player clicks on the screen
            while (DialogueManager._blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
            // Push a new dialogue, specifying who is speaking (enemy or player / narration)
            DialogueManager.PushDialogue("So.. you think you've won ? Do you truly think so ?", DialogueParts.Enemy);
            while (DialogueManager._blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
            DialogueManager.PushDialogue("Welcome to the other dimension.", DialogueParts.Player);
            while (DialogueManager._blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
            DialogueManager.PushDialogue("Now surpass your past selves !", DialogueParts.Enemy);
            while (DialogueManager._blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
            StartCoroutine(DialogueManager.CloseDialogueBox());
        }
    }
       
}
