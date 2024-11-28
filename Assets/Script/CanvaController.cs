using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CanvaController : MonoBehaviour
{
    public UnityEngine.UI.Image MainZone;
    float MZWidth;
    float MZHeight;
    public TMPro.TextMeshProUGUI MainZoneText;
    public float AttenteEcriture;
    GameManager manager;

    void Start()
    {
        MZWidth = MainZone.GetComponent<RectTransform>().rect.width;
        MZHeight = MainZone.GetComponent<RectTransform>().rect.height;
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }


    void Update()
    {
        
    }

    public void Activation()
    {
        MainZone.gameObject.SetActive(true);
        MainZone.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 10);
        MainZone.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 10);
        MainZoneText.text = "";

        StartCoroutine(OpenMenu());
    }

    IEnumerator Ecriture(string text)
    {
        MainZoneText.text = "";

        for (int i=0; i<= text.Length; i++)
        {
            MainZoneText.text += text[i];
            yield return new WaitForSeconds(AttenteEcriture);
        }
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
            Debug.Log("Hauteur");
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

        yield return new WaitForSeconds(1);

        StartCoroutine(Ecriture(manager.planeteContact.GetComponent<Planet>().Dialogue.Dialogues));

        yield break;
    }
}
