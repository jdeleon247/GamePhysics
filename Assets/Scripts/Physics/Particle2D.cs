using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2D : MonoBehaviour
{
    public Vector2 Pos;
    public float Mass;
    public Vector2 Velocity;
    public Vector2 Acceleration;
    public Vector2 AccumulatedForces;
    public double DampingConstant;
    public bool shouldIgnoreForces;

    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer)
        {
            radius = spriteRenderer.bounds.extents.x;
        }
        else radius = 0.0f;
    }

    public void setVelocity(Vector2 newVelocity)
    {
        Velocity = newVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        Pos = this.gameObject.transform.position;
    }

    void OnBecameInvisible()
    {
        if(this.gameObject.tag != "Target")
            ParticleManager.Instance.DeleteParticle(this);
    }
}
