using System;

[System.Serializable]
/**
 * The player object is used to group the data that needs to be saved for each
 * profile
 */
public class Player

{
    public string username;
    public string progress;
    public int[] stars;

    /**
     * The constructor for player defaults the variables while also setting the
     * profile name
     * @param name - The name for the profile
     */
    public Player(string name)
    {
        username = name;
        stars = new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        progress = string.Empty;
    }

}