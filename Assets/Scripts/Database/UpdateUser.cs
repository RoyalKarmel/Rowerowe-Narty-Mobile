using Firebase.Database;
using UnityEngine;

public class UpdateUser : MonoBehaviour
{
    public DatabaseManager databaseManager;
    private DatabaseReference dbReference;
    private string userID;

    void Start()
    {
        dbReference = databaseManager.GetDbReference();
        userID = databaseManager.GetUserID();
    }

    // Update username
    public void UpdateUserName(string newName)
    {
        dbReference.Child("users").OrderByChild("username").EqualTo(newName).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to check username availability: " + task.Exception);
                return;
            }

            if (task.Result.Exists)
            {
                Debug.LogError("Username already exists.");
                return;
            }

            dbReference.Child("users").Child(userID).Child("username").SetValueAsync(newName);
            Debug.Log("Updated username");
        });
    }

    // Update coins
    public void UpdateUserCoins(int coins)
    {
        dbReference.Child("users").Child(userID).Child("coins").SetValueAsync(coins);
        Debug.Log("Updated coins");
    }

    // Update best score
    public void UpdateUserScore(int best_score)
    {
        dbReference.Child("users").Child(userID).Child("best_score").SetValueAsync(best_score);
        Debug.Log("Updated best score");
    }
}
