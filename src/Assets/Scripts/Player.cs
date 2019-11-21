using System;

[System.Serializable]
public class Player

{
    public string username;
    public string progress;
    public int[] stars;

    public Player(string name)
    {
        username = name;
        stars = new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        progress = string.Empty;
    }

}