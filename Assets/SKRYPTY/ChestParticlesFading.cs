
using UnityEngine;

public class ChestParticlesFading : MonoBehaviour
{
    public ParticleSystem particle;
    [HideInInspector] public float lifeTime;
    [HideInInspector] public ParticleSystem.MainModule fade;
    [HideInInspector] public float Timer;
    [HideInInspector] public bool isTimer = false;
    [HideInInspector] public bool czyAktywowane = false;
    [HideInInspector] public LevelManager levelManager;

    void Start()
    {
        Timer = 3f;
        particle = GetComponent<ParticleSystem>();
        lifeTime = particle.main.startLifetime.constant;
        levelManager = FindObjectOfType<LevelManager>(); 
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CoinCollector" && czyAktywowane == false )
        {
            fade = particle.main;
            isTimer = true;
            czyAktywowane = true;
        }

    }



    void Update()
    {
        if (isTimer == true)
        {
            Timer -= Time.deltaTime;

            fade.startLifetime = lifeTime * (Timer / 3f);



            if (Timer <= 0f)
            {
                isTimer = false;
                Timer = 3f;
            }
        }
    }
}
