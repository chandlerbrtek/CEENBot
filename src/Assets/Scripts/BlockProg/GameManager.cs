using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/*
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
    public GameObject startButton;
    GameObject[] buttons;
    private GameObject newBlock;
    private bool hasNewBlock;
    private int blockId;
    private int selectedProfile;
    private List<Player> profiles;

    public List<GameObject> blocks;
    /*
     * Intialization function
     */
    void Start()
    {
        buttons = new GameObject[4] { button1,button2,button3,button4};
        blockId = 0;
        blocks = new List<GameObject>();
        addBlock(startButton);
    }
    /*
     * creates and returns a new block with id i
     */
    public GameObject instantiateBlock(int i)
    {
        newBlock = Instantiate(buttons[i]);
        newBlock.transform.position = new Vector3(100, 100, 3);
        addBlock(newBlock);
        return newBlock;

    }

    /*
     * adds block b to the list of all blocks
     */
    public void addBlock(GameObject b)
    {
        Debug.Log("adding block" + blockId);
        b.GetComponent<BlockBehavior>().blockId = blockId;
        blockId++;
        blocks.Add(b);
        Debug.Log("Blocks size:" + blocks.Count);
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

            Debug.Log("Game Loaded");

        }
        else
        {
            Debug.Log("No game saved!");
        }
    }


}
