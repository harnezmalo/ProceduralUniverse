using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Choix")]

public class Choix : ScriptableObject
{
    public List<string> Choix_R�ponse = new List<string>();
    public List<string> R�ponses_Alien = new List<string>();
}