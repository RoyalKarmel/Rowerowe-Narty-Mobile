public class User
{
    public string id;
    public string username;
    public int best_score;
    public int coins;

    public User(string id, string name)
    {
        this.id = id;
        this.username = name;
        this.best_score = 0;
        this.coins = 0;
    }
}