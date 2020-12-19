using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactResolver : MonoBehaviour
{
    private static ContactResolver instance;

    public static ContactResolver Instance { get { return instance; } }

    List<Particle2DCollision> mContacts = new List<Particle2DCollision>();
    List<Particle2DLink> mDeadLinks = new List<Particle2DLink>();
    public List<Particle2DLink> mParticleLinks = new List<Particle2DLink>();

    int mIterations;
    int mIterationsUsed = 0;

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

    }
    private void Update()
    {
        foreach (Particle2DLink ParticleLink in mParticleLinks)
        {
            if (ParticleLink == null)
            {
                mDeadLinks.Add(ParticleLink);
            }
            else
            {
                ParticleLink.CreateContacts(mContacts);
            }
        }
        foreach (Particle2DLink linkToRemove in mDeadLinks)
        {
            if (linkToRemove != null)
            {
                mParticleLinks.Remove(linkToRemove);
            }
        }
        mDeadLinks.Clear();

        ResolveContacts(mContacts, 10);
        foreach (Particle2DCollision contact in mContacts)
        {
            Destroy(contact);
        }
        mContacts.Clear();
    }

    public void ResolveContacts(List<Particle2DCollision> contacts, int iterations)
    {
        mIterationsUsed = 0;
        while (mIterationsUsed < iterations)
        {
            float max = float.MaxValue;
            int numContacts = contacts.Count;
            int maxIndex = numContacts;
            for (int i = 0; i < numContacts; i++)
            {
                float sepVel = contacts[i].CalculateSeparatingVelocity();
                if (sepVel < max && (sepVel < 0.0f || contacts[i].mPenetration > 0.0f))
                {
                    max = sepVel;
                    maxIndex = i;
                }
            }
            if (maxIndex == numContacts)
            {
                break;
            }

            contacts[maxIndex].Resolve();

            for (int i = 0; i < numContacts; i++)
            {
                if (contacts[i].mObj1 == contacts[maxIndex].mObj1)
                {
                    contacts[i].mPenetration -= Vector2.Dot(contacts[maxIndex].mMove1, contacts[i].mContactNormal);
                }
                else if (contacts[i].mObj1 == contacts[maxIndex].mObj2)
                {
                    contacts[i].mPenetration -= Vector2.Dot(contacts[maxIndex].mMove2, contacts[i].mContactNormal);
                }

                if (contacts[i].mObj2)
                {
                    if (contacts[i].mObj2 == contacts[maxIndex].mObj1)
                    {
                        contacts[i].mPenetration += Vector2.Dot(contacts[maxIndex].mMove1, contacts[i].mContactNormal);
                    }
                    else if (contacts[i].mObj2 == contacts[maxIndex].mObj2)
                    {
                        contacts[i].mPenetration -= Vector2.Dot(contacts[maxIndex].mMove2, contacts[i].mContactNormal);
                    }
                }

            }
            mIterationsUsed++;
        }
    }
}
