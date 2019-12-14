using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class handles the congradulations popup for when you beat a level, the related buttons and the star display
 */
public class congrats : MonoBehaviour
{
    public GameObject menu;
    public GameObject next;
    public GameObject oneStar;
    public GameObject twoStar;
    public GameObject threeStar;
    public GameObject[] star;

    void Start()
    {
        star = new GameObject[] { oneStar, twoStar, threeStar };
    }

    /**
     * This function is called by the game manager when the level is complete. It makes it's objects visible and the correct star displays.
     * @param stars the number of stars the player got aon this level
     */
    public void complete(int stars)
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
        menu.GetComponent<SpriteRenderer>().enabled = true;
        next.GetComponent<SpriteRenderer>().enabled = true;
        menu.GetComponent<BoxCollider2D>().enabled = true;
        next.GetComponent<BoxCollider2D>().enabled = true;
        star[stars - 1].GetComponent<SpriteRenderer>().enabled = true;


    }

    /**
     * When a new level is selected the congrats objects are set to invisible again.
     */
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

}
