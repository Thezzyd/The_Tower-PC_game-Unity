using UnityEngine;

public class MoonParalax : MonoBehaviour
{
    private float length, height, startPositionX, startPositionY;
    public GameObject cam;
    public float paralaxEffectX;
    public float paralaxEffectY;

    void Start()
    {
        startPositionX = transform.localPosition.x;
        startPositionY = transform.localPosition.y;

       // length = GetComponent<SpriteRenderer>().bounds.size.x;
      //  height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void FixedUpdate()
    {
        float distanceX = (cam.transform.position.x * paralaxEffectX);
        float distanceY = (cam.transform.position.y * paralaxEffectY);

        transform.localPosition = new Vector3(startPositionX + distanceX, startPositionY + distanceY, 10);
    }
}
