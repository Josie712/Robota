using System;
using UnityEngine;

[Serializable]
public class Question {

    [TextArea]
    public string text;
    public Answer[] answers;

}
