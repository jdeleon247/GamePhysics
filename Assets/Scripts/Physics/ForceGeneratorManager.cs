using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ForceGeneratorManager : MonoBehaviour
{
	private static ForceGeneratorManager instance;

	public static ForceGeneratorManager Instance { get { return instance; } }

	List<ForceGenerator> mForceGenerators = new List<ForceGenerator>();
	List<ForceGenerator> mDeadGenerators = new List<ForceGenerator>();


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

	// Start is called before the first frame update
	void Start()
	{


		//CreatePointForceGenerator(new Vector2(0, 0), 1);
		CreateBuoyancyForceGenerator(25, 0, 0.05f);
	}

	// Update is called once per frame
	void Update()
	{

		foreach (ForceGenerator forceGenerator in mForceGenerators)
		{
			if (forceGenerator == null)
			{
				mDeadGenerators.Add(forceGenerator);
			}
			else
			{
				foreach (Particle2D particle in ParticleManager.Instance.mParticles)
				{
					if (particle.gameObject == null)
					{
						return;
					}
					forceGenerator.UpdateForce(particle.gameObject);
				}
			}
		}
		foreach (ForceGenerator forceGenerator in mDeadGenerators)
		{
			DeleteForceGenerator(forceGenerator);
		}
		mDeadGenerators.Clear();
	}

	void AddForceGenerator(ForceGenerator forceGenerator)
	{
		mForceGenerators.Add(forceGenerator);
	}
	public void DeleteForceGenerator(ForceGenerator forceGenerator)
	{
		mForceGenerators.Remove(forceGenerator);
	}

	public ForceGenerator CreatePointForceGenerator(Vector2 point, float magnitude)
	{
		GameObject forceGenerator = new GameObject("PointForceGenerator");
		PointForceGenerator newGenerator = forceGenerator.AddComponent<PointForceGenerator>();
		newGenerator.Initialize(point, magnitude);
		AddForceGenerator(newGenerator);
		return forceGenerator.GetComponent<ForceGenerator>();
	}

	public ForceGenerator CreateSpringForceGenerator(GameObject object1, GameObject object2, float springConstant, float restLength)
	{
		GameObject forceGenerator = new GameObject("SpringForceGenerator");
		SpringForceGenerator newGenerator = forceGenerator.AddComponent<SpringForceGenerator>();
		newGenerator.Initialize(object1, object2, springConstant, restLength);
		AddForceGenerator(newGenerator);
		return forceGenerator.GetComponent<ForceGenerator>();
	}

	public ForceGenerator CreateBuoyancyForceGenerator(float liquidDensity, float surfaceLevel, float maxDepth)
	{
		GameObject forceGenerator = new GameObject("BuoyancyForceGenerator");
		BuoyancyForceGenerator newGenerator = forceGenerator.AddComponent<BuoyancyForceGenerator>();
		newGenerator.Initialize(liquidDensity, surfaceLevel, maxDepth);
		AddForceGenerator(newGenerator);
		return forceGenerator.GetComponent<ForceGenerator>();
	}
}
