using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This program represents the behavior for the stop button
 * 
 */
public class stop : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager gm;

    /**
     * Initializes game manager object
     */
    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    /**
     * Calls the stop execution function
     */
    void OnMouseDown()
    {

        gm.stopExecution();

    }

}
