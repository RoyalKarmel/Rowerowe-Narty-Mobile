using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public DatabaseManager databaseManager;
    public RowUI rowUI;
    private DatabaseReference dbReference;

    void Start()
    {
        dbReference = databaseManager.GetDbReference();

        StartCoroutine(GetUsersFromDatabase());
    }

    // Get users from database & sort them by best score
    IEnumerator GetUsersFromDatabase()
    {
        Debug.Log("Fetching users from the database...");

        var task = dbReference.Child("users").GetValueAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            Debug.LogError("Failed to get user data: " + task.Exception);
            yield break;
        }

        List<User> users = new List<User>();

        foreach (DataSnapshot userSnapshot in task.Result.Children)
        {
            string userDataJson = userSnapshot.GetRawJsonValue();
            User user = JsonUtility.FromJson<User>(userDataJson);

            if (user != null && user.best_score > 0)
                users.Add(user);
        }

        // Sort users by best score
        users.Sort((a, b) => b.best_score.CompareTo(a.best_score));

        CreateRanking(users);
    }

    // Create ranking with sorted users
    void CreateRanking(List<User> users)
    {
        for (int i = 0; i < users.Count; i++)
        {
            var row = Instantiate(rowUI, transform).GetComponent<RowUI>();
            row.rank.text = (i + 1).ToString() + ".";
            row.username.text = users[i].username;
            row.best_score.text = users[i].best_score.ToString();
        }
    }
}
