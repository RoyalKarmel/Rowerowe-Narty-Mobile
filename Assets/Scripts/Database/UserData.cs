using System;

[Serializable]
public class UserData
{
    public string id;
    public string name;
    public int best_score;
    public int coins;

    public static UserData CreateUser(string name)
    {
        UserData user = new UserData
        {
            id = Guid.NewGuid().ToString(),
            name = name,
            best_score = 0,
            coins = 0
        };
        return user;
    }
}