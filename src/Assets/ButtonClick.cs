using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]


public class ButtonClick : MonoBehaviour
{
    private Component newBlock; //the last block created, hack workaround for drag function not starting correctly
    private Vector3 offset;
    private Vector3 screenPoint;
    private GameObject newButton;
    bool hasNewButton;

    // Start is called before the first frame update
    void Start()
    {
        hasNewButton = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()//when the button is clicked on have the game manager make a new instance of the corresponding block
    {
        GameManager gm = GameObject.FindObjectOfType<GameManager>();
        newButton = gm.instantiateBlock();
        hasNewButton = true;
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        if(hasNewButton)//hack workaround for drag function not starting correctly
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            newButton.transform.position = curPosition;
        }


    }

    private void OnMouseUp()
    {
        hasNewButton = false;
    }
}
