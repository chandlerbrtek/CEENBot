using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**  
 * QuitOnClick is a simple class that exits the game on click   
 */

public class QuitOnClick : MonoBehaviour {
    /**  
    * Quits the game when prompted (on click of button)  
    */
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
