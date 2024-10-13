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

        public static IEnumerator OpenLootBox()
        {
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
            _dialogueText = GameObject.Find("DialogueBoxText").GetComponent<TextMeshProUGUI>();
            _dialogueText.transform.localScale = Vector3.zero;
            _dialogueArrow = GameObject.Find("DialogueBoxArrow");
            _dialogueArrow.SetActive(false);
            
            // Initialize loot internals
            _lootSurface = GameObject.Find("LootBoxBg");
            _lootText = GameObject.Find("LootBoxText").GetComponent<TextMeshProUGUI>();
            _lootImage = GameObject.Find("LootBoxImage").GetComponent<Image>();
        }

        IEnumerator Start()
        {
            StartCoroutine(OpenDialogueBox());
            
            while (_blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
            PushDialogue("AZERTYUIOPQSDFGHJKLMWXCVBN", DialogueParts.Enemy);
            while (_blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
            PushDialogue("Never gonna give you up, never gonna let you down", DialogueParts.Player);
            while (_blockDialoguePrinting) yield return new WaitForSeconds(Time.deltaTime);
            
            StartCoroutine(CloseDialogueBox());
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