using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menu : MonoBehaviour {
    public int menustate;
    public GameObject Logoscreen;
    public GameObject main;
    public GameObject levelselect;
    public GameObject instructions;
    public Image logo;
    public float timer;

    void Start () {
        menustate = 0;
        timer = 3;
        instructions.SetActive(false);
        main.SetActive(false);
        levelselect.SetActive(false);
        Logoscreen.SetActive(true);

        Button[] levels = levelselect.GetComponentsInChildren<Button>();

        int temp = PlayerPrefs.GetInt("levelscomplete");
        for (int i = 1; i < 21; i++)
        {
            if(i <= temp)
            {
                levels[i].interactable = true;
                levels[i].image.color = Color.green;
            }
            else if(i == temp+1)
            {
                levels[i].interactable = true;
            }
            else
            {
                levels[i].interactable = false;
                levels[i].image.color = Color.red;
            }
        }
    }

    void Update () {
		if(timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer >= 2)
            {
                Color temp = logo.color;
                temp.a = 3 - timer;
                logo.color = temp;
            }
            else if(timer < 1)
            {
                Color temp = logo.color;
                temp.a = timer;
                logo.color = temp;
            }
            if(timer <= 0)
            {
                Logoscreen.SetActive(false);
                main.SetActive(true);
            }
        }
	}

    public void site()
    {
        Application.OpenURL("http://www.lakeshoregames.co");
    }

    public void levelsel(int i)
    {
        Manager.instance.play(i);
    }

    public void instrucitonbutton()
    {
        instructions.SetActive(true);
        main.SetActive(false);
        levelselect.SetActive(false);
    }

    public void playbutton()
    {
        instructions.SetActive(false);
        main.SetActive(false);
        levelselect.SetActive(true);
    }

    public void back()
    {
        instructions.SetActive(false);
        main.SetActive(true);
        levelselect.SetActive(false);
    }
}
