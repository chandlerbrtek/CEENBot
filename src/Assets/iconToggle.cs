using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iconToggle : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject forward;
    public GameObject back;
    public GameObject left;
    public GameObject right;
    public GameObject beep;
    public GameObject bulb;
    public GameObject loop;

    public GameObject gm;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        gm.GetComponent<GameManager>().toggleBlocks();
        Debug.Log("toggling");
        forward.GetComponent<ButtonClick>().toggleSprite();
        back.GetComponent<ButtonClick>().toggleSprite();
        left.GetComponent<ButtonClick>().toggleSprite();
        right.GetComponent<ButtonClick>().toggleSprite();
        beep.GetComponent<ButtonClick>().toggleSprite();
        bulb.GetComponent<ButtonClick>().toggleSprite();
        loop.GetComponent<ButtonClick>().toggleSprite();

    }
}
