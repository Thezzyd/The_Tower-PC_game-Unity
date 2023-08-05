
using UnityEngine;

public class LightingHERO4 : MonoBehaviour
{

    public AudioClip clip;


    public void OnParticleCollision(GameObject other)
    {

        if(clip != null)
        GetComponent<AudioSource>().PlayOneShot(clip);

    

    }


}
