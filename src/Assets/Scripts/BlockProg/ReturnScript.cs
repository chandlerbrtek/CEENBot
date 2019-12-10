using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnScript : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 startPosition;
    private GameManager gm;
    public GameObject lm;
    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        startPosition = gm.startButton.transform.position;
    }

    void OnMouseDown()
    {//function setting up data for dragging block movement
        resetPosition();
    }

    public void resetPosition()
    {
        Vector3 offset = startPosition - gm.startButton.transform.position;
        gm.moveBlocks(offset);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
