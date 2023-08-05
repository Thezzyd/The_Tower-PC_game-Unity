

/*
public class LightFading : MonoBehaviour
{
    [HideInInspector] public Light2D light;
    [HideInInspector] public float fadeTimer = 0f;
    [HideInInspector] public bool czyRosnie = true;
    [HideInInspector] public bool stop = false;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light2D>();
        light.pointLightOuterRadius = 0f;
        light.intensity = 0f;
    }

    // Update is called once per frame
    void Update()
    {
      
      

        if (czyRosnie && stop == false)
        {
            fadeTimer += Time.deltaTime;

            if (fadeTimer <= 1f)
            {
                light.intensity = fadeTimer;
                light.pointLightOuterRadius = 8f * (fadeTimer / 1f );
            }

            if (fadeTimer >= 4f)
            {
                fadeTimer = 4f;
                czyRosnie = false;
            }

        }

        if (!czyRosnie && stop == false)
        {
            fadeTimer -= Time.deltaTime;
            light.intensity = fadeTimer / 4f ;
            light.pointLightOuterRadius = 8f * (fadeTimer / 4f);

            if (fadeTimer <= 0f)
            {
                fadeTimer = 0f;
                stop = true;
            }

        }
    }
}
*/