using System.Collections.Generic;
using UnityEngine;

public class DialogueWriteTemplate : MonoBehaviour {

    private List<string> _dialogueTextArray = new List<string>();
    private List<string> _dialogueButtonTextArray = new List<string>();
    private int _textCounter = 0;

    [SerializeField] private DialogueWriter _dialogueWriter;

    private void Update()
    {
        // Write
        if (Input.GetKeyDown(KeyCode.E) && _textCounter == 0 && !_dialogueWriter._writing)
        {
            // Adding text for dialogue
            _dialogueTextArray.Add("Hello");
            _dialogueTextArray.Add("How are you?");

            // Adding text for buttons
            _dialogueButtonTextArray.Add("I am fine");
            _dialogueButtonTextArray.Add("I am fked up");

            // Start writing text
            _dialogueWriter.WriterInit(_dialogueTextArray, _dialogueButtonTextArray, gameObject);
            ClearArrays();  // You have to clear after initialize writing
            _textCounter++;
        }

        // Answer
        if (GetComponent<DialogueAnswer>()._dialogueAnswer != -1 && _textCounter == 1)
        {
            switch (GetComponent<DialogueAnswer>()._dialogueAnswer)
            {
                case 0:
                    GetComponent<DialogueAnswer>()._dialogueAnswer = -1;
                    // Adding text for dialogue
                    _dialogueTextArray.Add("Its nice to meet you");
                    // Start writing text
                    _dialogueWriter.WriterInit(_dialogueTextArray, _dialogueButtonTextArray, gameObject);
                    ClearArrays();
                    _textCounter = 0;
                    break;
                case 1:
                    GetComponent<DialogueAnswer>()._dialogueAnswer = -1;
                    // Adding text for dialogue
                    _dialogueTextArray.Add("Fk you, bye...");
                    // Start writing text
                    _dialogueWriter.WriterInit(_dialogueTextArray, _dialogueButtonTextArray, gameObject);
                    ClearArrays();
                    _textCounter = 0;
                    break;
            }
        }
    }

    private void ClearArrays()
    {
        _dialogueTextArray.Clear();
        _dialogueButtonTextArray.Clear();
    }
}
