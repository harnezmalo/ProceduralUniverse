using UnityEngine;

public class Planet : MonoBehaviour
{
    Image Image;
    Dialogue Dialogue;
    Choix Choix;
    GameManager GameManager;
    Sprite Sprite;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnCollisionStay(Collision collision)
    {
        if (Input.GetButton("Fire3"))
        {
            Debug.Log("touché)");
        }
    }

    public void Initiate()
    {
        GameObject manager = GameObject.FindGameObjectWithTag("GameManager");
        GameManager = manager.GetComponent<GameManager>();
        GameManager.GetComponents(out Image image, out Dialogue dialogue, out Choix choix);
        Image = image;
        Dialogue = dialogue;
        Choix = choix;

        Sprite = GetComponent<SpriteRenderer>().sprite;
        Sprite = Image.Planète;
        
    }
}
