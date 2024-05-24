using System;
using System.Collections;
using Firebase.Database;
using UnityEngine;

public class UpdateUser : MonoBehaviour
{
    private DatabaseReference dbReference;
    private string deviceID;

    void Start()
    {
        dbReference = DatabaseManager.instance.dbReference;
        deviceID = DatabaseManager.instance.deviceID;
    }

    // Update username
    public IEnumerator UpdateUserName(string newName, Action<bool> onUpdateComplete)
    {
        // Check accesibility of new username
        var checkUsernameTask = dbReference
            .Child("users")
            .OrderByChild("username")
            .EqualTo(newName)
            .GetValueAsync();
        yield return new WaitUntil(() => checkUsernameTask.IsCompleted);

        // Error handler
        if (checkUsernameTask.IsFaulted)
        {
            Debug.LogError("Failed to check username availability: " + checkUsernameTask.Exception);
            onUpdateComplete?.Invoke(false);
            yield break;
        }

        // Check if new username exists
        foreach (var childSnapshot in checkUsernameTask.Result.Children)
        {
            string existingUserID = childSnapshot.Child("id").Value.ToString();
            if (existingUserID == deviceID)
            {
                Debug.LogError("Username already exists.");
                onUpdateComplete?.Invoke(false);
                yield break;
            }
        }

        // Update username in database
        var updateUsernameTask = dbReference
            .Child("users")
            .Child(deviceID)
            .Child("username")
            .SetValueAsync(newName);
        yield return new WaitUntil(() => updateUsernameTask.IsCompleted);

        // Error handler
        if (updateUsernameTask.IsFaulted)
        {
            Debug.LogError("Failed to update username: " + updateUsernameTask.Exception);
            onUpdateComplete?.Invoke(false);
            yield break;
        }

        Debug.Log("Updated username to: " + newName);
        onUpdateComplete?.Invoke(true);
    }

    // Update coins
    public void UpdateUserCoins(int coins)
    {
        dbReference.Child("users").Child(deviceID).Child("coins").SetValueAsync(coins);
        Debug.Log("Updated coins");
    }

    // Update best score
    public void UpdateUserScore(int best_score)
    {
        dbReference.Child("users").Child(deviceID).Child("best_score").SetValueAsync(best_score);
        Debug.Log("Updated best score");
    }

    // Add item
    public void AddItem(int itemID, string category)
    {
        dbReference
            .Child("users")
            .Child(deviceID)
            .Child(category)
            .Child(itemID.ToString())
            .SetValueAsync(1);
        Debug.Log("Added " + category + " with ID: " + itemID);
    }
}
