using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Hitbox3D : Collisionbox3D {
    public GameObject owner;
    public float damage;
}
