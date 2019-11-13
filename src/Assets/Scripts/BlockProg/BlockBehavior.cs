using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]

/**
 * BlockBehavior is a script that governs how individual programming blocks behave
 * Blocks function as a doubly linked list as well
 */
public class BlockBehavior : MonoBehaviour
{

    //movement data
    private Vector3 screenPoint;
    private Vector3 offset;


    //doubly linked listy of blocks
    private GameObject nextBlock;
    public bool hasNext;
    private GameObject prevBlock;
    public bool hasPrev;


    private GameManager gm;
    private BoxCollider2D nextCollider;

    public int blockId;
    public bool root;

    /**
     * Initializes data for this block when created
     */
    private void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        nextCollider = GetComponentInChildren<BoxCollider2D>();
        hasNext = false;
        hasPrev = false;
        screenPoint = new Vector3(0, 0, 3);

    }

    /**
     * When this block is clicked on
     * if it's in a list remove it from that list
     * then collect data for the starting point that was clicked
     */
    void OnMouseDown()
    {//function setting up data for dragging block movement
     // if (blockId != 0)
        Debug.Log("Block #" + blockId + " clicked");
        {
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

            //if the block was in a list, remove it, but it keeps it's children
            if (hasPrev)
            {
                prevBlock.GetComponent<BlockBehavior>().removeChild();
                hasPrev = false;
            }
        }
    }

    /**
     * When the mouse is moved, while it's held down on this block
     * move the block, and all of it's children with it.
     */
    void OnMouseDrag()
    {
      //  if (blockId != 0)
        {
            //move the block to follow the cursor
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;

            //have the children follow
            alignChildren();
        }
    }
    /**
     * When the mouse is released, check to see if this block can be attached to 
     * a previous black
     */
    private void OnMouseUp()
    {
      //  if (blockId != 0)
        {
            //when you stop moving the block, check if it's in position to be added to another list
            checkBlocks();
        }
    }

    /**
     * returns true if the position given would be in position to be the next block
     */
    public bool shouldSnap(Vector3 pos)
    {

        Vector3 tpos = new Vector3(pos.x, (float)(pos.y + .93), pos.z);
        if (nextCollider.bounds.Contains(tpos))
        {
            return true;
        }


        return false;
    }

    /**
     * Set the parent block to the block that was passed
     */
    public void setPrev(GameObject b)
    {
      //  if (blockId != 0)
        {
            hasPrev = true;
            prevBlock = b;
        }

    }

    /**
     * Remove the child block of this block
     */
    public void removeChild()
    {//remove child block
        hasNext = false;
        nextBlock = null;
    }



    /**
     * Function takes in a game block
     * If this block does not have a child it's child is set to the passed block
     * If we have a child, call pass the block recursively to it
     */
    public void addBlock(GameObject b)
    {//adds a block to this list
        if (b.GetComponent<BlockBehavior>().blockId == blockId)
        {//if this block is trying to add itself as a child, don't
            //Debug.Log("Woah");
            return;
        }
        if (hasNext)
        {//if this block has a child, add the block as a child of that block instead
            nextBlock.GetComponent<BlockBehavior>().addBlock(b);
        }
        else
        { //otherwise the block is the child of this block
            nextBlock = b;
            hasNext = true;
            b.GetComponent<BlockBehavior>().setPrev(this.gameObject);
            alignChildren();
        }
    }

    /**
     * Recursive function making sure that the children of this block are lined up with this one
     */
    public void alignChildren()
    {
        if (hasNext)
        {
            Vector3 tpos = nextCollider.transform.position;
            tpos.y = (float)(tpos.y - .93);
            nextBlock.transform.position = tpos;
            nextBlock.GetComponent<BlockBehavior>().alignChildren();
        }
    }

    public void move(Vector3 v)
    {
        transform.position = transform.position + v;
    }

    public GameObject getNext()
    {
        return nextBlock;
    }

    public GameObject getPrevious()
    {
        return prevBlock;
    }
    /**
     * Goes through the GameManager's list of all blocks to find out if this
     * block is in position under another block
     */
    public void checkBlocks()
    { 
      //  if (blockId != 0)
        {
            foreach (GameObject go in gm.blocks)
            {
                if (blockId != go.GetComponent<BlockBehavior>().blockId)
                {
                    if (go.GetComponent<BlockBehavior>().shouldSnap(transform.position))
                    {
                        go.GetComponent<BlockBehavior>().addBlock(this.gameObject);
                    }
                }

            }
        }

    }
}