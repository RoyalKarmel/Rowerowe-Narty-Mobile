using System.Collections.Generic;

[System.Serializable]
public class User
{
    public string authID;
    public string username;
    public int best_score;
    public int coins;
    public List<int> skins;
    public List<int> musics;
    public int activeSkinID;
    public int activeMusicID;

    public User(string id, string name)
    {
        this.authID = id;
        this.username = name;
        this.best_score = 0;
        this.coins = 0;
        this.skins = new List<int>();
        this.musics = new List<int>();
        this.activeSkinID = 0;
        this.activeMusicID = 0;

        // Add default items
        for (int i = 0; i <= 3; i++)
        {
            // Add skin IDs
            if (i == 0)
                this.skins.Add(1);
            else
                this.skins.Add(0);

            // Add music IDs
            if (i == 0)
                this.musics.Add(1);
            else
                this.musics.Add(0);
        }
    }
}