using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class functions as the behavior for the return button
 */
public class ReturnScript : MonoBehaviour
{
    Vector3 startPosition;
    private GameManager gm;
    public GameObject lm;
    /**
     * Initializes objects on load
     */
    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        startPosition = gm.startButton.transform.position;
    }

    /**
     * on click calls a function to reset the blocks
     */
    void OnMouseDown()
    {//function setting up data for dragging block movement
        resetPosition();
    }

    /**
     * Resets all programming blocks to so that start is at the top of the screen.
     */
    public void resetPosition()
    {
        Vector3 offset = startPosition - gm.startButton.transform.position;
        gm.moveBlocks(offset);
    }

}
