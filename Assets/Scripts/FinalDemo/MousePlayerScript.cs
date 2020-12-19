using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePlayerScript : MonoBehaviour
{
    public Sprite ballSprite;
    public float ballScale = 1;
    public Color ballColor;
    public float ballMass;
    public Vector2 ballVelocity;
    public float mouseDragMultiplier = 1;
    public Vector2 ballAcceleration;
    public float ballDampingConstant;
    public bool ballShouldIgnoreForces;

    Vector2 originalMousePosition;
    Vector2 finalMousePosition;
    Vector2 currentMousePosition;

    private LineRenderer launchLine;

    // Start is called before the first frame update
    void Start()
    {
        launchLine = this.gameObject.GetComponent<LineRenderer>();
        launchLine.startWidth = 0.1f;
        launchLine.positionCount = 2;
        launchLine.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            originalMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 firstClickPos = originalMousePosition;
            launchLine.SetPosition(0, originalMousePosition);
            launchLine.enabled = true;
        }

        launchLine.SetPosition(1, currentMousePosition);

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            finalMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            ballVelocity = (finalMousePosition - originalMousePosition) * mouseDragMultiplier;
            CreateProjectile();
            launchLine.enabled = false;
        }
        if(Input.GetKey(KeyCode.Return))
        {
            foreach (Particle2D particle in ParticleManager.Instance.mParticles)
            {
                ParticleManager.Instance.mParticlesToDelete.Add(particle);
            }
        }
    }

    void CreateProjectile()
    {
        GameObject Projectile = new GameObject("Circle");
        Projectile.transform.position = originalMousePosition;
        Projectile.transform.rotation = transform.rotation;
        Projectile.transform.localScale = new Vector3(ballScale, ballScale, ballScale);
        Projectile.tag = "Projectile";
        SpriteRenderer ProjectileRenderer = Projectile.AddComponent<SpriteRenderer>();
        ProjectileRenderer.sprite = ballSprite;
        ProjectileRenderer.color = ballColor;
        Particle2D ProjectileData = Projectile.AddComponent<Particle2D>();
        ProjectileData.Mass = ballMass;
        ProjectileData.Velocity = -ballVelocity;
        ProjectileData.Acceleration = ballAcceleration;
        ProjectileData.DampingConstant = ballDampingConstant;
        ProjectileData.shouldIgnoreForces = ballShouldIgnoreForces;
        ParticleManager.Instance.mParticles.Add(ProjectileData);
    }
}
