using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/**
 * The Save class serves to create an object to be saved to playerPrefs
 */
public class Save
{
    public List<Player> profiles = new List<Player>();

    public int selectedProfile;

}