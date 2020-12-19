using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    GameObject[] pProjectiles;
    ScoreScript scoreScript;
    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        scoreScript = GameObject.Find("GameManager").GetComponent<ScoreScript>();
    }

    // Update is called once per frame
    void Update()
    {
        pProjectiles = GameObject.FindGameObjectsWithTag("Projectile");

        foreach (GameObject projectile in pProjectiles)
        {
            if (Vector2.Distance(projectile.transform.position, transform.position) < radius)
            {
                transform.position = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                scoreScript.score += 1;
            }
        }
    }
}
