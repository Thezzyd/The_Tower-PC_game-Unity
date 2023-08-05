using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero3ProjectileComponent : MonoBehaviour
{
        
    private HeroesManager heroesManager;
    private LevelManager levelManager;
    private Rigidbody2D rb;
    public Transform crashPoint;

    void Start()
    {
        heroesManager = FindObjectOfType<HeroesManager>();
        levelManager = FindObjectOfType<LevelManager>();
        rb = GetComponent<Rigidbody2D>();
      //  Debug.Log(rb.velocity.magnitude);
        
    }

    void FixedUpdate(){
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //  if (collision.gameObject.tag != "Nietoperz1" || collision.gameObject.tag != "Nietoperz2" || collision.gameObject.tag != "Nietoperz3" || collision.gameObject.tag != "BLOOBENEMY" || collision.gameObject.tag != "ENEMY" || collision.gameObject.tag != "DRAGONBOSS")
        //  {   
                if(collision.gameObject.layer == 8){
                    var projectileCrashEffect = Instantiate(heroesManager.Hero3ProjectileCrashEffectPrefab, crashPoint.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));  
                    Destroy(projectileCrashEffect, 1f);
                }else{
                    var projectileCrashEffect = Instantiate(heroesManager.Hero3ProjectileCrashEffectPrefab, crashPoint.position, Quaternion.Euler(new Vector3(0f, 0f, crashPoint.rotation.z - 90)));  
                    Destroy(projectileCrashEffect, 1f);
                }
                
                Destroy(gameObject);

    }

    public void OnTriggerEnter2D(Collider2D other){
        if(!other.tag.Equals("medusaOrb") && other.gameObject.layer != 18){
            if(heroesManager){
                var projectileCrashEffect = Instantiate(heroesManager.Hero3ProjectileCrashEffectPrefab, crashPoint.position, Quaternion.Euler(new Vector3(0f, 0f, crashPoint.rotation.z - 90)));  
                Destroy(projectileCrashEffect, 1f);
                //Debug.Log("Uderzylo w : "+other.name);
            }else{
                heroesManager = FindObjectOfType<HeroesManager>();
                var projectileCrashEffect = Instantiate(heroesManager.Hero3ProjectileCrashEffectPrefab, crashPoint.position, Quaternion.Euler(new Vector3(0f, 0f, crashPoint.rotation.z - 90)));  
                Destroy(projectileCrashEffect, 1f);
                //Debug.Log("Uderzylo w : "+other.name);
            }
        }
    }

    public void KillObject(){
        Destroy(gameObject);
    }

}
