using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

    public static Manager instance = null;

    private int curlevel;
    public bool inmenu;
    public float timer;
    public float deathtimer;
    public AudioSource levelcomplete;
    public Texture2D targ;
    public Image logo;

    public GameObject splash;
    public GameObject pause;


	// Use this for initialization
	void Start () {
        if(!PlayerPrefs.HasKey("levelscomplete"))
        {
            PlayerPrefs.SetInt("levelscomplete", 0);
        }
        curlevel = 0;
        timer = 3;
        inmenu = true;

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

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer >= 2)
            {
                Color temp = logo.color;
                temp.a = 3 - timer;
                logo.color = temp;
            }
            else if (timer < 1)
            {
                Color temp = logo.color;
                temp.a = timer;
                logo.color = temp;
            }
            if (timer <= 0)
            {
                splash.SetActive(false);
                SceneManager.LoadScene(1);
            }
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
        inmenu = false;
        curlevel = lvl;
        SceneManager.LoadScene(curlevel+1);
    }

    public void nextlevel()
    {
        curlevel++;
        if(curlevel == 21)
        {
            curlevel = 0;
            inmenu = true;
        }
        SceneManager.LoadScene(curlevel+1);
    }

    public void reload()
    {
        unpause_game();
        Time.timeScale = 1;
        SceneManager.LoadScene(curlevel+1);
    }

    public void menu()
    {
        unpause_game();
        curlevel = 0;
        Time.timeScale = 1;
        inmenu = true;
        SceneManager.LoadScene(curlevel+1);
    }

    public void pause_game()
    {
        pause.SetActive(true);
    }

    public void unpause_game()
    {
        pause.SetActive(false);
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
    }

}
