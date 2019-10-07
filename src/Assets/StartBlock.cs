using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]

public class StartBlock : MonoBehaviour
{

    //movement data
    //private Vector3 screenPoint;
    //private Vector3 offset;


    //doubly linked listy of blocks
    private GameObject nextBlock;
    private bool hasNext;
    //private GameObject prevBlock;
    //private bool hasPrev;


    private GameManager gm;
    private BoxCollider2D nextCollider;

    public int blockId;

    private void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        nextCollider = GetComponentInChildren<BoxCollider2D>();
        hasNext = false;
        blockId = 0;
        gm.blocks.Add(this.gameObject);
        //hasPrev = false;
    }

    //void OnMouseDown()
    //{//function setting up data for dragging block movement
    //    offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    //    //if the block was in a list, remove it, but it keeps it's children
    //    if (hasPrev)
    //    {
    //        prevBlock.GetComponent<Drag>().removeChild();
    //        hasPrev = false;
    //    }
    //}


    //void OnMouseDrag()
    //{
    //    //move the block to follow the cursor
    //    Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
    //    Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
    //    transform.position = curPosition;

    //    //have the children follow
    //    alignChildren();
    //}

    //private void OnMouseUp()
    //{
    //    //when you stop moving the block, check if it's in position to be added to another list
    //    checkBlocks();
    //}

    public bool shouldSnap(Vector3 pos)
    {//returns true if the position given would be in position to be the next block

        Vector3 tpos = new Vector3(pos.x, (float)(pos.y + .93), pos.z);
        if (nextCollider.bounds.Contains(tpos))
        {
            return true;
        }


        return false;
    }

    //public void setPrev(GameObject b)
    //{//set parent block
    //    hasPrev = true;
    //    prevBlock = b;

    //}

    public void removeChild()
    {//remove child block
        hasNext = false;
        nextBlock = null;
    }

    public void addBlock(GameObject b)
    {//adds a block to this list
        if (b.GetComponent<Drag>().blockId == blockId)
        {//if this block is trying to add itself as a child, don't
            //Debug.Log("Woah");
            return;
        }
        if (hasNext)
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

    public void alignChildren()
    {//make sure that the children of this block are lined up with this one
        if (hasNext)
        {
            Vector3 tpos = nextCollider.transform.position;
            tpos.y = (float)(tpos.y - 1.5);
            nextBlock.transform.position = tpos;
            nextBlock.GetComponent<Drag>().alignChildren();
        }
    }

    //public void checkBlocks()
    //{//check to find if there is a block that this block is in position under
    //    foreach (GameObject go in gm.blocks)
    //    {
    //        if (blockId != go.GetComponent<Drag>().blockId)
    //        {
    //            if (go.GetComponent<Drag>().shouldSnap(transform.position))
    //            {
    //                go.GetComponent<Drag>().addBlock(this.gameObject);
    //            }
    //        }

    //    }

    //}
}