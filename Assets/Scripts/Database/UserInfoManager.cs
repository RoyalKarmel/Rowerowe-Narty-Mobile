using System;
using System.Collections;
using Firebase.Database;
using UnityEngine;

public class UserInfoManager : MonoBehaviour
{
    private DatabaseReference dbReference;
    private string deviceID;

    void Start()
    {
        dbReference = DatabaseManager.instance.dbReference;
    }

    // Get username
    public IEnumerator GetUserName(Action<string> onCallback)
    {
        var userNameData = dbReference
            .Child("users")
            .Child(deviceID)
            .Child("username")
            .GetValueAsync();

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
        var userCoinsData = dbReference
            .Child("users")
            .Child(deviceID)
            .Child("coins")
            .GetValueAsync();

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
        var userScoreData = dbReference
            .Child("users")
            .Child(deviceID)
            .Child("best_score")
            .GetValueAsync();

        yield return new WaitUntil(predicate: () => userScoreData.IsCompleted);

        if (userScoreData != null)
        {
            DataSnapshot snapshot = userScoreData.Result;

            onCallback.Invoke(int.Parse(snapshot.Value.ToString()));
        }
    }
}
