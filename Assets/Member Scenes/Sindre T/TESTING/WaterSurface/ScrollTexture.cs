using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour
{
    public Material WaterSurfaceMaterial;
    public Vector2 direction = new Vector2(1, 0);
    public float Speed = 1.0f;

    private Vector2 currentOffset;

    // Start is called before the first frame update
    void Start()
    {
        currentOffset = WaterSurfaceMaterial.GetTextureOffset("_MainTex");
    }

    // Update is called once per frame
    void Update()
    {
        currentOffset += direction * Speed * Time.deltaTime;
        WaterSurfaceMaterial.SetTextureOffset("_MainTex", currentOffset);
    }
}
