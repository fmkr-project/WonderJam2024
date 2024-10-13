using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Ships;
using UnityEngine;
using Random = UnityEngine.Random;

public class RebirthDialog : MonoBehaviour
{
   private IEnumerator Start()
    {
        Debug.Log(GameManager.CurrentRun);
        if (GameManager.CurrentRun == 2)
        {
            StartCoroutine(DialogueManager.OpenDialogueBox());
            // Before and after each operation, insert this line to wait until the player clicks on the screen
            while (DialogueManager._blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
            // Push a new dialogue, specifying who is speaking (enemy or player / narration)
            DialogueManager.PushDialogue("You died....... Sad Heh!......", DialogueParts.Player);
            while (DialogueManager._blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
            DialogueManager.PushDialogue("Don't worry, it was a clone all along and you've got plenty more.", DialogueParts.Player);
            while (DialogueManager._blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
            DialogueManager.PushDialogue("You will go back on your journey in a few. But for now, you can use your ether to permanently gain some bonus !", DialogueParts.Player);
            while (DialogueManager._blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
            StartCoroutine(DialogueManager.CloseDialogueBox());
        }
        }
       
    }
