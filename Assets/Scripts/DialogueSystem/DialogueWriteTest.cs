using System.Collections.Generic;
using UnityEngine;

public class DialogueWriteTest : MonoBehaviour {

    private List<string> _dialogueTextArray = new List<string>();
    private List<string> _dialogueButtonTextArray = new List<string>();

    [SerializeField] private DialogueWriter _dialogueWriter;

    private void Start()
    {
        _dialogueTextArray.Add("Hello");
        _dialogueTextArray.Add("How are you?");
        _dialogueTextArray.Add("I am fine");

        _dialogueButtonTextArray.Add("I am fine");
        _dialogueButtonTextArray.Add("I am fked up");
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            _dialogueWriter.WriterInit(_dialogueTextArray, _dialogueButtonTextArray);
        }
    }
}
