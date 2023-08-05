using UnityEngine;
using Pathfinding;


public class OdwrotDragonBoss : MonoBehaviour
{
    public AIPath aiPath;
    //  public Transform locsc;
    [HideInInspector] public float locscX;
    [HideInInspector] public float locscY;

    // Update is called once per frame
    private void Start()
    {
       var locsc = gameObject.transform;
         locscX = locsc.localScale.x;
         locscY = locsc.localScale.y;
       // var locscX = locsc.localScale.x;
    }


    void FixedUpdate()
    {

       
            if (aiPath.desiredVelocity.x >= 0.01f)
            {
                transform.localScale = new Vector3(locscX, locscY, 1f);

            }
            else
            {
                transform.localScale = new Vector3(-locscX, locscY, 1f);
            }
        
    }
}
