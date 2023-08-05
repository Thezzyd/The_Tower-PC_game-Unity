using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionCanvas : MonoBehaviour
{
    [HideInInspector] public static SceneTransitionCanvas instance;
    private Animator anim;

    public float transitionTime = 1f;


   /* public void Awake() {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

       
    }*/

   /* public void OnEnable() {
        Debug.Log("On enablee");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        gameObject.GetComponentInChildren<Canvas>().worldCamera = FindObjectOfType<Camera>();
        anim = GetComponentInChildren<Animator>();
        Debug.Log("Wykonalooo sii");
        anim.Play("SceneTransitionFadeOutAnim", -1, 0f);
    }
    */
    void Start()
    {
        gameObject.GetComponentInChildren<Canvas>().worldCamera = FindObjectOfType<Camera>();
        anim = GetComponentInChildren<Animator>();
    
        Debug.Log("Wykonalooo sii");
    }
    public void LoadNextScene()
    {
      //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(LoadLevel("Wieza_01"));
        Debug.Log("Weszlo loadNextScene ");
    }

    public void LoadMainMenuScene()
    {
        //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(LoadLevel("Wieza_Menu"));
        Debug.Log("Weszlo loadMainMenu");
    }


    IEnumerator LoadLevel(string mapName)
    {
        Time.timeScale = 1.0f;
        anim.SetTrigger("StartTransition");

        yield return new WaitForSecondsRealtime(transitionTime);
        SceneManager.LoadScene(mapName);
        Debug.Log("Load level weszlo");

    }

}
