using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Dialogue")]

public class Dialogue : ScriptableObject
{
    public UnityEngine.UI.Image Portrait;
    public string Dialogues;
}