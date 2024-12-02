using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipControler : MonoBehaviour
{
    public float thrustForce = 5f; // Force d'acc�l�ration
    public float rotationSpeed = 200f; // Vitesse de rotation
    public float maxSpeed = 10f; // Vitesse maximale du vaisseau
    GameManager manager;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (manager.pause == false)
        {
            // R�cup�rer les entr�es du clavier
            bool moveInput = Input.GetButton("Fire1");
            float rotateInput = -Input.GetAxis("Horizontal"); // A / D ou Q / D (invers� pour rotation)

            // Appliquer une rotation (progressive)
            rb.AddTorque(rotateInput * rotationSpeed * Time.deltaTime);

            // Appliquer une pouss�e vers l'avant
            if (moveInput)
            {
                Vector2 force = transform.up * thrustForce;
                rb.AddForce(force);
            }

            ClampSpeed();
        }
    }

    private void ClampSpeed()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity *= 0.99f;
        }
    }
}

