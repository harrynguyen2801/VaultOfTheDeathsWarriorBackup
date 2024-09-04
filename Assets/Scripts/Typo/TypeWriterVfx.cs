using System.Collections;
using TMPro;
using UnityEngine;

namespace Typo
{
    [RequireComponent(typeof(TMP_Text))]
    public class TypeWriterVfx : MonoBehaviour
    {
        private TMP_Text _textBox;
        [SerializeField] private string textTest;

        private int _currentVisibleCharacterIndex;

        private Coroutine _typeWriterCoroutine;
        private WaitForSeconds _simpleDelay;
        private WaitForSeconds _interpunctuationDelay;

        [Header("Type Writer Settings")] 
    
        [SerializeField]
        private float characterPerSecond = 20f;
        [SerializeField] 
        private float interpunctuationDelay = 0.5f;

        private TMP_TextInfo textInfo;
        private void Awake()
        {
            _textBox = GetComponent<TextMeshProUGUI>();
            _simpleDelay = new WaitForSeconds(1 / characterPerSecond);
            _interpunctuationDelay = new WaitForSeconds(interpunctuationDelay);
        }

        public void SetText(string text)
        {
            _textBox.text = text;
            Debug.Log("text is: " + text + "| length:  " + text.Length);
            if (_typeWriterCoroutine != null)
                StopCoroutine(_typeWriterCoroutine);
            
            _textBox.maxVisibleCharacters = 0;
            _currentVisibleCharacterIndex = 0;
            textInfo = _textBox.textInfo;
            _typeWriterCoroutine = StartCoroutine(TextWriter());
        }

        IEnumerator TextWriter()
        {
            while (_currentVisibleCharacterIndex < textInfo.characterCount + 1)
            {
                if (_currentVisibleCharacterIndex > 1 && _currentVisibleCharacterIndex >= textInfo.characterCount)
                {
                    yield break;
                }
                char character = textInfo.characterInfo[_currentVisibleCharacterIndex].character;
                _textBox.maxVisibleCharacters++;

                if (character == '?' || character == '.' || character == ',' || character == ':' ||
                     character == ';' || character == '!' || character == '-')
                {
                    yield return _interpunctuationDelay;
                }
                else
                {
                    yield return _simpleDelay;
                }
                _currentVisibleCharacterIndex++;
            }
        }
    }
}
