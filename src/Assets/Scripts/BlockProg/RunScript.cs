using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunScript : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager gm;
    

    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
    }
    void OnMouseDown()
    {
        gm.executeBlockProgram();

    }
        // Update is called once per frame
    void Update()
    {
        
    }
}
