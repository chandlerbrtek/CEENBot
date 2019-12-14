using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/**
 * This class functions as the behavior for the menu button
 */
public class menu : MonoBehaviour
{

    /**
     * When clicked it loads the main menu
     */
    void OnMouseDown()
    {
        SceneManager.LoadScene(0);

    }

 
}
