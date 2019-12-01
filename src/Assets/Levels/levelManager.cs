using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour
{
    public float[,] level1;
    public float[,] level2;
    public float[,] level3;
    public float[,] level4;
    public float[,] level5;
    public float[,] level6;
    public float[,] level7;
    public float[,] level8;
    public float[,] level9;
    public float[,] level10;



    public float[,] currLevel;

    public GameObject start;
    public GameObject finish;
    public GameObject grass;
    public GameObject wall;
    public GameObject water;
    public GameObject mud;
    public GameObject boulder;

    public GameObject robot;

    float rows = 5;
    float cols = 7;

    float xoffset = -6;
    float yoffset = 4;

    int level;
    Vector3 startPos;
    Vector2 startLocation;
    Vector2 endLocation;

    public List<GameObject> tiles;
    public List<GameObject> boulders;
    // Start is called before the first frame update

    private void Awake()
    {

    }

    void Start()
    {
        tiles = new List<GameObject>();
        boulders = new List<GameObject>();
        startPos = new Vector3(-2.888f, 3.974f);

        level1 = new float[,] { { 8,1,1,0,0,0,0},
                                { 0,1,1,0,0,0,0},
                                { 4,0,0,0,0,0,9},
                                { 0,3,2,0,0,0,0},
                                { 0,3,2,0,0,0,0}
        };

        level2 = new float[,] { { 8,1,1,0,0,0,0},
                                { 0,1,1,0,0,0,0},
                                { 0,0,0,0,0,0,9},
                                { 0,3,2,0,0,0,0},
                                { 0,3,2,0,0,0,0}
        };

        level3 = new float[,] { { 8,1,1,0,0,0,0},
                                { 0,1,1,0,0,0,0},
                                { 0,0,0,0,0,0,9},
                                { 0,3,2,0,0,0,0},
                                { 0,3,2,0,0,0,0}
        };

        level4 = new float[,] { { 8,1,1,0,0,0,0},
                                { 0,1,1,0,0,0,0},
                                { 0,0,0,0,0,0,9},
                                { 0,3,2,0,0,0,0},
                                { 0,3,2,0,0,0,0}
        };

        level5 = new float[,] { { 8,1,1,0,0,0,0},
                                { 0,1,1,0,0,0,0},
                                { 0,0,0,0,0,0,9},
                                { 0,3,2,0,0,0,0},
                                { 0,3,2,0,0,0,0}
        };

        level6 = new float[,] { { 8,1,1,0,0,0,0},
                                { 0,1,1,0,0,0,0},
                                { 0,0,0,0,0,0,9},
                                { 0,3,2,0,0,0,0},
                                { 0,3,2,0,0,0,0}
        };

        level7 = new float[,] { { 8,1,1,0,0,0,0},
                                { 0,1,1,0,0,0,0},
                                { 0,0,0,0,0,0,9},
                                { 0,3,2,0,0,0,0},
                                { 0,3,2,0,0,0,0}
        };

        level8 = new float[,] { { 8,1,1,0,0,0,0},
                                { 0,1,1,0,0,0,0},
                                { 0,0,0,0,0,0,9},
                                { 0,3,2,0,0,0,0},
                                { 0,3,2,0,0,0,0}
        };

        level9 = new float[,] { { 8,1,1,0,0,0,0},
                                { 0,1,1,0,0,0,0},
                                { 0,0,0,0,0,0,9},
                                { 0,3,2,0,0,0,0},
                                { 0,3,2,0,0,0,0}
        };

        level10 = new float[,] {{ 8,1,1,0,0,0,0},
                                { 0,1,1,0,0,0,0},
                                { 0,0,0,0,0,0,9},
                                { 0,3,2,0,0,0,0},
                                { 0,3,2,0,0,0,0}
        };



        setLevel(1);

        if (level == 1)
        {
            
            Debug.Log(level1[0, 0]);
            //generateLevel();
        }
    }

    public void setLevel(int i)
    {
        level = i;
        if(i==1)
        {
            currLevel = level1;
        }
        if (i == 2)
        {
            currLevel = level2;
        }
        if (i == 3)
        {
            currLevel = level3;
        }
        if (i == 4)
        {
            currLevel = level4;
        }
        if (i == 5)
        {
            currLevel = level5;
        }
        if (i == 6)
        {
            currLevel = level6;
        }
        if (i == 7)
        {
            currLevel = level7;
        }
        if (i == 8)
        {
            currLevel = level8;
        }
        if (i == 9)
        {
            currLevel = level9;
        }
        if (i == 10)
        {
            currLevel = level10;
        }
        generateLevel();
    }
    public void generateLevel()
    {
        for (int i = 0; i < rows; i++)
        {
            
            for (int j = 0; j < cols; j++)
            {
                if (currLevel[i, j] == 0)
                {
                    GameObject go = Instantiate(grass);
                    tiles.Add(go);
                    go.transform.position = startPos + new Vector3(j * 2, i * -2);
                }
                if (currLevel[i, j] == 1)
                {
                    GameObject go = Instantiate(wall);
                    tiles.Add(go);
                    go.transform.position = startPos + new Vector3(j * 2, i * -2);
                }
                if (currLevel[i, j] == 2)
                {
                    GameObject go = Instantiate(water);
                    tiles.Add(go);
                    go.transform.position = startPos + new Vector3(j * 2, i * -2);
                }
                if (currLevel[i, j] == 3)
                {
                    GameObject go = Instantiate(mud);
                    tiles.Add(go);
                    go.transform.position = startPos + new Vector3(j * 2, i * -2);
                }
                if(currLevel[i, j] == 4)
                {//grass with boulder
                    GameObject go = Instantiate(grass);
                    tiles.Add(go);
                    go.transform.position = startPos + new Vector3(j * 2, i * -2);
                    GameObject b = Instantiate(boulder);
                    b.transform.position = startPos + new Vector3(j * 2, i * -2);
                    boulders.Add(b);

                }
                if (currLevel[i, j] == 8)
                {
                    startLocation = new Vector2(i, j);
                    GameObject go = Instantiate(start);
                    tiles.Add(go);
                    go.transform.position = startPos + new Vector3(j * 2, i * -2);
                    robot.GetComponent<robot>().setPos(go.transform.position);
                    robot.GetComponent<robot>().position = new Vector3(j, i, 0);
                }
                if (currLevel[i, j] == 9)
                {
                    endLocation = new Vector2(i, j);
                    GameObject go = Instantiate(finish);
                    tiles.Add(go);
                    go.transform.position = startPos + new Vector3(j * 2, i * -2);
                }

            }
        }
    }


    public void right()
    {
        robot.GetComponent<robot>().right();
    }

    public void left()
    {
        robot.GetComponent<robot>().left();
    }

    public void forward()
    {
        if (legalMove(robot.GetComponent<robot>().getForward()))
        {
            //Debug.Log("moving to:" + robot.GetComponent<robot>().getForward());
            robot.GetComponent<robot>().forward();
        }
    }

    public void back()
    {
        if (legalMove(robot.GetComponent<robot>().getBackward()))
        {
           // Debug.Log("moving to:" + robot.GetComponent<robot>().getBackward());
            robot.GetComponent<robot>().back();
        }
    }

    public bool legalMove(Vector3 pos)
    {
        
        if (pos.x >= 0 && pos.x < cols && pos.y>=0 && pos.y<rows)
        {
          // Debug.Log("Checking" + pos + " val:" + currLevel[(int)pos.y, (int)pos.x]);
            if (currLevel[(int)pos.y,(int)pos.x] != 1 && currLevel[(int)pos.y, (int)pos.x]!=2)
            {
                //Debug.Log("all clear");
                return true;
            }
        }
        return false;
    }

    public void reset()
    {
        robot.GetComponent<robot>().reset();
        tiles.Clear();
        generateLevel();

    }

    public Vector2 getPos(GameObject go)
    {
        Vector2 rval = new Vector2();
        Vector3 temp = go.transform.position - startPos;
        rval.x = temp.x / 2;
        rval.y = temp.y / -2;
        return rval;
    }

    public GameObject getBoulder(int x, int y)
    {
        GameObject rval = null;
        Vector2 pos = new Vector2(x, y);
        foreach(GameObject go in boulders)
        {
            if(getPos(go)==pos)
            {
                rval = go;
            }
        }
        return rval;
    }
}
