using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
	private static ParticleManager instance;

	public static ParticleManager Instance { get { return instance; } }

	public List<Particle2D> mParticles = new List<Particle2D>();
	public List<Rectangle> mRectangles = new List<Rectangle>();
	List<Particle2D> mParticlesToDelete = new List<Particle2D>();

	private void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			instance = this;
		}
	}

	private void Start()
	{
		Particle2D[] particles = (Particle2D[])GameObject.FindObjectsOfType(typeof(Particle2D));
		foreach (Particle2D particle in particles)
		{
			mParticles.Add(particle);
		}
		Rectangle[] rectangles = (Rectangle[])GameObject.FindObjectsOfType(typeof(Rectangle));
		foreach (Rectangle rectangle in rectangles)
        {
			mRectangles.Add(rectangle);
        }

	}

	void FixedUpdate()
	{
		foreach (Particle2D particle in mParticles)
		{
			foreach(Particle2D otherParticle in mParticles)
			{
				if (CollisionDetector.DetectCollision(particle, otherParticle))
				{
					if (particle != otherParticle && particle.tag == "Ball")
					{
						Vector2 particle1Norm = particle.Pos - otherParticle.Pos;

						particle1Norm.Normalize();
						Vector2 particle2Norm = -particle1Norm;

						particle.setVelocity(Vector2.Reflect(particle.Velocity, particle2Norm));
					}
					else if (particle != otherParticle)
                    {
						mParticlesToDelete.Add(particle);
						mParticlesToDelete.Add(otherParticle);
                    }
				}
			}

			foreach (Rectangle rectangle in mRectangles)
			{
				if (CollisionDetector.DetectCollision(particle, rectangle))
				{
					particle.Velocity = Vector2.Reflect(particle.Velocity, rectangle.rectPos.normalized);
				}

			}
		}
		foreach (Particle2D particle in mParticlesToDelete)
		{
			DeleteParticle(particle);
		}
		mParticlesToDelete.Clear();
	}

	public void DeleteParticle(Particle2D particle)
	{
		mParticles.Remove(particle);
		Destroy(particle.gameObject);
	}
}

/*for(int i = 0; i < mParticles.Count; i++)
        {
			for (int j = 0; j < mRectangles.Count; j++ )
            {
				if (mParticles[j] != mParticles[i])
                {
					if(CollisionDetector.DetectCollision(mParticles[i], mParticles[j]))
                    {
=						Vector2.Reflect(mParticles[i].Velocity, mParticles[j].Velocity.normalized);
                    }

					for(int k = 0; k < mRectangles.Count; k++)
                    {
						if (CollisionDetector.DetectCollision(mParticles[i], mRectangles[k]))
						{
							mParticles[i].Velocity = Vector2.Reflect(mParticles[i].Velocity, mRectangles[k].rectPos.normalized);
						}
					}
                }
            }
        }
		for(int i = 0; i < mParticlesToDelete.Count; i++)
        {
			DeleteParticle(mParticles[i]);
        }
		mParticlesToDelete.Clear();*/