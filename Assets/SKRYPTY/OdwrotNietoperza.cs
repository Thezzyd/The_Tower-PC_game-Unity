using UnityEngine;
using Pathfinding;

public class OdwrotNietoperza : MonoBehaviour
{

    public AIPath aiPath;
    [HideInInspector] private float locscX;
    [HideInInspector] private float locscY;

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
            transform.localScale = new Vector3(-locscX, locscY, 1f);

        }
        else 
        {
            transform.localScale = new Vector3(locscX, locscY, 1f);
        }

    }
}
