using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    private Vector3 start;
    private Vector3 screenPoint;
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {

        gm = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {//function setting up data for dragging block movement
        start = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 currPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        Vector3 offset = currPos - start;
        start = currPos;
        offset.x = 0;
        gm.moveBlocks(offset);

            //have the children follow
    }
}
