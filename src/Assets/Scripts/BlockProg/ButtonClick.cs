﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

/**
 * This class functions as a button that spawns a new 
 * Programming block when clicked on.
 */
public class ButtonClick : MonoBehaviour
{
    private Component newBlock; //the last block created, hack workaround for drag function not starting correctly
    private Vector3 offset;
    private Vector3 screenPoint;
    private GameObject newButton;
    bool hasNewButton;
    private GameManager gm;
    public int buttonType;
    public Sprite button;
    public Sprite buttonIcon;
    bool icon = false;

   /**
    * Intializes class variables
    */
    void Start()
    {
        hasNewButton = false;
        gm = GameObject.FindObjectOfType<GameManager>();
        screenPoint = new Vector3(0, 0, 3);
    }

    /**
     * When the mouse is clicked on this button
     * spawn a block of the corresponding type and
     * set it to the position of the mouse
     */
    void OnMouseDown()//when the button is clicked on have the game manager make a new instance of the corresponding block
    {
        
        newButton = gm.instantiateBlock(buttonType);
        hasNewButton = true;
        newButton.GetComponent<SpriteRenderer>().sortingOrder = 5;
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    /**
     * Performs button dragging for intial button click
     */
    void OnMouseDrag()
    {
        if(hasNewButton)//hack workaround for drag function not starting correctly
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            newButton.transform.position = curPosition;
        }


    }

    /**
     * When you let the mouse up make sure it checks
     * if it is near another block
     */
    private void OnMouseUp()
    {
        newButton.GetComponent<SpriteRenderer>().sortingOrder = 1;
        hasNewButton = false;
        newButton.GetComponent<BlockBehavior>().checkBlocks();  
    }
    
    /**
     * Swaps between button sprites with text and icons
     */
    public void toggleSprite()
    {
        if(icon)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = button;
            icon = false;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = buttonIcon;
            icon = true;
        }
    }
}
