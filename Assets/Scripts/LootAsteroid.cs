using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Ships;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootAsteroid : MonoBehaviour
{
    public Dictionary<Resource, int> Loot { get; } = new();
    public Sprite spriteCrew;
    public Sprite spriteMoney;
    public Sprite spriteScrap;
    protected void GenerateLoot()
    {
        // Generate money drop
        int moneyDrop = Random.Range(0,100);
        Loot.Add(Resource.Money, moneyDrop);
            
        // Generate scrap drop
        int scrapDrop = Random.Range(0, 100);
        Loot.Add(Resource.Scrap, scrapDrop);
        
            
        // Generate crew drop
        int crewDrop = Random.Range(0, 100);
        if (crewDrop < 40)
        {
            Loot.Add(Resource.Crew, 1);
        }
    }
    private IEnumerator Start()
    {
        int i = Random.Range(0, 100);
        if (i > 40)
        {
            int k = Random.Range(0, 30);
            FindObjectOfType<PlayerShip>().healthManager.TakeDamage(k);
            StartCoroutine(DialogueManager.OpenDialogueBox());
            // Before and after each operation, insert this line to wait until the player clicks on the screen
            while (DialogueManager._blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
            // Push a new dialogue, specifying who is speaking (enemy or player / narration)
            DialogueManager.PushDialogue("Dommages pour vous, vous avez rencontré un vaisseau de trop près ! Vous perdez "+k+" pvs", DialogueParts.Player);
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
