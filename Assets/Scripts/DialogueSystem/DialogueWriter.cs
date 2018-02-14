using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class DialogueWriter : MonoBehaviour {

    [HideInInspector] public GameObject _startedWritingObject;
    [HideInInspector] public bool _writing = false;

    private List<string> _dialogueTextArray = new List<string>();
    private List<string> _dialogueButtonTextArray = new List<string>();
    private bool _endLine = false;
    private bool _waitingAnswer = false;
    private int _index = -1;
    private int _charIndex = -1;
    private float _charTimer = 0;

    [SerializeField] private List<Button> _buttonArray = new List<Button>();
    [SerializeField] private Text _dialogueText;
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private float _nextCharTime;

    private void Start()
    {
        _nextCharTime = _nextCharTime * Time.deltaTime;
    }

    public void WriterInit(List<string> dialogueTextArray, List<string> dialogueButtonTextArray, GameObject startedWritingObject)
    {
        // Check if dialogue is already writing
        if (!_writing) {

            // Clear
            _dialogueTextArray.Clear();
            if (dialogueButtonTextArray.Count <= 0) { _dialogueButtonTextArray.Clear(); }

            // Get dialogue text
            for (int i = 0; i < dialogueTextArray.Count; i++) _dialogueTextArray.Add(dialogueTextArray[i]);

            // Get dialogue button text
            if (dialogueButtonTextArray.Count > 0)
            {
                for (int i = 0; i < dialogueButtonTextArray.Count; i++)
                {
                    _dialogueButtonTextArray.Add(dialogueButtonTextArray[i]);
                }
            }

            // Initialize some variables for writing
            _writing = true;
            _index = 0;
            _charIndex = 0;
            _charTimer = _nextCharTime;
            _dialogueText.text = "";
            _dialogueBox.SetActive(true);
            _waitingAnswer = false;
            _startedWritingObject = startedWritingObject;
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
                if (_dialogueButtonTextArray.Count > 0 && _index >= _dialogueTextArray.Count - 1 && !_waitingAnswer) ActivateButtons();
                else if (Input.GetKeyDown(KeyCode.E) && _index <= _dialogueTextArray.Count - 1 && !_waitingAnswer)
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
                        if (_dialogueButtonTextArray.Count <= 0) StopWriting();
                    }
                }
                /*else if (_waitingAnswer)
                {
                    for (int i = 0; i < _dialogueButtonTextArray.Count; i++)
                    {
                        Debug.Log(i);
                        _buttonArray[i].onClick.AddListener(delegate { ButtonClicked(i); });
                    }
                }*/
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
        _waitingAnswer = false;
        _startedWritingObject = null;
    }
    
    private void DeactiveButtons()
    {
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }
    }

    private void ActivateButtons()
    {
        for (int i = 0; i < _dialogueButtonTextArray.Count; i++)
        {
            _buttonArray[i].GetComponentInChildren<Text>().text = _dialogueButtonTextArray[i];
            _buttonArray[i].gameObject.SetActive(true);
        }

        _waitingAnswer = true;
    }

    public void ButtonClicked(int i)
    {
        if (_waitingAnswer)
        {
            _startedWritingObject.GetComponent<DialogueAnswer>().GetAnswer(i);
            StopWriting();
        }
    }
}
