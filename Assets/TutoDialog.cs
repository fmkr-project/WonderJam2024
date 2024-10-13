using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Ships;
using UnityEngine;
using Random = UnityEngine.Random;

public class TutoDialog : MonoBehaviour
{
   private IEnumerator Start()
    {
        Debug.Log(GameManager.CurrentRun);
        if (GameManager.CurrentRun == 1)
        {
            StartCoroutine(DialogueManager.OpenDialogueBox());
            // Before and after each operation, insert this line to wait until the player clicks on the screen
            while (DialogueManager._blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
            // Push a new dialogue, specifying who is speaking (enemy or player / narration)
            DialogueManager.PushDialogue("You're on your way to discover the galaxy. Be aware, lots of pirates will try to attack you during your journey.", DialogueParts.Player);
            while (DialogueManager._blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
            DialogueManager.PushDialogue("May I suggest you go in the shop and buy some weapons to protect you?", DialogueParts.Player);
            while (DialogueManager._blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
            DialogueManager.PushDialogue("You will find it not far from here. Use your money to have defenses and weapons", DialogueParts.Player);
            while (DialogueManager._blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
            StartCoroutine(DialogueManager.CloseDialogueBox());
        }
        }
       
    }
