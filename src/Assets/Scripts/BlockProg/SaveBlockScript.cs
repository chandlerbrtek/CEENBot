using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class creates a backup of the block program
 */
public class SaveBlockScript : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager gm;
    private string saveString;
    /**
     * iniatialize the game manager
     */
    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    /**
     * WHen clicked it iterates through the block program and saves each to file.
     */
    void OnMouseDown()
    {
        saveString = "";
        GameObject blockIter = gm.getStartBlock();
        while (blockIter.GetComponent<BlockBehavior>().hasNext)
        {
            blockIter = blockIter.GetComponent<BlockBehavior>().getNext();
            if (blockIter.name.Contains("Forward"))
            {
                saveString += "F";
            }
            if (blockIter.name.Contains("Back"))
            {
                saveString += "B";
            }
            if (blockIter.name.Contains("Left"))
            {
                saveString += "L";
            }
            if (blockIter.name.Contains("Right"))
            {
                saveString += "R";
            }
            if (blockIter.name.Contains("Beep"))
            {
                saveString += "X";
            }
            if (blockIter.name.Contains("Light"))
            {
                saveString += "Y";
            }
            if (blockIter.name.Contains("Restart"))
            {
                saveString += "Z";
            }
        }
        gm.LoadGame();
        //Debug.Log(gm.selectedProfile);
        gm.saveBlocks(saveString);
    }
    // Update is called once per frame

}
