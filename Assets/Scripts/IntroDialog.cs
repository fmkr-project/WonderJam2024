using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Ships;
using UnityEngine;
using Random = UnityEngine.Random;

public class IntroDialog : MonoBehaviour
{
   private IEnumerator Start()
    {
        StartCoroutine(DialogueManager.OpenDialogueBox());
        // Before and after each operation, insert this line to wait until the player clicks on the screen
        while (DialogueManager._blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
        // Push a new dialogue, specifying who is speaking (enemy or player / narration)
        DialogueManager.PushDialogue("Hurry Up Cap'tain, we need to leave! Use our weapon to attack their shield and let's flee!", DialogueParts.Player);
        while (DialogueManager._blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
        DialogueManager.PushDialogue("They can't escape! Their ship is ours!", DialogueParts.Enemy);
        while (DialogueManager._blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
        StartCoroutine(DialogueManager.CloseDialogueBox());
        }
       
    }
