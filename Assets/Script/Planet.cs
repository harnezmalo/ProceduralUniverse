using UnityEngine;

public class Planet : MonoBehaviour
{
    Image Image;
    Dialogue Dialogue;
    Choix Choix;
    GameManager GameManager;
    Sprite Sprite;
    bool destroyable = false;
    bool visible = false;
    bool firstVisible = false;
    float time = 0;

    void Start()
    {
        
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
            Debug.Log("detruit");
        }

        time = time + Time.deltaTime;

        if (time > 8)
        {
            destroyable = true;
        }
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
