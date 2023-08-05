
using UnityEngine;

public class TextMovingUp : MonoBehaviour
{
    public bool isPositive;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

        if (isPositive)
            anim.SetFloat("animSpeed", 0.5f);
        else
            anim.SetFloat("animSpeed", 1.3f);
    }

    void FixedUpdate()
    {
        if (Time.timeScale == 1f)
        {
            gameObject.transform.position += new Vector3(0f, 0.038f, 0f);

        }

      
    }

    public void DstroyLootTextInstance()
    {
        Destroy(gameObject);
    }

}
