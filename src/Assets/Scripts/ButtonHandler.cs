using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

/**
 * The ButtonHandler is called for a variety of button clicks on the main menu
 */
public class ButtonHandler : MonoBehaviour
{

    private int selectedProfile;
    private List<Player> profiles;

    /**
     * Start is called before the first frame update
     */
    void Start()
    {
     
    }

    /**
     * Update is called once per frame
     */
    void Update()
    {
        
    }

    /**
     * This function loads the game and then adds a new profile to the end of
     * the list. It knows what name to used based upon the text entered into
     * the field. Once the profile has been added, the game is promptly saved.
     */
    public void CreateNewProfile()
    {
        profiles = new List<Player>();
        string profileName = "Null";
        LoadGame(false);
        GameObject inputFieldGo = GameObject.Find("TextBox");
        InputField inputFieldCo = inputFieldGo.GetComponent<InputField>();
        profileName = inputFieldCo.text;
        inputFieldCo.text = "";
        Player player = new Player(profileName);
        profiles.Add(player);
        SaveGame();
    }

    /**
     * Acts as a helper function to the SaveGame function, creating a Save
     * object and then saving the current variables to that object, before
     * returning it
     */
    private Save CreateSaveGameObject()
    {
        Save save = new Save();


        save.selectedProfile = selectedProfile;
        save.profiles = profiles;

        return save;
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

        Debug.Log("Game Saved");
    }

    /**
     * LoadGame loads the game from playerPrefs, setting the repective variables
     * to the values loaded. If loadProfiles is set to true, the text is set for
     * the profiles that can be selected
     * @param loadGame true if loading profiles, false if not
     */
    public void LoadGame(bool loadProfiles)
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

            if (profiles == null)
            {
                profiles = new List<Player>();
            }
            Debug.Log("Game Loaded");

            if (loadProfiles)
            {
                GameObject inputFieldGo;
                Text inputFieldCo;

                for (int i = 0; i < 30; i++)
                {
                    if (profiles.Count > i)
                    {
                        GameObject.Find("Profile" + (i + 1)).SetActive(true);
                        inputFieldGo = GameObject.Find("Prof" + (i + 1) + "Text");
                        inputFieldCo = inputFieldGo.GetComponent<Text>();
                        inputFieldCo.text = "" + profiles[i].username;
                    }
                    else
                    {
                        if (GameObject.Find("Profile" + (i + 1)))
                        {
                            GameObject.Find("Profile" + (i + 1)).SetActive(false);
                        }
                    }
                }
            }

        }
        else
        {
            selectedProfile = 99;
            Debug.Log("No game saved!");
        }
    }

    /**
     * Sets the selectedProfile variable in the playerPrefs to the index of the
     * selected profile
     */
    public void SelectProfile(int profileIdx)
    {
        LoadGame(false);
        if (profileIdx < 30)
        {
            Debug.Log(profiles[profileIdx].stars[0]);
        }
        selectedProfile = profileIdx;
        SaveGame();
    }

    public void LoadStars()
    {
        Sprite none = Resources.Load<Sprite>("0star");
        Sprite one = Resources.Load<Sprite>("1star");
        Sprite two = Resources.Load<Sprite>("2star");
        Sprite three = Resources.Load<Sprite>("3star");

        LoadGame(false);
        if (selectedProfile == 99)
        {
        GameObject.Find("Lvl1Stars").GetComponent<Image>().sprite = none;
        GameObject.Find("Lvl2Stars").GetComponent<Image>().sprite = none;
        GameObject.Find("Lvl3Stars").GetComponent<Image>().sprite = none;
        GameObject.Find("Lvl4Stars").GetComponent<Image>().sprite = none;
        GameObject.Find("Lvl5Stars").GetComponent<Image>().sprite = none;
        GameObject.Find("Lvl6Stars").GetComponent<Image>().sprite = none;
        GameObject.Find("Lvl7Stars").GetComponent<Image>().sprite = none;
        GameObject.Find("Lvl8Stars").GetComponent<Image>().sprite = none;
        GameObject.Find("Lvl9Stars").GetComponent<Image>().sprite = none;
        GameObject.Find("Lvl10Stars").GetComponent<Image>().sprite = none;

        }
        else
        {
            //guess we have to actually load the profile!!!!!
            int[] stars = profiles[selectedProfile].stars;

            for (int i = 0; i < stars.Length; i++)
            {
                if (stars[i] == -1 || stars[i] == 0)
                {
                    GameObject.Find("Lvl" + (i + 1) + "Stars").GetComponent<Image>().sprite = none;
                } else if (stars[i] == 1)
                {
                    GameObject.Find("Lvl" + (i + 1) + "Stars").GetComponent<Image>().sprite = one;
                } else if (stars[i] == 2)
                {
                    GameObject.Find("Lvl" + (i + 1) + "Stars").GetComponent<Image>().sprite = two;
                } else if (stars[i] == 3)
                {
                    GameObject.Find("Lvl" + (i + 1) + "Stars").GetComponent<Image>().sprite = three;
                }
            }
        }
    }

    /**
     * Deletes the profile at the selected index in the PlayerPrefs
     */
    public void DeleteProfile(int profileIdx)
    {
        LoadGame(false);
        profiles.RemoveAt(profileIdx);
        SaveGame();
    }
}
