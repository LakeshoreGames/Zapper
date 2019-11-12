using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menu : MonoBehaviour {
    public int menustate;
    public GameObject main;
    public GameObject levelselect;
    public GameObject instructions;

    void Start () {
        menustate = 0;
        instructions.SetActive(false);
        main.SetActive(true);
        levelselect.SetActive(false);

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
