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
        DialogueManager.PushDialogue("Hurry Up Cap'tain, we need to leave! Use our weapon to attack ", DialogueParts.Player);
            while (DialogueManager._blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
            StartCoroutine(DialogueManager.CloseDialogueBox());
        }
        else
        {
            GenerateLoot();
            
            foreach (var dico in Loot)
            {
                FindObjectOfType<PlayerShip>().AddResourceToInventory(dico.Key,dico.Value);
                switch (dico.Key)
                {
                    case Resource.Crew:
                        StartCoroutine(DialogueManager.OpenLootBox(spriteCrew,"Un nouveau membre !"));
                        break;
                    case Resource.Money:
                        StartCoroutine(DialogueManager.OpenLootBox(spriteMoney,dico.Value+" Pièces pour toi !" ));// TODO : changer les textes
                        break;
                    case Resource.Scrap:
                        StartCoroutine(DialogueManager.OpenLootBox(spriteScrap,dico.Value + " Pièces de ferailles !"));
                        break;
                }
                while (DialogueManager._blockLootClosing) yield return new WaitForSeconds(Time.deltaTime);
                StartCoroutine(DialogueManager.CloseLootBox());
                yield return new WaitForSeconds(1);
            }
        }

    }
}
