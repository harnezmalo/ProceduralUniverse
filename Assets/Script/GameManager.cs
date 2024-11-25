using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Image> images = new List<Image>();
    public List<Dialogue> dialogues = new List<Dialogue>();
    public List<Choix> choixI = new List<Choix>();
    public float FrequenceApparition;
    GameObject[] spawnPoints;
    public UnityEngine.Object planetePrefab;

    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints");
    }

    // Update is called once per frame
    void Update()
    {
        SpawnPlanets(spawnPoints);
    }

    public void GetComponents(out Image image, out Dialogue dialogue, out Choix choix)
    {
        int Rand1 = UnityEngine.Random.Range(0, images.Count);
        int Rand2 = UnityEngine.Random.Range(0, dialogues.Count);
        int Rand3 = UnityEngine.Random.Range(0, choixI.Count);

        image = images[Rand1];
        dialogue = dialogues[Rand2];
        choix = choixI[Rand3];

    }

    public void SpawnPlanets(GameObject[] spawnPoints)
    {
        foreach (GameObject go in spawnPoints)
        {
            float random = UnityEngine.Random.Range(0f, 100f);
            if (random <= FrequenceApparition)
            {
                UnityEngine.Object newPlanete = Instantiate(planetePrefab, go.transform.position, Quaternion.identity);
                Planet planetScript = newPlanete.GetComponent<Planet>();
                planetScript.Initiate();
                Debug.Log("créé");
            }

        }
    }

}
