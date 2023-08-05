
using UnityEngine;

public class RopeAnchorComponent : MonoBehaviour
{
    public float distanceFromRopeEnd = 0.0f; 

    public void ConnectRopeEnd(Rigidbody2D endRB)
    {
        HingeJoint2D hj = gameObject.AddComponent<HingeJoint2D>();

        hj.autoConfigureConnectedAnchor = false;

        hj.connectedBody = endRB;

        hj.anchor = Vector2.zero;

        hj.connectedAnchor = new Vector2(0.0f, -distanceFromRopeEnd);
    }


}
