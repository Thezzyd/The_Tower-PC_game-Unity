/*using UnityEngine;

public class DisolveP2 : MonoBehaviour
{
  /*  [HideInInspector] public Player2 player2;
    [HideInInspector] public Player3 player3;
    [HideInInspector] public Player player1;
    [HideInInspector] public Player4 player4;
    [HideInInspector] public Player5 player5;*/
/*
    public Material[] material;
    public SpriteRenderer[] material2;
    [HideInInspector] public LevelManager levelManager;
    [HideInInspector] public bool ifDead;
    [HideInInspector] public bool ifDisolving;

    [HideInInspector] float fade = 1f;

    private void Update()
    {
        /*if (ifDead && ifDisolving)
        {
            fade -= Time.deltaTime;

            if (fade <= 0f)
            {
                fade = 0f;
                ifDisolving = false;
            }

            for (int i = 0; i < material2.Length; i++)
            {
                material2[i].material.SetFloat("_Fade", fade);

            }
        }*/
        /*
        if (!ifDead && ifDisolving)
        {
            fade += Time.deltaTime;

            if (fade >= 1f)
            {
                fade = 1f;
                ifDisolving = false;
            }
            for (int i = 0; i < material2.Length; i++)
            {
                material2[i].material.SetFloat("_Fade", fade);

            }
        }
    }


    /*public void DisolvingDead()
    {
        material2 = GetComponentsInChildren<SpriteRenderer>();
        levelManager = FindObjectOfType<LevelManager>();
        fade = 1f;
        ifDead = true;
        ifDisolving = true;
       levelManager.isAlive = false;
      /* if (player3 != null) { player3.rb.velocity = new Vector2(0f, 5f); }
        if (player2 != null) {  player2.rb.velocity = new Vector2(0f, 5f); }
        if (player1 != null) {  player1.rb.velocity = new Vector2(0f, 5f); }
        if (player4 != null) {  player4.rb.velocity = new Vector2(0f, 5f); }*/
    //  }*/

    /*
    public void DisolvingRespawn()
    {
        material2 = GetComponentsInChildren<SpriteRenderer>();
        levelManager = FindObjectOfType<LevelManager>();
        Instantiate(levelManager.resurectionEffect, levelManager.actualPlayer.transform.position + new Vector3(0, 5f , 0), levelManager.resurectionEffect.transform.rotation);
        fade = 0f;
        ifDead = false;
        ifDisolving = true;

       // levelManager.actualPlayer.GetComponent<Animator>().SetBool("isResurecting", true);
       
        /* if (player3 != null) player3.isAlive = true;
         if (player1 != null) player1.isAlive = true;
         if (player4 != null) player4.isAlive = true;*/
   /* }
    /*
    public void InvokeDisolve()
    {
        levelManager = FindObjectOfType<LevelManager>();

        material2 = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < material2.Length; i++)
        {
            material2[i].material.SetFloat("_Fade", 0);

        }
     //   fade = 0f;
        Invoke("DisolvingRespawn", 1f);
        levelManager.isAlive = true;
    }

   

}
*/