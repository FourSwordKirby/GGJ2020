using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollisionboxManager3D : MonoBehaviour {

    public List<Collisionbox3D> CollisionBoxes;
    private Dictionary<string, Collisionbox3D> AllCollisionBoxes;

  	void Awake()
    {
        AllCollisionBoxes = new Dictionary<string, Collisionbox3D>();
        //Initializes internal dictionary
        foreach(Collisionbox3D collisionbox in CollisionBoxes)
        {
           AllCollisionBoxes.Add(collisionbox.name, collisionbox);
        }
    }

    public void activateHitBox(string frameName)
    {
        AllCollisionBoxes[frameName].Activate();
    }

    public void deactivateHitBox(string frameName)
    {
        AllCollisionBoxes[frameName].Deactivate();
    }

    public void deactivateAllHitboxes()
    {
        foreach(Collisionbox3D col in AllCollisionBoxes.Values){
            col.Deactivate();
        }
    }

    public void activateAllHitboxes()
    {
        foreach (Collisionbox3D col in AllCollisionBoxes.Values)
        {
            col.Activate();
        }
    }

    public Collisionbox3D getHitbox(string frameName)
    {
        return AllCollisionBoxes[frameName];
    }
}
