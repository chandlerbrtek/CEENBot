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
                GameObject inputFieldGo = GameObject.Find("ProfOneText");
                Text inputFieldCo = inputFieldGo.GetComponent<Text>();
                inputFieldCo.text = "" + profiles[0].username;

                inputFieldGo = GameObject.Find("ProfTwoText");
                inputFieldCo = inputFieldGo.GetComponent<Text>();
                inputFieldCo.text = "" + profiles[1].username;

                inputFieldGo = GameObject.Find("ProfThreeText");
                inputFieldCo = inputFieldGo.GetComponent<Text>();
                inputFieldCo.text = "" + profiles[2].username;

                inputFieldGo = GameObject.Find("ProfFourText");
                inputFieldCo = inputFieldGo.GetComponent<Text>();
                inputFieldCo.text = "" + profiles[3].username;

                inputFieldGo = GameObject.Find("ProfFiveText");
                inputFieldCo = inputFieldGo.GetComponent<Text>();
                inputFieldCo.text = "" + profiles[4].username;
            }

        }
        else
        {
            selectedProfile = 0;
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
            selectedProfile = profileIdx;
        }
        SaveGame();
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
