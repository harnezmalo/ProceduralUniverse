using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Image")]

public class Image : ScriptableObject
{
    public Sprite Plan�te;
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Dialogue")]

public class Dialogue : ScriptableObject
{
    public UnityEngine.UI.Image Portrait;
    public List<string> Dialogues = new List<string>();
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Choix")]

public class Choix : ScriptableObject
{
    public List<string> Choix_R�ponse = new List<string>();
    public List<string> R�ponses_Alien = new List<string>();
}