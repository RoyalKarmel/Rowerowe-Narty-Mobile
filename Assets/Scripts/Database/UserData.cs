[System.Serializable]
public class User
{
    public string authID;
    public string username;
    public int best_score;
    public int coins;

    public User(string id, string name)
    {
        authID = id;
        username = name;
        best_score = 0;
        coins = 0;
    }
}
