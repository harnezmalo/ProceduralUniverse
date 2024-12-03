using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class GameManager : MonoBehaviour
{
    public List<Image> images = new List<Image>();
    public List<Dialogue> dialogues = new List<Dialogue>();
    public List<Choix> choixI = new List<Choix>();
    public float FrequenceApparition;
    GameObject[] spawnPoints;
    public UnityEngine.Object planetePrefab;
    public float DetectionRadius;
    string objectTag = "Planete";
    public UnityEngine.UI.Image noir;
    public float Fade;
    AudioSource son;
    public float AttenteDebut;
    public float texte1;
    public float texte2;
    public float texte3;
    public float DebutFade;

    [HideInInspector]
    public GameObject planeteContact;

    [HideInInspector]
    public bool pause = true;

    IEnumerator Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints");
        StartCoroutine(Debut());
        son = GetComponent<AudioSource>();
        yield return new WaitForSeconds(1f);


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
        bool found = false;

        foreach (GameObject go in spawnPoints)
        {
            float random = UnityEngine.Random.Range(0f, 100f);

            GameObject[] objectsToCheck = GameObject.FindGameObjectsWithTag(objectTag);
            found = false;

            foreach (GameObject obj in objectsToCheck)
            {
                float distance = Vector3.Distance(go.transform.position, obj.transform.position);

                if (distance <= DetectionRadius)
                {
                    found = true;
                    break;
                }
            }

            if (random <= FrequenceApparition && found == false && go.GetComponent<SpriteRenderer>().isVisible == false)
            {
                UnityEngine.Object newPlanete = Instantiate(planetePrefab, go.transform.position, Quaternion.identity);
                Planet planetScript = newPlanete.GetComponent<Planet>();
                planetScript.Initiate();
            }

        }
    }

    IEnumerator FadeOut()
    {
        Color color = noir.color;
        float elapsedTime = 0f;
        while (color.a > 0f)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime*Fade);
            noir.color = color;
            yield return null;
        }
        pause = false;
    }

    IEnumerator Debut()
    {
        yield return new WaitForSeconds(AttenteDebut);
        son.Play();
        yield return new WaitForSeconds(texte1);

        yield return new WaitForSeconds(texte2);

        yield return new WaitForSeconds(texte3);

        yield return new WaitForSeconds(DebutFade);
        StartCoroutine(FadeOut());
    }
}
