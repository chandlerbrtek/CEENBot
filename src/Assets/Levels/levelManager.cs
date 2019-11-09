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

    public GameObject start;
    public GameObject finish;
    public GameObject grass;
    public GameObject wall;

    float rows = 5;
    float cols = 7;

    float xoffset = -6;
    float yoffset = 4;

    int level;
    Vector3 startPos;

    // Start is called before the first frame update

    private void Awake()
    {

    }

    void Start()
    {

        startPos = new Vector3(-4, 4);

        float[,] level1 = {     { 8,1,1,0,0,0,0},
                                { 0,1,1,0,0,0,0},
                                { 0,0,0,0,0,0,9},
                                { 0,1,1,0,0,0,0},
                                { 0,1,1,0,0,0,0}
        };

        level = 1;

        if(level == 1)
        {
            Debug.Log(level1[0, 0]);
            generateLevel(level1);
        }
    }

    public void generateLevel(float[,] lvl)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if( lvl[i,j] == 0)
                {
                    GameObject go = Instantiate(grass);
                    go.transform.position = startPos + new Vector3(j * 2, i * -2);
                }
                if (lvl[i, j] == 1)
                {
                    GameObject go = Instantiate(wall);
                    go.transform.position = startPos + new Vector3(j * 2, i * -2);
                }
                if (lvl[i, j] == 8)
                {
                    GameObject go = Instantiate(start);
                    go.transform.position = startPos + new Vector3(j * 2, i * -2);
                }
                if (lvl[i, j] == 9)
                {
                    GameObject go = Instantiate(finish);
                    go.transform.position = startPos + new Vector3(j * 2, i * -2);
                }
            }
        }
    }
}
