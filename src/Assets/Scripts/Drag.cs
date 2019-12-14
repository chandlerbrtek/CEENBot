using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]

/**
 * Class that handles dragging of blocks
 */
public class Drag : MonoBehaviour
{

    //movement data
    //private Vector3 screenPoint;
    private Vector3 offset;
    

    //doubly linked listy of blocks
    private GameObject nextBlock;
    private bool hasNext;
    private GameObject prevBlock;
    private bool hasPrev;


    private GameManager gm;
    private BoxCollider2D nextCollider;

    public int blockId;

    /**
     * Start is called before the first frame update
     */
    private void Start()
    {                                                         
        gm = GameObject.FindObjectOfType<GameManager>();
        nextCollider = GetComponentInChildren<BoxCollider2D>();
        hasNext = false;
        hasPrev = false;
    }

    /**
     * Function setting up data for dragging block movement
     */
    void OnMouseDown()
    {
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

        //if the block was in a list, remove it, but it keeps it's children
        if (hasPrev)
        {
            prevBlock.GetComponent<Drag>().removeChild();
            hasPrev = false;
        }
    }

    /**
     * Move the block to follow the cursor
     */
    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;

        //have the children follow
        alignChildren();
    }

    /**
     * When you stop moving the block, check if it's in position to be added to another list
     */
    private void OnMouseUp()
    {
        checkBlocks();
    }

    /**
     * Returns true if the position given would be in position to be the next block
     */
    public bool shouldSnap(Vector3 pos)
    {

            Vector3 tpos = new Vector3(pos.x, (float)(pos.y + 1.5), pos.z);
            if (nextCollider.bounds.Contains(tpos))
            {
                return true;
            }
       
        
        return false;
    }

    /**
     * set parent block
     */
    public void setPrev(GameObject b)
    {//set parent block
        hasPrev = true;
        prevBlock = b;

    }

    /**
     * remove child block
     */
    public void removeChild()
    {//remove child block
        hasNext = false;
        nextBlock = null;
    }

    /**
     * Adds a block to this list
     */
    public void addBlock(GameObject b)
    {//adds a block to this list
        if(b.GetComponent<Drag>().blockId == blockId)
        {//if this block is trying to add itself as a child, don't
            //Debug.Log("Woah");
            return;
        }
        if(hasNext)
        {//if this block has a child, add the block as a child of that block instead
            nextBlock.GetComponent<Drag>().addBlock(b);
        }
        else
        { //otherwise the block is the child of this block
            nextBlock = b;
            hasNext = true;
            b.GetComponent<Drag>().setPrev(this.gameObject);
            alignChildren();
        }
    }

    /**
     * Makes sure that the children of this block are lined up with this one
     */
    public void alignChildren()
    {//make sure that the children of this block are lined up with this one
        if(hasNext)
        {
            Vector3 tpos = nextCollider.transform.position;
            tpos.y = (float)(tpos.y - 1.5);
            nextBlock.transform.position = tpos;
            nextBlock.GetComponent<Drag>().alignChildren();
        }
    }

    /**
     * Checks to find if there is a block that this block is in position under
     */
    public void checkBlocks()
    {//check to find if there is a block that this block is in position under
        foreach(GameObject go in gm.blocks)
        {
            if (blockId != go.GetComponent<Drag>().blockId)
            {
                if (go.GetComponent<Drag>().shouldSnap(transform.position))
                {
                    go.GetComponent<Drag>().addBlock(this.gameObject);
                }
            }

        }
        
    }
}