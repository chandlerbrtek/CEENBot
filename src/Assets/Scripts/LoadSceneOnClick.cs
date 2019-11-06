using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**  
 * LoadSceneOnClick is a simple class that that switches the scene (or level) to the index given   
 */

public class LoadSceneOnClick : MonoBehaviour {

 /**  
 * switches to desired scene as indicated by index
 * @param sceneIndex    the index of the desired scene
 */
    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
