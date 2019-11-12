using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelloader : MonoBehaviour  {
    public Texture2D level;
    public Color32[] levelc;
    public Sprite[] sprites;

    public bool just_walls;

    public int[,] floormap;
    public GameObject playercamera;
    public Transform walls;
    public Transform enemies;
    public Transform stuff;

    public GameObject Player;
    public GameObject tile;
    public GameObject walkenemy;
    public GameObject flyenemy;
    public GameObject batteryhold;
    public GameObject batterycharge;
    public GameObject batterytoggle;
    public GameObject doorup;
    public GameObject doordown;
    public GameObject doorleft;
    public GameObject doorright;
    public GameObject movingplatform;
    public GameObject deathfloor;
    public GameObject turret;



    public Color32 Playerc;
    public Color32 tilec;
    public Color32 walkenemyc;
    public Color32 flyenemyc;
    public Color32 batteryholdc;
    public Color32 batterychargec;
    public Color32 batterytogglec;
    public Color32 doorupc;
    public Color32 doordownc;
    public Color32 doorleftc;
    public Color32 doorrightc;
    public Color32 movingplatformc;
    public Color32 deathfloorc;
    public Color32 turretc;

    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void getcolors() {
        levelc = level.GetPixels32();
        int width = level.width;
        int height = level.height;
        floormap = new int[width, height];
        for(int i= 0;i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                buildmap(levelc[(j * width) + i], i, j);
            }
        }
	}

    void buildmap(Color32 col,int x,int y)
    {
        if (col.Equals(tilec))
        {
            floormap[x, y] = 1;
            return;
        }
        if (!just_walls)
        {
            if (col.Equals(walkenemyc))
            {
                Instantiate(walkenemy, new Vector3(0.32f * x, 0.32f * y, 0), Quaternion.Euler(0, 0, 0), enemies);
                return;
            }
            if (col.Equals(flyenemyc))
            {
                Instantiate(flyenemy, new Vector3(0.32f * x, 0.32f * y, 0), Quaternion.Euler(0, 0, 0), enemies);
                return;
            }
            if (col.Equals(turretc))
            {
                Instantiate(turret, new Vector3(0.32f * x, 0.32f * y, 0), Quaternion.Euler(0, 0, 0), enemies);
                return;
            }
            if (col.Equals(batterychargec))
            {
                Instantiate(batterycharge, new Vector3(0.32f * x, 0.32f * y, 0), Quaternion.Euler(0, 0, 0), stuff);
                return;
            }
            if (col.Equals(batteryholdc))
            {
                Instantiate(batteryhold, new Vector3(0.32f * x, 0.32f * y, 0), Quaternion.Euler(0, 0, 0), stuff);
                return;
            }
            if (col.Equals(batterytogglec))
            {
                Instantiate(batterytoggle, new Vector3(0.32f * x, 0.32f * y, 0), Quaternion.Euler(0, 0, 0), stuff);
                return;
            }
            if (col.Equals(doordownc))
            {
                Instantiate(doordown, new Vector3(0.32f * x, 0.32f * y, 0), Quaternion.Euler(0, 0, 0), stuff);
                return;
            }
            if (col.Equals(doorupc))
            {
                Instantiate(doorup, new Vector3(0.32f * x, 0.32f * y, 0), Quaternion.Euler(0, 0, 0), stuff);
                return;
            }
            if (col.Equals(doorrightc))
            {
                Instantiate(doorright, new Vector3(0.32f * (x + 2), 0.32f * y, 0), Quaternion.Euler(0, 0, 0), stuff);
                return;
            }
            if (col.Equals(doorleftc))
            {
                Instantiate(doorleft, new Vector3(0.32f * (x - 2), 0.32f * y, 0), Quaternion.Euler(0, 0, 0), stuff);
                return;
            }
            if (col.Equals(movingplatformc))
            {
                Instantiate(movingplatform, new Vector3(0.32f * x, 0.32f * y, 0), Quaternion.Euler(0, 0, 0), stuff);
                return;
            }
            if (col.Equals(Playerc))
            {
                Debug.Log("player");
                GameObject temp2 = Instantiate(Player, new Vector3(0.32f * x, 0.32f * y, 0), Quaternion.Euler(0, 0, 0));
                GameObject temp = Instantiate(playercamera, new Vector3(0.32f * x, 0.32f * y, -10), Quaternion.Euler(0, 0, 0));
                temp.GetComponent<cameracontrol>().player = temp2.GetComponent<Player>();
                return;
            }
            if (col.Equals(deathfloorc))
            {
                Debug.Log("lava");
                Instantiate(deathfloor, new Vector3(0.32f * x, 0.32f * y, 0), Quaternion.Euler(0, 0, 0), walls);
                return;
            }
        }
    }

    void buildtiles()
    {
        bool left = false;
        bool down = false;
        bool right = false;
        bool up = false;
        bool tright = false;
        bool bright = false;
        bool tleft = false;
        bool bleft = false;
        for (int i = 0; i < level.width; i++)
        {
            for (int j = 0; j < level.height; j++)
            {
                left = false;
                down = false;
                right = false;
                up = false;
                tright = false;
                bright = false;
                tleft = false;
                bleft = false;
                if (floormap[i,j] == 1)
                {
                    GameObject temp = Instantiate(tile, new Vector3(0.32f * i, 0.32f * j, 0), Quaternion.Euler(0,0,0),walls);
                    if(i>0)
                    {
                        if(floormap[i-1,j] == 1)
                        {
                            left = true;
                        }
                    }
                    if (i < level.width-1)
                    {
                        if (floormap[i + 1, j] == 1)
                        {
                            right = true;
                        }
                    }
                    if (j > 0)
                    {
                        if (floormap[i, j-1] == 1)
                        {
                            down = true;
                        }
                    }
                    if (j < level.height-1)
                    {
                        if (floormap[i, j+1] == 1)
                        {
                            up = true;
                        }
                    }
                    if (left && down)
                    {
                        if (floormap[i - 1, j -1] == 1)
                        {
                            bleft = true;
                        }
                    }
                    if (down && right)
                    {
                        if (floormap[i + 1, j - 1] == 1)
                        {
                            bright = true;
                        }
                    }
                    if (up && left)
                    {
                        if (floormap[i - 1, j + 1] == 1)
                        {
                            tleft = true;
                        }
                    }
                    if (up && right)
                    {
                        if (floormap[i + 1, j + 1] == 1)
                        {
                            tright = true;
                        }
                    }



                    if (!up && !down && !left && right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite  = sprites[3];
                    }
                    if (!up && !down && left && !right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[0];
                    }
                    if (!up && down && !left && !right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[1];
                    }
                    if (up && !down && !left && !right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[7];
                    }
                    ////////
                    if (up && down && !left && !right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[9];
                    }
                    if (up && !down && left && !right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[8];
                    }
                    if (up && !down && !left && right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[11];
                    }
                    ///////
                    if (!up && down && !left && right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[5];
                    }
                    if (!up && down && left && !right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[2];
                    }
                    /////
                    if (!up && !down && left && right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[4];
                    }
                    /////
                    if (!up && down && left && right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[6];
                    }
                    if (up && !down && left && right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[12];
                    }
                    if (up && down && left && !right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[10];
                    }
                    if (up && down && !left && right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[13];
                    }
                    if (up && down && left && right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[14];
                    }
                    ////
                    if (up && !down && !left && right && !bright && tright && !bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[15];
                    }
                    if (up && !down && left && !right && !bright && !tright && !bleft && tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[17];
                    }
                    if (up && !down && left && right && !bright && tright && !bleft && tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[16];
                    }
                    ///
                    if (!up && down && !left && right && bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[22];
                    }
                    if (!up && down && left && !right && !bright && !tright && bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[21];
                    }
                    if (!up && down && left && right && bright && !tright && bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[23];
                    }
                    ///
                    if (up && down && left && right && bright && tright && bleft && tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[19];
                    }
                    if (up && down && left && !right && !bright && !tright && bleft && tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[20];
                    }
                    if (up && down && !left && right && bright && tright && !bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[18];
                    }
                    ///
                    if (up && !down && left && right && !bright && tright && !bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[24];
                    }
                    if (up && !down && left && right && !bright && !tright && !bleft && tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[25];
                    }
                    if (!up && down && left && right && bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[26];
                    }
                    if (!up && down && left && right && !bright && !tright && bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[27];
                    }
                    ///
                    if (up && down && left && right && !bright && tright && bleft && tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[28];
                    }
                    if (up && down && left && right && bright && !tright && bleft && tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[29];
                    }
                    if (up && down && left && right && bright && tright && !bleft && tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[30];
                    }
                    if (up && down && left && right && bright && tright && bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[31];
                    }
                    ////
                    if (up && down && left && right && bright && !tright && bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[32];
                    }
                    if (up && down && left && right && !bright && !tright && bleft && tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[33];
                    }
                    if (up && down && left && right && !bright && tright && !bleft && tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[34];
                    }
                    if (up && down && left && right && bright && tright && !bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[35];
                    }
                    ///
                    if (up && down && !left && right && bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[36];
                    }
                    if (up && down && !left && right && !bright && tright && !bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[37];
                    }
                    if (up && down && left && !right && !bright && !tright && !bleft && tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[38];
                    }
                    if (up && down && left && !right && !bright && !tright && bleft && !tleft)
                    {
                        temp.GetComponent<SpriteRenderer>().sprite = sprites[39];
                    }
                }
            }
        }
    }

    public void buildlvl()
    {
        getcolors();
        buildtiles();
    }
}
