using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelloader : MonoBehaviour {
    public Texture2D level;
    public Color32[] levelc;

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
    [ExecuteInEditMode]
    void buildmap(Color32 col,int x,int y)
    {
        Debug.Log(col.ToString());
        if (col.Equals(tilec))
        {
            floormap[x, y] = 1;
            return;
        }
        if (col.Equals(walkenemyc))
        {
            Instantiate(walkenemy, new Vector3(0.32f * x, 0.32f * y, 0), transform.rotation,enemies);
            return;
        }
        if (col.Equals(flyenemyc))
        {
            Instantiate(flyenemy, new Vector3(0.32f * x, 0.32f * y, 0), transform.rotation,enemies);
            return;
        }
        if (col.Equals(turretc))
        {
            Instantiate(turret, new Vector3(0.32f * x, 0.32f * y, 0), transform.rotation, enemies);
            return;
        }
        if (col.Equals(batterychargec))
        {
            Instantiate(batterycharge, new Vector3(0.32f * x, 0.32f * y, 0), transform.rotation, stuff);
            return;
        }
        if (col.Equals(batteryholdc))
        {
            Instantiate(batteryhold, new Vector3(0.32f * x, 0.32f * y, 0), transform.rotation, stuff);
            return;
        }
        if (col.Equals(batterytogglec))
        {
            Instantiate(batterytoggle, new Vector3(0.32f * x, 0.32f * y, 0), transform.rotation, stuff);
            return;
        }
        if (col.Equals(doordownc))
        {
            Instantiate(doordown, new Vector3(0.32f * x, 0.32f * y, 0), transform.rotation, stuff);
            return;
        }
        if (col.Equals(doorupc))
        {
            Instantiate(doorup, new Vector3(0.32f * x, 0.32f * y, 0), transform.rotation, stuff);
            return;
        }
        if (col.Equals(doorrightc))
        {
            Instantiate(doorright, new Vector3(0.32f * (x+2), 0.32f * y, 0), transform.rotation, stuff);
            return;
        }
        if (col.Equals(doorleftc))
        {
            Instantiate(doorleft, new Vector3(0.32f * (x-2), 0.32f * y, 0), transform.rotation, stuff);
            return;
        }
        if (col.Equals(movingplatformc))
        {
            Instantiate(movingplatform, new Vector3(0.32f * x, 0.32f * y, 0), transform.rotation,stuff);
            return;
        }
        if (col.Equals(Playerc))
        {
            Debug.Log("player");
            GameObject temp2 = Instantiate(Player, new Vector3(0.32f * x, 0.32f * y, 0), transform.rotation);
            GameObject temp =  Instantiate(playercamera, new Vector3(0.32f * x, 0.32f * y, -10), transform.rotation);
            temp.GetComponent<cameracontrol>().player = temp2.GetComponent<Player>();
            return;
        }
        if (col.Equals(deathfloorc))
        {
            Debug.Log("lava");
            Instantiate(deathfloor, new Vector3(0.32f * x, 0.32f * y, 0), transform.rotation,walls);
            return;
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
                    GameObject temp = Instantiate(tile, new Vector3(0.32f * i, 0.32f * j, 0), transform.rotation,walls);
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
                        temp.GetComponent<wallsprite>().spritenum = 3;
                    }
                    if (!up && !down && left && !right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 0;
                    }
                    if (!up && down && !left && !right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 1;
                    }
                    if (up && !down && !left && !right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 7;
                    }
                    ////////
                    if (up && down && !left && !right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 9;
                    }
                    if (up && !down && left && !right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 8;
                    }
                    if (up && !down && !left && right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 11;
                    }
                    ///////
                    if (!up && down && !left && right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 5;
                    }
                    if (!up && down && left && !right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 2;
                    }
                    /////
                    if (!up && !down && left && right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 4;
                    }
                    /////
                    if (!up && down && left && right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 6;
                    }
                    if (up && !down && left && right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 12;
                    }
                    if (up && down && left && !right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 10;
                    }
                    if (up && down && !left && right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 13;
                    }
                    if (up && down && left && right && !bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 14;
                    }
                    ////
                    if (up && !down && !left && right && !bright && tright && !bleft && !tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 15;
                    }
                    if (up && !down && left && !right && !bright && !tright && !bleft && tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 17;
                    }
                    if (up && !down && left && right && !bright && tright && !bleft && tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 16;
                    }
                    ///
                    if (!up && down && !left && right && bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 22;
                    }
                    if (!up && down && left && !right && !bright && !tright && bleft && !tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 21;
                    }
                    if (!up && down && left && right && bright && !tright && bleft && !tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 23;
                    }
                    ///
                    if (up && down && left && right && bright && tright && bleft && tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 19;
                    }
                    if (up && down && left && !right && !bright && !tright && bleft && tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 20;
                    }
                    if (up && down && !left && right && bright && tright && !bleft && !tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 18;
                    }
                    ///
                    if (up && !down && left && right && !bright && tright && !bleft && !tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 24;
                    }
                    if (up && !down && left && right && !bright && !tright && !bleft && tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 25;
                    }
                    if (!up && down && left && right && bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 26;
                    }
                    if (!up && down && left && right && !bright && !tright && bleft && !tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 27;
                    }
                    ///
                    if (up && down && left && right && !bright && tright && bleft && tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 28;
                    }
                    if (up && down && left && right && bright && !tright && bleft && tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 29;
                    }
                    if (up && down && left && right && bright && tright && !bleft && tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 30;
                    }
                    if (up && down && left && right && bright && tright && bleft && !tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 31;
                    }
                    ////
                    if (up && down && left && right && bright && !tright && bleft && !tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 32;
                    }
                    if (up && down && left && right && !bright && !tright && bleft && tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 33;
                    }
                    if (up && down && left && right && !bright && tright && !bleft && tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 34;
                    }
                    if (up && down && left && right && bright && tright && !bleft && !tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 35;
                    }
                    ///
                    if (up && down && !left && right && bright && !tright && !bleft && !tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 36;
                    }
                    if (up && down && !left && right && !bright && tright && !bleft && !tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 37;
                    }
                    if (up && down && left && !right && !bright && !tright && !bleft && tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 38;
                    }
                    if (up && down && left && !right && !bright && !tright && bleft && !tleft)
                    {
                        temp.GetComponent<wallsprite>().spritenum = 39;
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
