using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Image")]

public class Image : ScriptableObject
{
    public Sprite Planète;
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
    public List<string> Choix_Réponse = new List<string>();
    public List<string> Réponses_Alien = new List<string>();
}