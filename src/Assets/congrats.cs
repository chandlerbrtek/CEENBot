using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class congrats : MonoBehaviour
{
    public GameObject menu;
    public GameObject next;
    public GameObject oneStar;
    public GameObject twoStar;
    public GameObject threeStar;
    public GameObject[] star;

    // Start is called before the first frame update
    void Start()
    {
        star = new GameObject[] { oneStar,twoStar,threeStar};
    }

    public void complete(int stars)
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
        menu.GetComponent<SpriteRenderer>().enabled = true;
        next.GetComponent<SpriteRenderer>().enabled = true;
        menu.GetComponent<BoxCollider2D>().enabled = true;
        next.GetComponent<BoxCollider2D>().enabled = true;
        star[stars-1].GetComponent<SpriteRenderer>().enabled = true;


    }

    public void invis()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        menu.GetComponent<SpriteRenderer>().enabled = false;
        next.GetComponent<SpriteRenderer>().enabled = false;
        oneStar.GetComponent<SpriteRenderer>().enabled = false;
        twoStar.GetComponent<SpriteRenderer>().enabled = false;
        threeStar.GetComponent<SpriteRenderer>().enabled = false;
        menu.GetComponent<BoxCollider2D>().enabled = false;
        next.GetComponent<BoxCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
