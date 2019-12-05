using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stop : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager gm;


    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
    }
    void OnMouseDown()
    {
        Debug.Log("what");
        //gm.running = false;
        //gm.executeBlockProgram();
        gm.stopExecution();

    }
    // Update is called once per frame
    void Update()
    {

    }
}
