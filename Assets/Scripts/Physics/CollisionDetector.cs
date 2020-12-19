using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public static bool DetectCollision(Particle2D particle, Particle2D otherParticle)
    {
        if (Vector2.Distance(particle.transform.position, otherParticle.transform.position) < particle.radius + otherParticle.radius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool DetectCollision(Particle2D particle, Rectangle rectangle)
    {

        Vector2 particleDistance;
        particleDistance.x = Mathf.Abs(particle.Pos.x - rectangle.rectPos.x);
        particleDistance.y = Mathf.Abs(particle.Pos.y - rectangle.rectPos.y);

        if (particleDistance.x > (rectangle.width / 2 + particle.radius)) { return false; }
        if (particleDistance.y > (rectangle.height / 2 + particle.radius)) { return false; }

        if (particleDistance.x <= (rectangle.width / 2)) { return true; }
        if (particleDistance.y <= (rectangle.height / 2)) { return true; }

        float squaredCornerDistance = Mathf.Pow(particleDistance.x - rectangle.width/ 2, 2) + Mathf.Pow((particleDistance.y - rectangle.height / 2), 2);

        return (squaredCornerDistance <= (Mathf.Pow(particle.radius,2)));

    }

}
