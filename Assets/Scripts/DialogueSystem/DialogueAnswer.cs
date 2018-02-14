using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAnswer : MonoBehaviour {

    [HideInInspector] public int _dialogueAnswer = -1;

    public void GetAnswer(int i)
    {
        _dialogueAnswer = i;
    }
}
