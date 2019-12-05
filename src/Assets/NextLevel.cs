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
        level.GetComponent<levelManager>().setLevel(level.GetComponent<levelManager>().getLevel() + 1);
        congrats.GetComponent<congrats>().invis();
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
