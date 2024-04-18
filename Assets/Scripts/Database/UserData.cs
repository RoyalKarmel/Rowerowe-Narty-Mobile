using System.Collections.Generic;

[System.Serializable]
public class User
{
    public string id;
    public string username;
    public int best_score;
    public int coins;
    public List<string> unlockedSkins;
    public List<string> unlockedMusic;
    public string activeSkin;
    public string activeMusic;

    public User(string id, string name)
    {
        this.id = id;
        this.username = name;
        this.best_score = 0;
        this.coins = 0;
        this.unlockedSkins = new List<string>();
        this.unlockedMusic = new List<string>();
        this.activeSkin = "default";
        this.activeMusic = "default";

        // Add default items
        this.unlockedSkins.Add("default");
        this.unlockedMusic.Add("default");
    }
}