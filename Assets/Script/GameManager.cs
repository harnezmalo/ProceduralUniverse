using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Image> images = new List<Image>();
    public List<Dialogue> dialogues = new List<Dialogue>();
    public List<Choix> choixI = new List<Choix>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetComponents(out Image image, out Dialogue dialogue, out Choix choix)
    {
        int Rand1 = Random.Range(0, images.Count);
        int Rand2 = Random.Range(0, dialogues.Count);
        int Rand3 = Random.Range(0, choixI.Count);

        image = images[Rand1];
        dialogue = dialogues[Rand2];
        choix = choixI[Rand3];

    }

}
