using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Hitbox : Collisionbox{
    public GameObject owner;

//    public bool useAttackDefaults;

//    public AttackType attackType;
//    public AttackGuard attackGuard;
//    public AttackAttribute attackAttribute;
//    public int attackLevel;

//    public float damage;
//    public float chipDamage;

//    public float blockstun;
//    public float hitstun;
//    public float untech;
//    public float hitstop;

//    public float meterGain;

//    public AttackAttribute type;
//    public bool knockdown;

//    public float xKnockback;
//    public float yKnockback; //Things with a positive yKnockback will put the enemy in a juggle state

//    public void Awake()
//    {
//        LoadDefaults();
//    }

//    private void LoadDefaults()
//    {
//        switch (attackType)
//        {
//            case AttackType.StandingNormal:
//                attackAttribute = AttackAttribute.Body;
//                break;
//            case AttackType.CrouchingNormal:
//                attackAttribute = AttackAttribute.Foot;
//                break;
//            case AttackType.AirNormal:
//                attackAttribute = AttackAttribute.Head;
//                break;
//            case AttackType.GroundSpecial:
//                attackAttribute = AttackAttribute.Body;
//                break;
//            case AttackType.AirSpecial:
//                attackAttribute = AttackAttribute.Head;
//                break;
//            case AttackType.Assist:
//                attackAttribute = AttackAttribute.Body;
//                break;
//        }
//    }

//    private void Start()
//    {
//        gameObject.layer = owner.layer; //Need to make this recursively change the layers of the children
//    }
}
