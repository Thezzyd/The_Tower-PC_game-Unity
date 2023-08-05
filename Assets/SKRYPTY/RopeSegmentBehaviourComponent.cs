using System;
using UnityEngine;


public class RopeSegmentBehaviourComponent : MonoBehaviour
{

    private GameObject connectedAbove;
    private GameObject connectedBelow;
    private float localScaleY;

    public bool isEnemyWebStringType;

    void Start()
    {
        GetComponent<Rigidbody2D>().centerOfMass = new Vector3(0,0,0);
        GetComponent<Rigidbody2D>().inertia = 1;
        RopeSegmentBehaviourComponent aboveSegment = null;
        localScaleY = transform.localScale.y;

        try
        {
            connectedAbove = GetComponent<HingeJoint2D>().connectedBody.gameObject;
            aboveSegment = connectedAbove.GetComponent<RopeSegmentBehaviourComponent>();
        }
        catch(Exception e)
        { 
            // do nothing
        }

        if (aboveSegment != null)
        {
            aboveSegment.connectedBelow = gameObject;
            float spriteBottom = connectedAbove.GetComponent<SpriteRenderer>().bounds.size.y / localScaleY;
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, spriteBottom * -1);
        }
        else
        {
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, 0);
        }

    }

    public void WebSplit()
    {
        GetComponent<HingeJoint2D>().enabled = false;
        gameObject.layer = 0;
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        if (isEnemyWebStringType)
        {
            try
            {
                transform.parent.gameObject.GetComponent<RopeBehaviourComponent>().enemyOnThisWebString.GetComponent<EnemyHangingSpider>().isFalling = true;
                isEnemyWebStringType = false;
            }
            catch (MissingReferenceException e)
            {
                // do nothing 
                //touching web after spider is killed generates it
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 8 && collision.gameObject.layer != 18)
        {
            WebSplit();
        }
    }

    private void OnParticleCollision(GameObject collision)
    {
        if (collision.CompareTag("AttackHero4") || collision.CompareTag("AttackHero5"))
        {
            WebSplit();
        }
    }

}
