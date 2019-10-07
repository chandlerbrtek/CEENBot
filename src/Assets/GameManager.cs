using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public List<GameObject> blocks;
    void Start()
    {
        buttons = new GameObject[4] { button1,button2,button3,button4};
        blockId = 0;
        blocks = new List<GameObject>();
        addBlock(startButton);
    }

    public GameObject instantiateBlock(int i)
    {
        newBlock = Instantiate(buttons[i]);
        newBlock.transform.position = new Vector3(100, 100, 3);
        addBlock(newBlock);
        return newBlock;

    }

    public void addBlock(GameObject b)
    {
        Debug.Log("adding block" + blockId);
        b.GetComponent<BlockBehavior>().blockId = blockId;
        blockId++;
        blocks.Add(b);
        Debug.Log("BLocks size:" + blocks.Count);
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
