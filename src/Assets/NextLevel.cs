using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public GameObject level;
    public GameObject congrats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseDown()
    {
        Debug.Log("end");
        int lvl = level.GetComponent<levelManager>().getLevel();
        if(lvl == 0)
        {

        }
        else if (lvl == 10)
        {
            lvl = 1;
        }
        else
        {
            lvl++;
        }
        level.GetComponent<levelManager>().setLevel(lvl);
        congrats.GetComponent<congrats>().invis();
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
