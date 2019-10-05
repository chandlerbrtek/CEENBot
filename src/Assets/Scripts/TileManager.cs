using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    public GameObject player;
    public GameObject redhouse;
    public GameObject bluehouse;
    public GameObject tree;
    public GameObject kill;
    public GameObject bonus;
    public GameObject tile;
    public GameObject[] Hardtiles;
    float zCounter = 80;


    // Use this for initialization

    void Start () {
        spawnTile();
    }
	
	// Update is called once per frame
	void Update () {
        if (player.transform.position.z >= zCounter - 80)
        {
            if (zCounter <= 1500)
            {
                spawnTile();
            }
            else if (zCounter > 1500)
            {
                spawnHardTiles();
            }
        }
    }

    private void spawnTile()
    {
        GameObject go;
        go = Instantiate(tile) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = new Vector3(0f, -.01f, zCounter);
        spawnDestructables();
        zCounter = zCounter + 20;
    }

    private void spawnHardTiles()
    {
        int ratio = Random.Range(0, 11);
        if (ratio < 3)
        {
            GameObject go;
            go = Instantiate(Hardtiles[0]) as GameObject;
            go.transform.SetParent(transform);
            go.transform.position = new Vector3(0f, -.01f, zCounter);
            spawnDestructables();
            zCounter = zCounter + 20;
        }
        else
        {
            GameObject go;
            go = Instantiate(Hardtiles[1]) as GameObject;
            go.transform.SetParent(transform);
            go.transform.position = new Vector3(0f, -.01f, zCounter);
            spawnDestructables();
            zCounter = zCounter + 20;
        }
    }

    private void spawnDestructables()
    {
        float leftLimit = -10;
        float rightLimit = 10;
        float zLimit = zCounter - 20;
        GameObject go;

        if (player.transform.position.z <= 500){
            for(int x = 0; x < 6; x++)
            {
                int temp = Random.Range(1, 11);
                if(temp <= 4)
                {
                    go = Instantiate(tree) as GameObject;
                    go.transform.SetParent(transform);
                    go.transform.position = new Vector3(Random.Range(leftLimit, rightLimit), .5f, Random.Range(zLimit, zCounter));

                    go = Instantiate(tree) as GameObject;
                    go.transform.SetParent(transform);
                    go.transform.position = new Vector3(Random.Range(leftLimit, rightLimit), .5f, Random.Range(zLimit, zCounter));
                }
                else if(temp == 5 || temp == 6)
                {
                    go = Instantiate(redhouse) as GameObject;
                    go.transform.SetParent(transform);
                    go.transform.position = new Vector3(Random.Range(leftLimit, rightLimit), .5f, Random.Range(zLimit, zCounter));
                }
                else if (temp == 7 || temp == 8)
                {
                    go = Instantiate(bluehouse) as GameObject;
                    go.transform.SetParent(transform);
                    go.transform.position = new Vector3(Random.Range(leftLimit, rightLimit), .5f, Random.Range(zLimit, zCounter));
                }
                else
                {
                    go = Instantiate(kill) as GameObject;
                    go.transform.SetParent(transform);
                    go.transform.position = new Vector3(Random.Range(leftLimit, rightLimit), .5f, Random.Range(zLimit, zCounter));
                }

            }
        }

        if (player.transform.position.z > 500 && player.transform.position.z < 1500)
        {
            for (int x = 0; x < 8; x++)
            {
                int temp = Random.Range(1, 11);
                if (temp <= 2)
                {
                    go = Instantiate(tree) as GameObject;
                    go.transform.SetParent(transform);
                    go.transform.position = new Vector3(Random.Range(leftLimit, rightLimit), .5f, Random.Range(zLimit, zCounter));
                }
                else if (temp == 3 || temp == 4)
                {
                    go = Instantiate(redhouse) as GameObject;
                    go.transform.SetParent(transform);
                    go.transform.position = new Vector3(Random.Range(leftLimit, rightLimit), .5f, Random.Range(zLimit, zCounter));
                }
                else if (temp == 5 || temp == 6)
                {
                    go = Instantiate(bluehouse) as GameObject;
                    go.transform.SetParent(transform);
                    go.transform.position = new Vector3(Random.Range(leftLimit, rightLimit), .5f, Random.Range(zLimit, zCounter));
                }
                else if (temp == 7 || temp == 8 || temp == 9)
                {
                    go = Instantiate(kill) as GameObject;
                    go.transform.SetParent(transform);
                    go.transform.position = new Vector3(Random.Range(leftLimit, rightLimit), .5f, Random.Range(zLimit, zCounter));
                }
                else
                {
                    go = Instantiate(bonus) as GameObject;
                    go.transform.SetParent(transform);
                    go.transform.position = new Vector3(Random.Range(leftLimit, rightLimit), .5f, Random.Range(zLimit, zCounter));
                }
            }
        }

        if (player.transform.position.z > 1500)
        {
            for (int x = 0; x < 10; x++)
            {
                int temp = Random.Range(1, 11);
                if (temp <= 0)
                {
                    go = Instantiate(tree) as GameObject;
                    go.transform.SetParent(transform);
                    go.transform.position = new Vector3(Random.Range(leftLimit, rightLimit), .5f, Random.Range(zLimit, zCounter));
                }
                else if (temp == 1)
                {
                    go = Instantiate(redhouse) as GameObject;
                    go.transform.SetParent(transform);
                    go.transform.position = new Vector3(Random.Range(leftLimit, rightLimit), .5f, Random.Range(zLimit, zCounter));
                }
                else if (temp == 2 || temp == 3)
                {
                    go = Instantiate(bluehouse) as GameObject;
                    go.transform.SetParent(transform);
                    go.transform.position = new Vector3(Random.Range(leftLimit, rightLimit), .5f, Random.Range(zLimit, zCounter));
                }
                else if (temp == 4 || temp == 5 || temp == 6)
                {
                    go = Instantiate(kill) as GameObject;
                    go.transform.SetParent(transform);
                    go.transform.position = new Vector3(Random.Range(leftLimit, rightLimit), .5f, Random.Range(zLimit, zCounter));
                }
                else
                {
                    go = Instantiate(bonus) as GameObject;
                    go.transform.SetParent(transform);
                    go.transform.position = new Vector3(Random.Range(leftLimit, rightLimit), .5f, Random.Range(zLimit, zCounter));
                }
            }
        }


    }
}
