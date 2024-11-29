using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{
    [HideInInspector]
    public Image Image;

    [HideInInspector]
    public Dialogue Dialogue;

    [HideInInspector]
    public Choix Choix;


    GameManager GameManager;
    Sprite Sprite;
    bool destroyable = false;
    bool visible = false;
    bool firstVisible = false;
    float time = 0;
    Canvas Canvas;

    void Start()
    {
        Canvas = GameObject.Find("UI").GetComponent<Canvas>();
    }

    void Update()
    {
        if (destroyable == false)
        {
            if (firstVisible == true)
            {
                destroyable = true;
            }
        }

        if (visible == false && destroyable == true)
        {
            Destroy(gameObject);
        }

        time = time + Time.deltaTime;

        if (time > 8)
        {
            destroyable = true;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetButton("Fire3") && GameManager.GetComponent<GameManager>().pause == false)
        {
            GameManager.GetComponent<GameManager>().planeteContact = gameObject;
            GameManager.GetComponent<GameManager>().pause = true;
            Canvas.GetComponent<CanvaController>().Activation();

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

        GetComponent<SpriteRenderer>().sprite = Image.Planète;
        
    }

    public void OnBecameInvisible()
    {
        visible = false;
    }

    public void OnBecameVisible()
    {
        firstVisible = true;
        visible = true;
    }
}
