using UnityEngine;

public class CanvaController : MonoBehaviour
{
    UnityEngine.UI.Image MainZone;

    void Start()
    {
        MainZone = GetComponentInChildren<UnityEngine.UI.Image>();
    }


    void Update()
    {
        
    }

    public void Activation()
    {
        //MainZone.GetComponent<RectTransform>().
    }
}
