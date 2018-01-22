using System;
using UnityEngine;

[Serializable]
public class NewsArticle
{
    public string newsName;
    public string newsHeadline;
    [TextArea]
    public string newsText;
}