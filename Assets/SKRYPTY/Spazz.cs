using UnityEngine;

public class Spazz : MonoBehaviour
{

    public LevelManager levelManager;
    public Animator anim;
    public bool isResurecting;
    public GameObject resurectionLight;
    public Transform LeftHand;
    public Transform RightHand;
    public Transform Heart;

 //   private float factor;

    public Material laserMaterial;
    //   public Material laserHandBeamMaterial;

    public ParticleSystem spazzHandEffect;

    private bool isLaserFadingIn;
    private bool isLaserFadingOut;
    private float timerLaser;

    private GameObject laser1;
    private GameObject laser2;
    private GameObject handBeam1;
    private GameObject handBeam2;

    private GameObject wingDustEffect;
    public GameObject starDonateButton;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        anim = GetComponent<Animator>();
        timerLaser = 0f;
      //  factor = Mathf.Pow(2f, 2.8f);
        

    }

private void FixedUpdate()
    {
        if (isLaserFadingIn)
        {
            timerLaser += Time.fixedDeltaTime;
            if (timerLaser >= 1f) 
                timerLaser = 1f;
            laserMaterial.SetColor("_Color", new Color(10f, 10f, 10f, timerLaser));
            //  laserHandBeamMainModule1.startSize = new ParticleSystem.MinMaxCurve(14f * timerLaser, 20 * timerLaser);
            // laserHandBeamMainModule2.startSize = new ParticleSystem.MinMaxCurve(14f * timerLaser, 20 * timerLaser);

            // laserHandBeamMaterial.SetColor("_Color", new Color(5.6f, 5.6f, 5.6f, timerLaser));

        }

        if (isLaserFadingOut)
        {
            timerLaser -= Time.fixedDeltaTime;
            if (timerLaser <= 0f) timerLaser = 0f;
            laserMaterial.SetColor("_Color", new Color(10f, 10f, 10f, timerLaser));
           // laserHandBeamMainModule1.startSize = new ParticleSystem.MinMaxCurve(14f * timerLaser, 20 * timerLaser);
           // laserHandBeamMainModule2.startSize = new ParticleSystem.MinMaxCurve(14f * timerLaser, 20 * timerLaser);

            // laserHandBeamMaterial.SetColor("_Color", new Color(5.6f, 5.6f, 5.6f, timerLaser));

        }

    }

    public void Resurect()
    {
        isResurecting = true;
       var effect = Instantiate(wingDustEffect, transform.position + new Vector3(0,4.6f,0), wingDustEffect.transform.rotation);
        Destroy(effect, 3f);
        anim.SetBool("isResurecting", isResurecting);
        Invoke("ResurectAnimDeactivation", 1);
        resurectionLight.GetComponent<Animator>().SetBool("isResurecting", true);

    }

    public void ResurectAnimDeactivation()
    {
        anim.SetBool("isResurecting",false);
        resurectionLight.GetComponent<Animator>().SetBool("isResurecting", false);
    }

   /* public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Spazz kolidujjjeeee. "+ collision.tag);
        if (collision.CompareTag("SpazzFinalPoint"))
        {
            // odpalamy particle system..

       //     levelManager.InvokedPortalDestructionEffect();
            Invoke("LaserOn", 1.5f);
            Destroy(collision.gameObject);
            handBeam1 = Instantiate(spazzHandEffect.gameObject, LeftHand.position, spazzHandEffect.transform.rotation);
            handBeam2 = Instantiate(spazzHandEffect.gameObject, RightHand.position, spazzHandEffect.transform.rotation);

            handBeam1.transform.SetParent(LeftHand.transform);
            handBeam2.transform.SetParent(RightHand.transform);

            levelManager.animEnemyPortal1.SetBool("grow", true);
            levelManager.animEnemyPortal2.SetBool("grow", true);


        }
    }*/

    public void LaserOn()
    {
       // laser1 = Instantiate(levelManager.laserLeft, LeftHand.position, levelManager.laserLeft.transform.rotation);
      //  laser2 = Instantiate(levelManager.laserRight, RightHand.position, levelManager.laserRight.transform.rotation);

        
        laser1.transform.SetParent(LeftHand.transform);
        laser2.transform.SetParent(RightHand.transform);

        /*laserHandBeam1 = laser1.GetComponentInChildren<ParticleSystem>();
        laserHandBeam2 = laser2.GetComponentInChildren<ParticleSystem>();

        laserHandBeamMainModule1 = laserHandBeam1.main;
        laserHandBeamMainModule2 = laserHandBeam2.main;*/

        isLaserFadingIn = true;
        isLaserFadingOut = false;

        Destroy(laser1, 2f);
        Destroy(laser2, 2f);
        Invoke("LaserFadeOut", 1f);
    }

    public void LaserFadeOut()
    {
        timerLaser = 1f;
        isLaserFadingOut = true;
        isLaserFadingIn = false;

        handBeam1.GetComponent<Animator>().SetBool("isFadingOut", true);
        handBeam2.GetComponent<Animator>().SetBool("isFadingOut", true);

        levelManager.canvasGameViewAnimator.SetTrigger("GameOver");

        Invoke("GameWonCanvasOn", 4f);

    }

  /*  public void GameWonCanvasOn()
    {
        levelManager.GameWonFinal();
    }*/
  
}
