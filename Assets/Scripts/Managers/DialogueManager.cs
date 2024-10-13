using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public enum DialogueParts
    {
        Player,
        Enemy
    }
    
    public class DialogueManager : MonoBehaviour
    {
        private static TMP_FontAsset _playerFont;
        private static TMP_FontAsset _enemyFont;
        
        // Dialogue box
        private static GameObject _dialogueSurface;
        private static TextMeshProUGUI _dialogueText;
        private static GameObject _dialogueArrow;

        private static bool _canPrintDialogue = false;
        private static bool _isPrinting = false;
        private static bool _blockDialoguePrinting = false;
        private static int _i;
        private static string _dialogueTextContents;
        
        // Loot pop-up
        private static GameObject _lootSurface;
        private static TextMeshProUGUI _lootText;
        private static Image _lootImage;

        private static bool _blockLootClosing = false;
        
        // Time & animation
        private static float _dialogueOpenCloseTime = .75f;
        
        private static float _dialogueArrowTimeSinceLastBlink;
        private static float _dialogueArrowBlinkingPeriod = 0.5f;
        private static int _dialogueTicksSinceLastChar;
        private static int _dialogueTicksPerChar = 5;


        public static IEnumerator OpenDialogueBox()
        {
            _blockDialoguePrinting = true;
            var elapsed = 0f;
            var deltaTime = Time.deltaTime;
            while (elapsed < _dialogueOpenCloseTime)
            {
                elapsed += deltaTime;
                _dialogueSurface.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, elapsed / _dialogueOpenCloseTime);
                yield return new WaitForSeconds(deltaTime);
            }

            _blockDialoguePrinting = false;
        }

        public static IEnumerator OpenLootBox(Sprite bg, string description)
        {
            _blockLootClosing = true;
            _lootImage.sprite = bg;
            _lootText.text = description;
            
            var elapsed = 0f;
            var deltaTime = Time.deltaTime;
            while (elapsed < _dialogueOpenCloseTime)
            {
                elapsed += deltaTime;
                _lootSurface.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, elapsed / _dialogueOpenCloseTime);
                yield return new WaitForSeconds(deltaTime);
            }
        }

        public static IEnumerator CloseLootBox()
        {
            var elapsed = 0f;
            var deltaTime = Time.deltaTime;
            while (elapsed < _dialogueOpenCloseTime)
            {
                elapsed += deltaTime;
                _lootSurface.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, elapsed / _dialogueOpenCloseTime);
                yield return new WaitForSeconds(deltaTime);
            }
        }

        public static IEnumerator CloseDialogueBox()
        {
            _blockDialoguePrinting = true;
            var elapsed = 0f;
            var deltaTime = Time.deltaTime;
            while (elapsed < _dialogueOpenCloseTime)
            {
                elapsed += deltaTime;
                _dialogueSurface.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, elapsed / _dialogueOpenCloseTime);
                yield return new WaitForSeconds(deltaTime);
            }

            _blockDialoguePrinting = false;
        }
        
        public static void NewPage()
        {
            _blockDialoguePrinting = false;
            _blockLootClosing = false;
            
            _dialogueText.text = "";
            _i = 0;
            _dialogueTextContents = "";
            _canPrintDialogue = false;
            _isPrinting = false;
        }

        public static void PushDialogue(string dia, DialogueParts who)
        {
            _blockDialoguePrinting = true;
            
            _dialogueText.font = who == DialogueParts.Player ? _playerFont : _enemyFont; 
            _dialogueText.text = "";
            _dialogueTextContents = dia;
            _canPrintDialogue = true;
            _isPrinting = true;
        }

        private void OnMouseDown()
        {
            if (!_isPrinting)
            {
                NewPage();
            }
        }

        void Awake()
        {
            _playerFont = Resources.Load<TMP_FontAsset>("Fonts/KH-DOT-AKIHABARA-16 SDF");
            _enemyFont = Resources.Load<TMP_FontAsset>("Fonts/KH-DOT-DOUGENZAKA-16 SDF");
            
            // Initialize dialogue internals
            _dialogueSurface = GameObject.Find("DialogueBoxBg");
            _dialogueSurface.transform.localScale = Vector3.zero;
            _dialogueText = GameObject.Find("DialogueBoxText").GetComponent<TextMeshProUGUI>();
            _dialogueText.text = "";
            _dialogueArrow = GameObject.Find("DialogueBoxArrow");
            _dialogueArrow.SetActive(false);
            
            // Initialize loot internals
            _lootSurface = GameObject.Find("LootBoxBg");
            _lootSurface.transform.localScale = Vector3.zero;
            _lootText = GameObject.Find("LootBoxText").GetComponent<TextMeshProUGUI>();
            _lootImage = GameObject.Find("LootBoxImage").GetComponent<Image>();
        }

        IEnumerator Example()
        {
            // Example DialogueManager coroutine.
            // You can use FindObjectByType to find the DialogueManager in the scene if you use the prefab.
            // Open an empty dialogue box
            StartCoroutine(OpenDialogueBox());
            
            // Before and after each operation, insert this line to wait until the player clicks on the screen
            while (_blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
            // Push a new dialogue, specifying who is speaking (enemy or player / narration)
            PushDialogue("AZERTYUIOPQSDFGHJKLMWXCVBN", DialogueParts.Enemy);
            while (_blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
            PushDialogue("Never gonna give you up, never gonna let you down, lorem ipsum dolor sit amet, lorem ipsum dolor sit amet' or 1 == 1;", DialogueParts.Player);
            while (_blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
            
            // Close the dialogue box.
            // Add a WaitForSeconds if you intend to open a loot popup afterwards.
            StartCoroutine(CloseDialogueBox());
            yield return new WaitForSeconds(0.5f);

            // Open a loot popup.
            // Specify the context image that will appear (dat here).
            // An example of loot string format is provided (amount and resource type to string). Replace with proper values.
            // Wait until the player clicks to close the popup.
            var dat = Resources.Load<Sprite>("TestSprites/Ship1");
            StartCoroutine(OpenLootBox(dat, $"Looted {42} {ResourceManager.StringOfResource(Resource.Money)}."));
            while (_blockLootClosing) yield return new WaitForSeconds(Time.deltaTime);
            StartCoroutine(CloseLootBox());

            // DON'T REMOVE THIS!
            // or else you won't see the closing animation end properly
            yield return new WaitForSeconds(1);

        }

        void Update()
        {
            var deltaTime = Time.deltaTime;
            _dialogueTicksSinceLastChar++;
            
            // Next char printing
            if (!_canPrintDialogue) return;
            if (_isPrinting)
            {
                _dialogueArrow.SetActive(false);
                if (_dialogueText.text == _dialogueTextContents)
                    _isPrinting = false;
                else if (_dialogueTicksSinceLastChar >= _dialogueTicksPerChar)
                {
                    _dialogueTicksSinceLastChar = 0;
                    _i++;
                    _dialogueText.text = _dialogueTextContents.Substring(0, _i);
                }
            }
            else
            {
                // Arrow blinking
                _dialogueArrowTimeSinceLastBlink += deltaTime;
                if (_dialogueArrowTimeSinceLastBlink >= _dialogueArrowBlinkingPeriod)
                {
                    _dialogueArrow.SetActive(!_dialogueArrow.activeSelf);
                    _dialogueArrowTimeSinceLastBlink -= _dialogueArrowBlinkingPeriod;
                }
            }
        }
    }
}