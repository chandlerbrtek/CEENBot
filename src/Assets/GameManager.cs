using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject button1;
    private GameObject newBlock;
    private bool hasNewBlock;

    public List<GameObject> blocks;
    void Start()
    {
        blocks = new List<GameObject>();
    }

    public GameObject instantiateBlock()
    {
        newBlock = Instantiate(button1);
        newBlock.transform.position = new Vector3(100, 100, 0);
        blocks.Add(newBlock);
        return newBlock;

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
