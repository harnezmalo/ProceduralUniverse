using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollUV1 : MonoBehaviour
{
    public float parralax;

    void Start()
    {
        GetComponent<MeshRenderer>().sortingOrder = 3;
    }
    void Update()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        Material mat = mr.material;
        Vector2 offset = mat.mainTextureOffset;
        offset.x = transform.position.x / transform.localScale.x / parralax;
        offset.y = transform.position.y / transform.localScale.y / parralax;
        mat.mainTextureOffset = offset;

    }
}
