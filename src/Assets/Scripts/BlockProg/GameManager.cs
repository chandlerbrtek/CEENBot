using System.Collections;
using System.Collections.Generic;
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

    public string levelString;
    public GameObject levelManager;

    public List<GameObject> blocks;
    /*
     * Intialization function
     */

    void Awake()
    {

    }

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
        Debug.Log("BLocks size:" + blocks.Count);
        

    }
}
