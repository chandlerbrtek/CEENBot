using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/**
 * Since blocks are doubly linked lists but there can be multiple lists
 * the game manager functions as an object in the scene that keeps track of
 * all of the individual blocks. In addition to that it manages the level object
 * as well as the player profile data.
 */
public class GameManager : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject button6;
    public GameObject button7;

    public GameObject resetPosition;
    public GameObject startButton;
    public GameObject arrow;
    GameObject[] buttons;
    private GameObject newBlock;
    private bool hasNewBlock;
    private int blockId;

    private Vector3 height;
    private int selectedProfile;
    private List<Player> profiles;
    public string blocksave;

    public string levelString;
    public GameObject levelManager;
    private levelManager lm;
    public GameObject activeLevel;

    public List<GameObject> blocks;

    public bool running = false;
    public GameObject congrats;
    public GameObject trash;

    bool toggle = false;

    /**
     * initialize variables and set the level
     */
    void Start()
    {
        height = new Vector3(0, 0.93f, 0);
        buttons = new GameObject[7] { button1,button2,button3,button4, button5, button6, button7};
        blockId = 0;
        blocks = new List<GameObject>();
        addBlock(startButton);
        lm = levelManager.GetComponent<levelManager>();
        lm.setLevel(activeLevel.GetComponent<activeLevel>().getLevel());
    }
    /**
     * creates and returns a new block with id i
     */
    public GameObject instantiateBlock(int i)
    {
        newBlock = Instantiate(buttons[i]);
        newBlock.transform.position = new Vector3(100, 100, 3);
        addBlock(newBlock);
        return newBlock;

    }

    /**
     * adds block b to the list of all blocks
     */
    public void addBlock(GameObject b)
    {
        if(toggle)
        {
            b.GetComponent<BlockBehavior>().toggle();
        }
        b.GetComponent<BlockBehavior>().blockId = blockId;
        blockId++;
        blocks.Add(b);
    }

    /**
     * Saves the level's score into the player profile
     */
    public void SaveLevelScore(int levelIndex, int numOfStars)
    {
        LoadGame();
        Player player = profiles[selectedProfile];
        player.stars[levelIndex - 1] = numOfStars;
        SaveGame();
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        save.selectedProfile = selectedProfile;
        save.profiles = profiles;

        return save;
    }

    /**
     *A function that moves all blocks by an offset
     * @param offset the distance and direction to move the objects
     */
    public void moveBlocks(Vector3 offset)
    {
        foreach (GameObject go in blocks)
        {
            go.GetComponent<BlockBehavior>().move(offset);
        }
    }

    /**
     * This function is called by the start button, if the program is not allready running this resets the level and calls the coroutine that executes the program.
     */
    public void executeBlockProgram()
    {
        if (!running)
        {
            running = true;
            resetPosition.GetComponent<ReturnScript>().resetPosition();
            levelManager.GetComponent<levelManager>().reset();
            arrow.GetComponent<SpriteRenderer>().enabled = true;
            StartCoroutine(Run());
            
        }
    }

    /**
     * Stops the current program from execution.
     */
    public void stopExecution()
    {
        if(running)
        {
            running = false;

        }
    }

    /**
     * This program runs the block programming code. 
     * It's set up as a coroutine so that it can wait for other parts of the game to catch up.
     * While running it executes the current block by calling the appropriate methods in level manager, 
     * waits for a short period of time, gets the next command, and breaks the loop if the level was completed.
     */
    IEnumerator Run()
    {
        GameObject curr = startButton;


        while (running)
        {

            
            if (curr.name == "ForwardBlock(Clone)")
            {
                lm.addMove();
                lm.startDriving();
                lm.forward();
            }
            else if(curr.name == "BackBlock(Clone)")
            {
                lm.addMove();
                lm.startDriving();
                lm.back();
            }
            else if (curr.name == "RightBlock(Clone)")
            {
                lm.addMove();
                lm.startDriving();
                lm.right();
            }
            else if (curr.name == "LeftBlock(Clone)")
            {
                lm.addMove();
                lm.startDriving();
                lm.left();
            }
            else if (curr.name == "BeepBlock(Clone)")
            {
                lm.addMove();
                lm.stopDriving();
                lm.musicActivate();
            }
            else if (curr.name == "LightBlock(Clone)")
            {
                lm.addMove();
                lm.stopDriving();
                lm.lightActivate();
            }
            else if (curr.name == "RestartBlock(Clone)")
            {
                lm.addMove();
                while (curr.GetComponent<BlockBehavior>().hasPrev)
                {
                    curr = curr.GetComponent<BlockBehavior>().getPrevious();
                    moveBlocks(-height);
                }
  
            }
            
            yield return new WaitForSeconds(.5f);
            while(lm.isMoving)
            {
                yield return new WaitForSeconds(.1f);
            }
            if (lm.complete)
            {
                running = false;
                int stars = lm.getStars();
                activeLevel.GetComponent<activeLevel>().setStars(stars);
                if (activeLevel.GetComponent<activeLevel>().getLevel() > 0)
                {
                    SaveLevelScore(activeLevel.GetComponent<activeLevel>().getLevel(), stars);
                }
               // Debug.Log("complete");
                congrats.GetComponent<congrats>().complete(stars);
                clear();
            }
            
            if (!running)
                break;
            moveBlocks(height);
            if (!curr.GetComponent<BlockBehavior>().hasNext)
            {
                lm.stopDriving();
                break;
            }
            curr = curr.GetComponent<BlockBehavior>().getNext();
            
        }
        //yield return new WaitForSeconds(1);
        arrow.GetComponent<SpriteRenderer>().enabled = false;
        resetPosition.GetComponent<ReturnScript>().resetPosition();
        //
        running = false;
        
    }

    /**
     * Creates a save object and then saves that data to the PlayerPrefs
     */
    private void SaveGame()
    {
        // 1
        Save save = CreateSaveGameObject();

        // 2
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        //Debug.Log("Game Saved");
    }


    /**
     * LoadGame loads the game from playerPrefs, setting the repective variables
     * to the values loaded.
     */
    public void LoadGame()
    {
        // 1
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            // 2
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            selectedProfile = save.selectedProfile;
            profiles = save.profiles;
            blocksave = save.profiles[selectedProfile].progress;

            //Debug.Log("Game Loaded");

        }
        else
        {
            //Debug.Log("No game saved!");
        }
    }

    /**
     * Saves the current laid blocks to the players profile
     */
    public void saveBlocks(string toSave)
    {
        //Debug.Log(selectedProfile);
        profiles[selectedProfile].progress = toSave;
        SaveGame();
    }

    /**
     * Gets the starting block of the LinkedList
     */
    public GameObject getStartBlock()
    {
        return startButton;
    }
    /**
     * This function deletes all programming blocks.
     */
    public void clear()
    {
        GameObject g;
        blocks[0].GetComponent<BlockBehavior>().removeChild();
        while(blocks.Count>1)
        {
            g = blocks[1];
            removeBlock(g);
        }
       
    }
    /**
     * This function removes a specific block.
     */
    public void removeBlock(GameObject block)
    {
        blocks.Remove(block);
        Destroy(block);
    }

    /**
     * This function iterates through all blocks to toggle their sprites between text and icons
     */
    public void toggleBlocks()
    {
        for(int i = 1; i<blocks.Count; ++i)
        {
            blocks[i].GetComponent<BlockBehavior>().toggle();
        }
        toggle = ! toggle;
    }
}
