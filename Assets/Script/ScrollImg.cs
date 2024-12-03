using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollImg : MonoBehaviour
{
    public float parralax;
    public GameObject quad;

    private void Start()
    {
        Renderer quadRenderer = quad.GetComponent<Renderer>();
        if (quadRenderer != null)
        {
            quadRenderer.sortingLayerName = "Above"; // Assigner la couche
            quadRenderer.sortingOrder = 10;              // Ordre élevé pour être au-dessus
        }
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
