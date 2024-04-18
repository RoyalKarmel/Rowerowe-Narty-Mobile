using System;
using System.Collections;
using Firebase.Database;
using UnityEngine;

public class UserInfoManager : MonoBehaviour
{
    public DatabaseManager databaseManager;
    private DatabaseReference dbReference;
    private string userID;

    void Start()
    {
        dbReference = databaseManager.GetDbReference();
        userID = databaseManager.GetUserID();
    }

    // Get user info from database
    public void GetUserInfo()
    {
        StartCoroutine(GetUserName((string name) =>
        {
            Debug.Log("Username:" + name);
        }));

        StartCoroutine(GetUserCoins((int coins) =>
        {
            Debug.Log("Coins:" + coins);
        }));

        StartCoroutine(GetUserScore((int score) =>
        {
            Debug.Log("Score:" + score);
        }));
    }

    // Get username
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

    // Get user coins
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

    // Get user best score
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
}
