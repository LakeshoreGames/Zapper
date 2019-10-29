using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

    public static Manager instance = null;

    private int curlevel;
    public float timer;
    public float deathtimer;
    public bool paused;
    public AudioSource levelcomplete;
    public Texture2D targ;
    public GameObject levelselect;

	// Use this for initialization
	void Start () {
        if(!PlayerPrefs.HasKey("levelscomplete"))
        {
            PlayerPrefs.SetInt("levelscomplete", 0);
        }
        curlevel = 0;

        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Cursor.SetCursor(targ, new Vector2(targ.width*0.5f,targ.height*0.5f), CursorMode.Auto);
        /*
        Application.ExternalEval(
          @"if(typeof(kongregateUnitySupport) != 'undefined'){
            kongregateUnitySupport.initAPI('Manager', 'OnKongregateAPILoaded');
          };"
        );
        */
    }
	
	void Update () {
        if(deathtimer>0 )
        {
            deathtimer -= Time.deltaTime;
            if(deathtimer <=0)
            {
                deathtimer = 0;
                reload();
            }
        }
        if(Input.GetKeyDown("r"))
        {
            reload();
        }
        if (Input.GetKeyDown("escape") && !paused)
        {
            paused = true;
            Time.timeScale = 0;
        }
        else if(Input.GetKeyDown("escape") && paused)
        {
            paused = false;
            Time.timeScale = 1;
        }     
	}

    public void finished()
    {
        if (PlayerPrefs.GetInt("levelscomplete") < curlevel)
        {
            PlayerPrefs.SetInt("levelscomplete", curlevel);
            //Application.ExternalCall("kongregate.stats.submit", "LevelsComplete", curlevel);
        }
        nextlevel();
        levelcomplete.Play();
    }

    public void play(int lvl)
    {
        curlevel = lvl;
        SceneManager.LoadScene(curlevel);
    }

    public void nextlevel()
    {
        curlevel++;
        if(curlevel == 21)
        {
            curlevel = 0;
        }
        SceneManager.LoadScene(curlevel);
    }

    public void reload()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(curlevel);
    }

    public void OnKongregateAPILoaded(string userInfoString)
    {
        OnKongregateUserInfo(userInfoString);
    }

    public void OnKongregateUserInfo(string userInfoString)
    {
        var info = userInfoString.Split('|');
        var userId = System.Convert.ToInt32(info[0]);
        var username = info[1];
        var gameAuthToken = info[2];
        Debug.Log("Kongregate User Info: " + username + ", userId: " + userId);
    }

}
