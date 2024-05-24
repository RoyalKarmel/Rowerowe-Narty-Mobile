using System.Collections.Generic;

[System.Serializable]
public class User
{
    public string email;
    public string username;
    public int best_score;
    public int coins;

    public User(string email, string name)
    {
        this.email = email;
        username = name;
        best_score = 0;
        coins = 0;
    }
}
