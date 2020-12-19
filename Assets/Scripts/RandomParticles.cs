using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomParticles : MonoBehaviour
{
    public float delay = 2f;
    public Sprite mSprite;
    public Color mColor;
    // Update is called once per frame
    private void Start()
    {
        InvokeRepeating("CreateParticles", delay, delay);
    }

    void CreateParticles()
    {
        GameObject randomParticle = new GameObject("spawned particle");
        randomParticle.transform.position = transform.position;
        randomParticle.transform.rotation = transform.rotation;
        randomParticle.transform.localScale = new Vector3(0.25f, 0.25f, 1);
        SpriteRenderer randomParticleRenderer = randomParticle.AddComponent<SpriteRenderer>();
        randomParticleRenderer.sprite = mSprite;
        randomParticleRenderer.color = mColor;
        Particle2D randomParticleData = randomParticle.AddComponent<Particle2D>();
        randomParticleData.Mass = 1;
        randomParticleData.Velocity = transform.up * 5;
        randomParticleData.Acceleration = new Vector2(0, -12);
        randomParticleData.DampingConstant = 0.7f;
        randomParticleData.shouldIgnoreForces = false;
        ParticleManager.Instance.mParticles.Add(randomParticleData);

        transform.position = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
