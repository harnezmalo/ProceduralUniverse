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
    public float fadeText;
    public float AttenteDebut;
    public float texte1;
    public TMPro.TextMeshProUGUI text1;
    public float apparition1;
    public float texte2;
    public TMPro.TextMeshProUGUI text2;
    public float apparition2;
    public float DebutFade;
    public AudioClip SonBoucle;
    bool check = false;

    [HideInInspector]
    public GameObject planeteContact;

    [HideInInspector]
    public bool pause = true;

    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints");
        StartCoroutine(Debut());
        son = GetComponent<AudioSource>();
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        SpawnPlanets(spawnPoints);

        if (check)
        {
            if (!son.isPlaying)
            {
                son.clip = SonBoucle;
                son.Play();
            }
        }
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
        StartCoroutine(FadeInText(text1));
        yield return new WaitForSeconds(apparition1);
        StartCoroutine(FadeOutText(text1));

        yield return new WaitForSeconds(texte2);
        StartCoroutine(FadeInText(text2));
        yield return new WaitForSeconds(apparition2);
        StartCoroutine(FadeOutText(text2));

        yield return new WaitForSeconds(DebutFade);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeInText(TMPro.TextMeshProUGUI text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        Color color = new Color(text.color.r, text.color.g, text.color.b, 0);
        float elapsedTime = 0f;
        while (color.a < 1f)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime * fadeText);
            text.color = color;
            yield return null;
        }
    }

    IEnumerator FadeOutText(TMPro.TextMeshProUGUI text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        Color color = new Color(text.color.r, text.color.g, text.color.b, 1);
        float elapsedTime = 0f;
        while (color.a > 0f)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime * fadeText);
            text.color = color;
            yield return null;
        }
    }
}
