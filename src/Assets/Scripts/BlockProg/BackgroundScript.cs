using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class functions as a behavior for the background image to allow dragging all blocks when clicking on the background.
 */
public class BackgroundScript : MonoBehaviour
{
    private Vector3 start;
    private Vector3 screenPoint;
    private GameManager gm;
    // Start is called before the first frame update
    /**
     * On start grab a copy of the GameManager
     */
    void Start()
    {

        gm = GameObject.FindObjectOfType<GameManager>();
    }


    /**
     * If the background is clicked save that position for refrence.
     */
    void OnMouseDown()
    {//function setting up data for dragging block movement
        start = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    /**
     * While draging on the background call moveBlocks() in GameManager with the move vector.
     */
    void OnMouseDrag()
    {
        Vector3 currPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        Vector3 offset = currPos - start;
        start = currPos;
        offset.x = 0;
        gm.moveBlocks(offset);

    }
}
