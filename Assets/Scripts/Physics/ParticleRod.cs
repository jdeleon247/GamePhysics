using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class ParticleRod : Particle2DLink
{
    float mLength;

    public void Initialize(GameObject obj1, GameObject obj2, float length)
    {
        mObj1 = obj1;
        mObj2 = obj2;
        mLength = length;
    }
    public override void CreateContacts(List<Particle2DCollision> contacts)
    {
        if (mObj1 == null || mObj2 == null)
        {
            Destroy(gameObject);
            return;
        }
        float penetration = 0.0f;
        float length = Vector2.Distance(mObj1.transform.position, mObj2.transform.position);
        if (length == mLength)
        {
            return;
        }

        Vector2 normal = mObj2.transform.position - mObj1.transform.position;
        normal.Normalize();

        if (length > mLength)
        {
            penetration = length - mLength;
        }
        else
        {
            normal *= -1.0f;
            penetration = mLength - length;
        }

        penetration /= 100.0f;

        Particle2DCollision newParticleContact = gameObject.AddComponent<Particle2DCollision>();
        newParticleContact.Initialize(mObj1, mObj2, 0.0f, penetration, normal, Vector2.zero, Vector2.zero);
        contacts.Add(newParticleContact);
    }
}
