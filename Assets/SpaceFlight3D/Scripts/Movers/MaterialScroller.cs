using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialScroller : MonoBehaviour
{
    public Material material;
    public string textureName;
    public Vector2 speed;

    Vector2 offset;
    private void Update()
    {
        offset += speed * Time.unscaledDeltaTime;
        material.SetTextureOffset(textureName, offset);
    }
}
