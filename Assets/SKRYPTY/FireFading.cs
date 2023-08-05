
/*
public class FireFading : MonoBehaviour
{

    [HideInInspector] public float fadeTimer = 0f;
    [HideInInspector] public bool czyRosnie = true;
    [HideInInspector] public bool stop = false;

    public ParticleSystem[] particle;
    private ParticleSystem.MainModule ALPHASizeModule;
    private ParticleSystem.MainModule ADDizeModule;
    private ParticleSystem.MainModule GLOWSizeModule;
    private ParticleSystem.MainModule SPARCLESSizeModule;


    // Start is called before the first frame update
    void Start()
    {
        ALPHASizeModule = particle[0].main;
        ADDizeModule = particle[1].main;
        GLOWSizeModule = particle[2].main;
        SPARCLESSizeModule = particle[3].main;

        ALPHASizeModule.startSize = 0f;
        ADDizeModule.startSize = 0f;
        GLOWSizeModule.startSize = 0f;
        SPARCLESSizeModule.startSize = new ParticleSystem.MinMaxCurve(0f, 0f);

    }

    // Update is called once per frame
    void Update()
    {

      

        if (czyRosnie && stop == false)
        {
            fadeTimer += Time.deltaTime;
            if (fadeTimer <= 0.2f)
            {
                //  light.intensity = fadeTimer;
                //  light.pointLightOuterRadius = fadeTimer * 10f;

                ALPHASizeModule.startSize = 5f * (fadeTimer / 0.2f);
                ADDizeModule.startSize = 3f * (fadeTimer / 0.2f);
                GLOWSizeModule.startSize = 0.5f * (fadeTimer / 0.2f);
                SPARCLESSizeModule.startSize = new ParticleSystem.MinMaxCurve(0.4f * (fadeTimer / 0.2f), 1f * (fadeTimer / 0.2f));
            }

            if (fadeTimer >= 4f)
            {
                fadeTimer = 3f;
                czyRosnie = false;
            }

        }

        if (!czyRosnie && stop == false)
        {
            fadeTimer -= Time.deltaTime;
            //   light.intensity = fadeTimer;
            //   light.pointLightOuterRadius = fadeTimer * 10f;


            ALPHASizeModule.startSize = 5f * (fadeTimer / 3f);
            ADDizeModule.startSize = 3f * (fadeTimer / 3f);
            GLOWSizeModule.startSize = 0.5f * (fadeTimer / 3f);
            SPARCLESSizeModule.startSize = new ParticleSystem.MinMaxCurve(0.2f * (fadeTimer / 3f), 0.6f * (fadeTimer / 3f));


            if (fadeTimer <= 0f)
            {
                if(GetComponent<CapsuleCollider2D>())
                GetComponent<CapsuleCollider2D>().enabled = false;

               fadeTimer = 0f;
                stop = true;
            }

        }

       

    }
}
*/