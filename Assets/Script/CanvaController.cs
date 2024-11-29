using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CanvaController : MonoBehaviour
{
    public UnityEngine.UI.Image MainZone;
    float MZWidth;
    float MZHeight;
    float B1Width;
    float B1Height;
    float B2Width;
    float B2Height;
    public TMPro.TextMeshProUGUI MainZoneText;
    public float AttenteEcriture;
    GameManager manager;
    public UnityEngine.UI.Button bouton1;
    public UnityEngine.UI.Button bouton2;
    public TMPro.TextMeshProUGUI bouton1Text;
    public TMPro.TextMeshProUGUI bouton2Text;

    [HideInInspector]
    public bool CoroutineEcriture = false;

    void Start()
    {
        MZWidth = MainZone.GetComponent<RectTransform>().rect.width;
        MZHeight = MainZone.GetComponent<RectTransform>().rect.height;
        B1Height = bouton1.GetComponent<RectTransform>().rect.height;
        B1Width = bouton1.GetComponent<RectTransform>().rect.width;
        B2Height = bouton2.GetComponent<RectTransform>().rect.height;
        B2Width = bouton2.GetComponent<RectTransform>().rect.width;
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }


    void Update()
    {

        if (manager.pause == true && 
            MainZone.enabled == true && 
            manager.planeteContact.GetComponent<Planet>().Dialogue.Dialogues != MainZoneText.text && 
            Input.GetButton("Fire1") && 
            CoroutineEcriture == true)
        {
            StopAllCoroutines();
            MainZoneText.text = manager.planeteContact.GetComponent<Planet>().Dialogue.Dialogues;
            CoroutineEcriture = false;
            Debug.Log("Force");
        }

        if (!bouton1.isActiveAndEnabled && !bouton2.isActiveAndEnabled && 
            MainZoneText.text == manager.planeteContact.GetComponent<Planet>().Dialogue.Dialogues)
        {
            StartCoroutine(OpenButtons());
        }
    }

    public void Activation()
    {
        MainZone.gameObject.SetActive(true);
        MainZone.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1);
        MainZone.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1);
        MainZoneText.text = "";

        StartCoroutine(OpenMenu());
    }

    IEnumerator Ecriture(string text)
    {
        CoroutineEcriture = true;
        MainZoneText.text = "";

        for (int i=0; i<= (text.Length-1); i++)
        {
            MainZoneText.text += text[i];
            yield return new WaitForSeconds(AttenteEcriture);
        }

        CoroutineEcriture = false;
    }

    IEnumerator OpenMenu()
    {
        RectTransform rectTransform = MainZone.GetComponent<RectTransform>();
        float currentHeight = rectTransform.rect.height;
        float lerpSpeed = 0.019f;
        float lerpSpeed1 = 0.025f;

        while (Mathf.Abs(currentHeight - MZHeight) > 1f) 
        {
            currentHeight = Mathf.Lerp(currentHeight, MZHeight, lerpSpeed1);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentHeight);
            yield return null;
        }
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, MZHeight);

        float currentWidth = rectTransform.rect.width;

        while(Mathf.Abs(currentWidth - MZWidth) > 1f)
        {
            currentWidth = Mathf.Lerp(currentWidth, MZWidth, lerpSpeed);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentWidth);
            yield return null;
        }

        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, MZWidth);

        yield return new WaitForSeconds(0.25f);

        StartCoroutine(Ecriture(manager.planeteContact.GetComponent<Planet>().Dialogue.Dialogues));

        yield break;
    }

    IEnumerator OpenButtons()
    {
        bouton1.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 5);
        bouton1.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 5);
        bouton1Text.text = "";
        bouton2.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 5);
        bouton2.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 5);
        bouton2Text.text = "";

        bouton1.gameObject.SetActive(true);

        RectTransform rectTransform1 = bouton1.GetComponent<RectTransform>();
        float currentHeight = rectTransform1.rect.height;
        float lerpSpeed = 0.019f;
        float lerpSpeed1 = 0.025f;

        while (Mathf.Abs(currentHeight - B1Height) > 1f)
        {
            currentHeight = Mathf.Lerp(currentHeight, B1Height, lerpSpeed1);
            rectTransform1.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentHeight);
            yield return null;
        }
        rectTransform1.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, B1Height);

        float currentWidth = rectTransform1.rect.width;

        while (Mathf.Abs(currentWidth - B1Width) > 1f)
        {
            currentWidth = Mathf.Lerp(currentWidth, B1Width, lerpSpeed);
            rectTransform1.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentWidth);
            yield return null;
        }

        rectTransform1.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, B1Width);

        yield return new WaitForSeconds(0.25f);

        StartCoroutine(EcritureButtons(bouton1Text, manager.planeteContact.GetComponent<Planet>().Choix.Choix_Réponse[0]));

        yield return new WaitForSeconds(0.25f);

        bouton2.gameObject.SetActive(true);

        RectTransform rectTransform2 = bouton2.GetComponent<RectTransform>();
        float currentHeight2 = rectTransform2.rect.height;
        float lerpSpeed2 = 0.019f;
        float lerpSpeed3 = 0.025f;

        while (Mathf.Abs(currentHeight2 - B2Height) > 1f)
        {
            currentHeight2 = Mathf.Lerp(currentHeight2, B2Height, lerpSpeed2);
            rectTransform2.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentHeight2);
            yield return null;
        }
        rectTransform2.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, B2Height);

        float currentWidth2 = rectTransform2.rect.width;

        while (Mathf.Abs(currentWidth2 - B2Width) > 1f)
        {
            currentWidth2 = Mathf.Lerp(currentWidth2, B2Width, lerpSpeed3);
            rectTransform2.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentWidth2);
            yield return null;
        }

        rectTransform2.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, B2Width);

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(EcritureButtons(bouton2Text, manager.planeteContact.GetComponent<Planet>().Choix.Choix_Réponse[1]));

        yield return new WaitForSeconds(1.5f);

        EventSystem.current.SetSelectedGameObject(bouton1.gameObject);

    }

    IEnumerator EcritureButtons(TMPro.TextMeshProUGUI zoneText, string text)
    {
        CoroutineEcriture = true;
        zoneText.text = "";

        for (int i = 0; i <= (text.Length - 1); i++)
        {
            zoneText.text += text[i];
            yield return new WaitForSeconds(AttenteEcriture);
        }

        CoroutineEcriture = false;
    }

    public void Click(int i)
    {
        Debug.Log("appuyé");
        StartCoroutine(Ecriture(manager.planeteContact.GetComponent<Planet>().Choix.Réponses_Alien[i]));
    }
}
