using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]

public class Drag : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    private GameObject nextBlock;
    private GameManager gm;
    private BoxCollider2D nextCollider;

    private void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        nextCollider = GetComponentInChildren<BoxCollider2D>();

        //Debug.Log("Pos:" + transform.position);

        //Debug.Log("Snap Pos:" + getSnapPos());
    }

    void OnMouseDown()
    {

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    
    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    private void OnMouseUp()
    {
        checkBlocks();
    }

    public bool shouldSnap(Vector3 pos)
    {

        Vector3 tpos = new Vector3(pos.x, (float)(pos.y + 1.5), pos.z);
        if(nextCollider.bounds.Contains(tpos))
        {
            return true;
        }
        return false;
    }

    public Vector3 getSnapPos()
    {
        Vector3 tpos = nextCollider.transform.position;
        tpos.y = (float)(tpos.y - 1.5);
        return tpos;
    }

    public void checkBlocks()
    {
        int i = 0;
        foreach(GameObject go in gm.blocks)
        {
            i++;
           // Debug.Log("item:" + i);
            if(go.GetComponent<Drag>().shouldSnap(transform.position))
            {
                //Debug.Log("should snap");
                //Debug.Log("Pos:" + transform.position);

                Debug.Log("Snap Pos:" + go.GetComponent<Drag>().getSnapPos());
                transform.position = go.GetComponent<Drag>().getSnapPos();
            }

        }
        
    }
}