using UnityEngine;

public class Planet : MonoBehaviour
{
    Image Image;
    Dialogue Dialogue;
    Choix Choix;
    GameManager GameManager;
    Sprite Sprite;
    bool destroyable = false;
    float time = 0;
    bool visible = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (destroyable == false)
        {
            time = time + Time.deltaTime;
            if (time > 3)
            {
                destroyable = true;
            }
        }

        if (visible == false && destroyable == true)
        {
            Destroy(gameObject);
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

        Sprite = GetComponent<SpriteRenderer>().sprite;
        Sprite = Image.Planète;
        
    }

    public void OnBecameInvisible()
    {
        visible = false;
    }

    public void OnBecameVisible()
    {
        visible = true;
    }
}
