using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rectangle : MonoBehaviour
{
    public float height;
    public float width;

    public Vector2 rectPos;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        width = spriteRenderer.bounds.size.x;
        height = spriteRenderer.bounds.size.y;

        rectPos = this.gameObject.transform.position;
    }

}
