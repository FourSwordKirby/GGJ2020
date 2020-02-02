using UnityEngine;
using System.Collections;

public abstract class Collisionbox3D : MonoBehaviour {

    public string boxName;
    private Collider[] colliders;

    void Awake()
    {
        colliders = this.GetComponents<Collider>();
    }

    public void Activate()
    {
        foreach(Collider col in colliders)
        {
            col.enabled = true;
        }
    }

    public void Deactivate()
    {
        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }
    }
}
