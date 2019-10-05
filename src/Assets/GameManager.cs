using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject button1;
    private GameObject newBlock;
    private bool hasNewBlock;
    void Start()
    {
          
    }

    public GameObject instantiateBlock()
    {
        newBlock = Instantiate(button1);
        return newBlock;

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
