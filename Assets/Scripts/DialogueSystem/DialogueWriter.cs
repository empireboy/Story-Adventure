using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class DialogueWriter : MonoBehaviour {

    private List<string> _dialogueTextArray = new List<string>();
    private List<string> _dialogueButtonTextArray = new List<string>();
    private bool _writing = false;
    private bool _endLine = false;
    private int _index = -1;
    private int _charIndex = -1;
    private float _charTimer = 0;

    [SerializeField] private List<GameObject> _buttonArray = new List<GameObject>();
    [SerializeField] private Text _dialogueText;
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private float _nextCharTime;

    private void Start()
    {
        _nextCharTime = _nextCharTime * Time.deltaTime;
    }

    public void WriterInit(List<string> dialogueTextArray, List<string> dialogueButtonTextArray)
    {
        // Check if dialogue is already writing
        if (!_writing) {

            // Clear
            _dialogueTextArray.Clear();
            _dialogueButtonTextArray.Clear();

            // Get dialogue text
            for (int i = 0; i < dialogueTextArray.Count; i++) _dialogueTextArray.Add(dialogueTextArray[i]);

            // Get dialogue button text
            if (dialogueButtonTextArray != null)
            {
                for (int i = 0; i < dialogueButtonTextArray.Count; i++) _dialogueButtonTextArray.Add(dialogueButtonTextArray[i]);

                // Activate buttons
                for (int i = 0; i < dialogueButtonTextArray.Count; i++)
                {
                    _buttonArray[i].GetComponentInChildren<Text>().text = _dialogueButtonTextArray[i];
                    _buttonArray[i].SetActive(true);
                }
            }

            // Initialize some variables for writing
            _writing = true;
            _index = 0;
            _charIndex = 0;
            _charTimer = _nextCharTime;
            _dialogueText.text = "";
            _dialogueBox.SetActive(true);
        }
        else
        {
            // Already writing

        }
    }

    private void Update()
    {
        if (_writing)
        {
            if (_charTimer <= 0 && !_endLine)
            {
                string text = _dialogueTextArray[_index];

                char currentChar = text[_charIndex];
                _dialogueText.text = _dialogueText.text + currentChar;

                if (_charIndex < text.Length-1)
                {
                    _charIndex++;
                    _charTimer = _nextCharTime;
                }
                else
                {
                    _endLine = true;

                    // Stop writing
                    if (_index > _dialogueTextArray.Count - 1)
                    {
                        StopWriting();
                    }
                }
            }
            else if (!_endLine)
            {
                _charTimer--;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Z) && _index <= _dialogueTextArray.Count - 1)
                {
                    if (_index < _dialogueTextArray.Count - 1)
                    {
                        _index++;
                        _charIndex = 0;
                        _charTimer = _nextCharTime;
                        _dialogueText.text = "";
                        _endLine = false;
                    }
                    else
                    {
                        // Stop writing
                        StopWriting();
                    }
                }
            }
        }
    }

    private void StopWriting()
    {
        _writing = false;
        _endLine = false;
        _dialogueText.text = "";
        _dialogueBox.SetActive(false);
        DeactiveButtons();
    }
    
    private void DeactiveButtons()
    {
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }
    }
}
