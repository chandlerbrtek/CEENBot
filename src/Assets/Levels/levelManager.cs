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

    public GameObject robot;

    float rows = 5;
    float cols = 7;

    float xoffset = -6;
    float yoffset = 4;

    int level;
    Vector3 startPos;
    Vector2 startLocation;
    Vector2 endLocation;

    // Start is called before the first frame update

    private void Awake()
    {

    }

    void Start()
    {

        startPos = new Vector3(-2.888f, 3.974f);

        float[,] level1 = {     { 8,1,1,0,0,0,0},
                                { 0,1,1,0,0,0,0},
                                { 0,0,0,0,0,0,9},
                                { 0,3,2,0,0,0,0},
                                { 0,3,2,0,0,0,0}
        };

        level = 1;

        if (level == 1)
        {
            Debug.Log(level1[0, 0]);
            generateLevel(level1);
        }
    }

    public void generateLevel(float[,] lvl)
    {
        currLevel = lvl;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (lvl[i, j] == 0)
                {
                    GameObject go = Instantiate(grass);
                    go.transform.position = startPos + new Vector3(j * 2, i * -2);
                }
                if (lvl[i, j] == 1)
                {
                    GameObject go = Instantiate(wall);
                    go.transform.position = startPos + new Vector3(j * 2, i * -2);
                }
                if (lvl[i, j] == 2)
                {
                    GameObject go = Instantiate(water);
                    go.transform.position = startPos + new Vector3(j * 2, i * -2);
                }
                if (lvl[i, j] == 3)
                {
                    GameObject go = Instantiate(mud);
                    go.transform.position = startPos + new Vector3(j * 2, i * -2);
                }
                if (lvl[i, j] == 8)
                {
                    startLocation = new Vector2(i, j);
                    GameObject go = Instantiate(start);
                    go.transform.position = startPos + new Vector3(j * 2, i * -2);
                    robot.GetComponent<robot>().setPos(go.transform.position);
                    robot.GetComponent<robot>().position = new Vector3(j, i, 0);
                }
                if (lvl[i, j] == 9)
                {
                    endLocation = new Vector2(i, j);
                    GameObject go = Instantiate(finish);
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
    }

}
