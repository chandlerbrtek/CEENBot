using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickToContinue : MonoBehaviour {

    public GameObject titleScreen;
    public GameObject MainMenuPanel;
    public GameObject pickup;
    public GameObject bhouse;
    public GameObject rhouse;
    public GameObject tree;
    // Use this for initialization
    void Start () {

        
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
        {
            titleScreen.SetActive(false);
            pickup.SetActive(false);
            bhouse.SetActive(false);
            rhouse.SetActive(false);
            tree.SetActive(false);
            MainMenuPanel.SetActive(true);

        }
	}
}
