using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Camera : MonoBehaviour
{
    public Transform SpaceShip;
    public float latence;
    Vector3 DistZ = new Vector3(0, 0, -2);
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = SpaceShip.position + DistZ;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, latence* Time.deltaTime);

        transform.position = smoothedPosition;
    }
}
