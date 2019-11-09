using System;

[System.Serializable]
public class Player

{
    public string username = string.Empty;
    public int[] stars;

    public Player(string name)
    {
        username = name;
        stars = new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
    }

}