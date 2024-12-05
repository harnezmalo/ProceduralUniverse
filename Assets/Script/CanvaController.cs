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
    bool phase2 = false;
    int reponse;

    public float Open1;
    public float Open2;
    public float close1;
    public float close2;
    bool down = false;
    Coroutine ecritureCoroutine;

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
            CoroutineEcriture == true &&
            !bouton1.isActiveAndEnabled && 
            !bouton2.isActiveAndEnabled &&
            phase2 == false)
        {
            StopAllCoroutines();
            MainZoneText.text = manager.planeteContact.GetComponent<Planet>().Dialogue.Dialogues;
            CoroutineEcriture = false;
        }

        if (!bouton1.isActiveAndEnabled && !bouton2.isActiveAndEnabled &&
            MainZoneText.text == manager.planeteContact.GetComponent<Planet>().Dialogue.Dialogues &&
            phase2 == false)
        {
            StartCoroutine(OpenButtons());
            phase2 = true;
        }

        if (manager.pause == true &&
            MainZone.enabled == true &&
            manager.planeteContact.GetComponent<Planet>().Choix.Réponses_Alien[0] != MainZoneText.text &&
            manager.planeteContact.GetComponent<Planet>().Choix.Réponses_Alien[1] != MainZoneText.text &&
            manager.planeteContact.GetComponent<Planet>().Dialogue.Dialogues != MainZoneText.text &&
            Input.GetButton("Fire1") &&
            CoroutineEcriture == true &&
            phase2 == true &&
            down == false)
        {
            StopCoroutine(ecritureCoroutine);
            MainZoneText.text = manager.planeteContact.GetComponent<Planet>().Choix.Réponses_Alien[reponse];
            Debug.Log("fin ecriture 2");
            CoroutineEcriture = false;
            down = true;
            StartCoroutine(Wait());
        }

        if (phase2 == true &&
            manager.pause == true &&
            MainZone.enabled == true &&
            (manager.planeteContact.GetComponent<Planet>().Choix.Réponses_Alien[0] == MainZoneText.text ||
            manager.planeteContact.GetComponent<Planet>().Choix.Réponses_Alien[1] == MainZoneText.text) &&
            Input.GetButton("Fire1") &&
            !bouton1.isActiveAndEnabled && 
            !bouton2.isActiveAndEnabled &&
            down == false)
        {
            Debug.Log("c bon");
            phase2 = false;
            StartCoroutine(CloseMenu());
            StartCoroutine(Decrire(MainZoneText));

        }
    }

    public void Activation()
    {
        MainZone.gameObject.SetActive(true);
        MainZone.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 8);
        MainZone.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 8);
        MainZoneText.text = "";

        StartCoroutine(OpenMenu());
    }

    IEnumerator Ecriture(string text)
    {
        
        CoroutineEcriture = true;
        MainZoneText.text = "";

        for (int i=0; i< text.Length; i++)
        {
            MainZoneText.text = MainZoneText.text + text[i];
            yield return new WaitForSeconds(AttenteEcriture);
        }

        CoroutineEcriture = false;

    }

    IEnumerator OpenMenu()
    {
        RectTransform rectTransform = MainZone.GetComponent<RectTransform>();
        float currentHeight = rectTransform.rect.height;
        float lerpSpeed = Open1;
        float lerpSpeed1 = Open2;

        while (Mathf.Abs(currentHeight - MZHeight) > 2f) 
        {
            currentHeight = Mathf.Lerp(currentHeight, MZHeight, lerpSpeed1*Time.deltaTime);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentHeight);
            yield return null;
        }
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, MZHeight);

        float currentWidth = rectTransform.rect.width;

        while(Mathf.Abs(currentWidth - MZWidth) > 1f)
        {
            currentWidth = Mathf.Lerp(currentWidth, MZWidth, lerpSpeed * Time.deltaTime);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentWidth);
            yield return null;
        }

        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, MZWidth);

        yield return new WaitForSeconds(0.25f);

        StartCoroutine(Ecriture(manager.planeteContact.GetComponent<Planet>().Dialogue.Dialogues));

        yield break;
    }

    IEnumerator CloseMenu()
    {
        RectTransform rectTransform = MainZone.GetComponent<RectTransform>();
        float currentHeight = rectTransform.rect.height;
        float lerpSpeed = close1;
        float lerpSpeed1 = close2;

        while (Mathf.Abs(currentHeight - 5) > 1f)
        {
            currentHeight = Mathf.Lerp(currentHeight, 5, lerpSpeed1 * Time.deltaTime);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentHeight);
            yield return null;
        }
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 5);

        float currentWidth = rectTransform.rect.width;

        while (Mathf.Abs(currentWidth) > 1f)
        {
            currentWidth = Mathf.Lerp(currentWidth, 0, lerpSpeed * Time.deltaTime);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentWidth);
            yield return null;
        }

        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);

        MainZone.gameObject.SetActive(false);
        manager.pause = false;
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
        float lerpSpeed = Open1;
        float lerpSpeed1 = Open2;

        while (Mathf.Abs(currentHeight - B1Height) > 1f)
        {
            currentHeight = Mathf.Lerp(currentHeight, B1Height, lerpSpeed1 * Time.deltaTime);
            rectTransform1.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentHeight);
            yield return null;
        }
        rectTransform1.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, B1Height);

        float currentWidth = rectTransform1.rect.width;

        while (Mathf.Abs(currentWidth - B1Width) > 1f)
        {
            currentWidth = Mathf.Lerp(currentWidth, B1Width, lerpSpeed * Time.deltaTime);
            rectTransform1.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentWidth);
            yield return null;
        }

        rectTransform1.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, B1Width);

        yield return new WaitForSeconds(0.15f);

        StartCoroutine(EcritureButtons(bouton1Text, manager.planeteContact.GetComponent<Planet>().Choix.Choix_Réponse[0]));

        yield return new WaitForSeconds(0.15f);

        bouton2.gameObject.SetActive(true);

        RectTransform rectTransform2 = bouton2.GetComponent<RectTransform>();
        float currentHeight2 = rectTransform2.rect.height;
        float lerpSpeed2 = Open1;
        float lerpSpeed3 = Open2;

        while (Mathf.Abs(currentHeight2 - B2Height) > 1f)
        {
            currentHeight2 = Mathf.Lerp(currentHeight2, B2Height, lerpSpeed2 * Time.deltaTime);
            rectTransform2.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentHeight2);
            yield return null;
        }
        rectTransform2.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, B2Height);

        float currentWidth2 = rectTransform2.rect.width;

        while (Mathf.Abs(currentWidth2 - B2Width) > 1f)
        {
            currentWidth2 = Mathf.Lerp(currentWidth2, B2Width, lerpSpeed3 * Time.deltaTime);
            rectTransform2.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentWidth2);
            yield return null;
        }

        rectTransform2.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, B2Width);

        yield return new WaitForSeconds(0.15f);

        StartCoroutine(EcritureButtons(bouton2Text, manager.planeteContact.GetComponent<Planet>().Choix.Choix_Réponse[1]));

        yield return new WaitForSeconds(0.25f);

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
        down = true;
        ecritureCoroutine = StartCoroutine(Ecriture(manager.planeteContact.GetComponent<Planet>().Choix.Réponses_Alien[i]));
        EventSystem.current.SetSelectedGameObject(null);
        StartCoroutine(CloseButtons());
        StartCoroutine(Decrire(bouton1Text));
        StartCoroutine(Decrire(bouton2Text));
        reponse = i;
        StartCoroutine(Wait());
    }

    IEnumerator CloseButtons()
    {

        RectTransform rectTransform1 = bouton1.GetComponent<RectTransform>();
        float currentHeight = rectTransform1.rect.height;
        float lerpSpeed = close1;
        float lerpSpeed1 = close2;

        while (Mathf.Abs(currentHeight - 5) > 1f)
        {
            currentHeight = Mathf.Lerp(currentHeight, 5, lerpSpeed1 * Time.deltaTime);
            rectTransform1.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentHeight);
            yield return null;
        }
        rectTransform1.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 5);

        float currentWidth = rectTransform1.rect.width;

        while (Mathf.Abs(currentWidth - 5) > 1f)
        {
            currentWidth = Mathf.Lerp(currentWidth, 5, lerpSpeed * Time.deltaTime);
            rectTransform1.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentWidth);
            yield return null;
        }

        rectTransform1.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);

        yield return new WaitForSeconds(0.15f);

        bouton1.gameObject.SetActive(false);

        RectTransform rectTransform2 = bouton2.GetComponent<RectTransform>();
        float currentHeight2 = rectTransform2.rect.height;
        float lerpSpeed2 = close1;
        float lerpSpeed3 = close2;

        while (Mathf.Abs(currentHeight2 - 5) > 1f)
        {
            currentHeight2 = Mathf.Lerp(currentHeight2, 5, lerpSpeed2 * Time.deltaTime);
            rectTransform2.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentHeight2);
            yield return null;
        }
        rectTransform2.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 5);

        float currentWidth2 = rectTransform2.rect.width;

        while (Mathf.Abs(currentWidth2 - 5) > 1f)
        {
            currentWidth2 = Mathf.Lerp(currentWidth2, 5, lerpSpeed3 * Time.deltaTime);
            rectTransform2.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentWidth2);
            yield return null;
        }

        rectTransform2.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);

        bouton2.gameObject.SetActive(false);

    }

    IEnumerator Decrire(TMPro.TextMeshProUGUI zoneText)
    {
        for (int i = 0; i < zoneText.text.Length; i++)
        {
            zoneText.text = zoneText.text.Remove(0);
            yield return new WaitForSeconds(AttenteEcriture/3);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        down = false;
    }

}
