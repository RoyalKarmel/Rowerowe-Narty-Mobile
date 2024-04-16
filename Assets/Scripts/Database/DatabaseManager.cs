using UnityEngine;
using Firebase.Database;
using System.Collections;
using System;

public class DatabaseManager : MonoBehaviour
{
    private string userID;
    private DatabaseReference dbReference;

    void Start()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Create new user in database
    public void CreateUser()
    {
        User newUser = new User(userID, "test1");
        string json = JsonUtility.ToJson(newUser);

        dbReference.Child("users").Child(userID).SetRawJsonValueAsync(json);

        Debug.Log("Created new user");
    }

    #region UpdateUserInfo
    // Update username
    public void UpdateUserName()
    {
        dbReference.Child("users").Child(userID).Child("username").SetValueAsync("newName");
        Debug.Log("Updated username");
    }

    // Update coins
    public void UpdateUserCoins()
    {
        dbReference.Child("users").Child(userID).Child("coins").SetValueAsync(1);
        Debug.Log("Updated coins");
    }

    // Update best score
    public void UpdateUserScore()
    {
        dbReference.Child("users").Child(userID).Child("best_score").SetValueAsync(1);
        Debug.Log("Updated best score");
    }
    #endregion

    #region GetUserInfo
    // Get user info from database
    public void GetUserInfo()
    {
        StartCoroutine(GetUserName((string name) =>
        {
            Debug.Log("name:" + name);
        }));

        StartCoroutine(GetUserCoins((int coins) =>
        {
            Debug.Log("coins:" + coins);
        }));

        StartCoroutine(GetUserScore((int score) =>
        {
            Debug.Log("score:" + score);
        }));
    }

    // IEnumerators
    public IEnumerator GetUserName(Action<string> onCallback)
    {
        var userNameData = dbReference.Child("users").Child(userID).Child("username").GetValueAsync();

        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);

        if (userNameData != null)
        {
            DataSnapshot snapshot = userNameData.Result;

            onCallback.Invoke(snapshot.Value.ToString());
        }
    }

    public IEnumerator GetUserCoins(Action<int> onCallback)
    {
        var userCoinsData = dbReference.Child("users").Child(userID).Child("coins").GetValueAsync();

        yield return new WaitUntil(predicate: () => userCoinsData.IsCompleted);

        if (userCoinsData != null)
        {
            DataSnapshot snapshot = userCoinsData.Result;

            onCallback.Invoke(int.Parse(snapshot.Value.ToString()));
        }
    }

    public IEnumerator GetUserScore(Action<int> onCallback)
    {
        var userScoreData = dbReference.Child("users").Child(userID).Child("best_score").GetValueAsync();

        yield return new WaitUntil(predicate: () => userScoreData.IsCompleted);

        if (userScoreData != null)
        {
            DataSnapshot snapshot = userScoreData.Result;

            onCallback.Invoke(int.Parse(snapshot.Value.ToString()));
        }
    }
    #endregion
}
