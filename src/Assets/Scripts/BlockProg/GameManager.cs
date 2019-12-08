using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/**
 * Since blocks are doubly linked lists but there can be multiple lists
 * the game manager functions as an object in the scene that keeps track of
 * all of the individual blocks.
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
    public GameObject activeLevel;

    public List<GameObject> blocks;

    public bool running = false;
    public GameObject congrats;
    public GameObject trash;

    bool toggle = false;
    /**
     * Intialization function
     */

    void Awake()
    {

    }

    void Start()
    {
        height = new Vector3(0, 0.93f, 0);
        buttons = new GameObject[7] { button1,button2,button3,button4, button5, button6, button7};
        blockId = 0;
        blocks = new List<GameObject>();
        addBlock(startButton);
        Debug.Log("Loading level:" + activeLevel.GetComponent<activeLevel>().getLevel());
        levelManager.GetComponent<levelManager>().setLevel(activeLevel.GetComponent<activeLevel>().getLevel());
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

    public void SaveLevelScore(int levelIndex, int numOfStars)
    {
        Player player = profiles[selectedProfile];
        player.stars[levelIndex] = numOfStars;
        SaveGame();
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        save.selectedProfile = selectedProfile;
        save.profiles = profiles;

        return save;
    }

    public void moveBlocks(Vector3 offset)
    {
        foreach (GameObject go in blocks)
        {
            go.GetComponent<BlockBehavior>().move(offset);
        }
    }

    public void executeBlockProgram()
    {
        if (!running)
        {
            running = true;
            resetPosition.GetComponent<ReturnScript>().resetPosition();
            levelManager.GetComponent<levelManager>().reset();
            arrow.GetComponent<SpriteRenderer>().enabled = true;
            StartCoroutine(Wait());
            
        }
    }

    public void stopExecution()
    {
        if(running)
        {
            running = false;

        }
    }

    IEnumerator Wait()
    {
        GameObject curr = startButton;


        while (running)
        {

            
            if (curr.name == "ForwardBlock(Clone)")
            {

                levelManager.GetComponent<levelManager>().startDriving();
                levelManager.GetComponent<levelManager>().forward();
            }
            else if(curr.name == "BackBlock(Clone)")
            {
                levelManager.GetComponent<levelManager>().startDriving();
                levelManager.GetComponent<levelManager>().back();
            }
            else if (curr.name == "RightBlock(Clone)")
            {
                levelManager.GetComponent<levelManager>().startDriving();
                levelManager.GetComponent<levelManager>().right();
            }
            else if (curr.name == "LeftBlock(Clone)")
            {
                levelManager.GetComponent<levelManager>().startDriving();
                levelManager.GetComponent<levelManager>().left();
            }
            else if (curr.name == "BeepBlock(Clone)")
            {
                levelManager.GetComponent<levelManager>().stopDriving();
                levelManager.GetComponent<levelManager>().musicActivate();
            }
            else if (curr.name == "LightBlock(Clone)")
            {
                levelManager.GetComponent<levelManager>().stopDriving();
                levelManager.GetComponent<levelManager>().lightActivate();
            }
            else if (curr.name == "RestartBlock(Clone)")
            {
                while (curr.GetComponent<BlockBehavior>().hasPrev)
                {
                    curr = curr.GetComponent<BlockBehavior>().getPrevious();
                    moveBlocks(-height);
                }
  
            }
            
            yield return new WaitForSeconds(.5f);
            while(levelManager.GetComponent<levelManager>().isMoving)
            {
                yield return new WaitForSeconds(.1f);
            }
            if (levelManager.GetComponent<levelManager>().complete)
            {
                running = false;
                int stars = levelManager.GetComponent<levelManager>().getStars();
                activeLevel.GetComponent<activeLevel>().setStars(stars);
                congrats.GetComponent<congrats>().complete(stars);
                clear();
            }
            
            if (!running)
                break;
            moveBlocks(height);
            if (!curr.GetComponent<BlockBehavior>().hasNext)
            {
                levelManager.GetComponent<levelManager>().stopDriving();
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

    private void SaveGame()
    {
        // 1
        Save save = CreateSaveGameObject();

        // 2
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Game Saved");
    }

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

            Debug.Log("Game Loaded");

        }
        else
        {
            Debug.Log("No game saved!");
        }
    }

    public void saveBlocks(string toSave)
    {
        Debug.Log(selectedProfile);
        profiles[selectedProfile].progress = toSave;
        SaveGame();
    }

    public GameObject getStartBlock()
    {
        return startButton;
    }

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

    public void removeBlock(GameObject block)
    {
        blocks.Remove(block);
        Destroy(block);
    }

    public void removeBlock(int id)
    {
        foreach(GameObject g in blocks)
        {
            if(g.GetComponent<BlockBehavior>().blockId == id)
            {
                removeBlock(g);
            }
        }
    }

    public void toggleBlocks()
    {
        for(int i = 1; i<blocks.Count; ++i)
        {
            blocks[i].GetComponent<BlockBehavior>().toggle();
        }
        toggle = ! toggle;
    }
}
