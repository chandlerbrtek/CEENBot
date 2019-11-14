using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{

    private int selectedProfile;
    private List<Player> profiles;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateNewProfile()
    {
        string profileName = "Null";
        Debug.Log("I'm about to load the game...");
        LoadGame(false);
        GameObject inputFieldGo = GameObject.Find("TextBox");
        InputField inputFieldCo = inputFieldGo.GetComponent<InputField>();
        profileName = inputFieldCo.text;
        Debug.Log(profileName);
        Debug.Log("Profiles before creation:");
        Debug.Log(profiles.ToString());
        Player player = new Player(profileName);
        profiles.Add(player);
        Debug.Log("Profiles after creation:");
        Debug.Log(profiles.ToString());
        Debug.Log(profiles.Count);
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
                inputFieldCo.text = "" + profiles[profiles.Count - 1].username;

                inputFieldGo = GameObject.Find("ProfTwoText");
                inputFieldCo = inputFieldGo.GetComponent<Text>();
                inputFieldCo.text = "" + profiles[profiles.Count - 2].username;

                inputFieldGo = GameObject.Find("ProfThreeText");
                inputFieldCo = inputFieldGo.GetComponent<Text>();
                inputFieldCo.text = "" + profiles[profiles.Count - 3].username;

                inputFieldGo = GameObject.Find("ProfFourText");
                inputFieldCo = inputFieldGo.GetComponent<Text>();
                inputFieldCo.text = "" + profiles[profiles.Count - 4].username;

                inputFieldGo = GameObject.Find("ProfFiveText");
                inputFieldCo = inputFieldGo.GetComponent<Text>();
                inputFieldCo.text = "" + profiles[profiles.Count - 5].username;
            }

        }
        else
        {
            selectedProfile = 0;
            Debug.Log("No game saved!");
        }
    }
}
