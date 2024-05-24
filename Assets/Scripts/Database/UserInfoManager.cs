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
        deviceID = DatabaseManager.instance.deviceID;
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

        if (userNameData != null && userNameData.Result.Exists)
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

        if (userCoinsData != null && userCoinsData.Result.Exists)
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

        if (userScoreData != null && userScoreData.Result.Exists)
        {
            DataSnapshot snapshot = userScoreData.Result;

            onCallback.Invoke(int.Parse(snapshot.Value.ToString()));
        }
    }

    // Get user items (skins or music) based on itemType (0 for skins, 1 for music)
    public IEnumerator GetUserItems(int itemType, Action<int[]> onCallback)
    {
        string itemPath = (itemType == 0) ? "skins" : "music";
        var userItemsData = dbReference
            .Child("users")
            .Child(deviceID)
            .Child(itemPath)
            .GetValueAsync();

        yield return new WaitUntil(predicate: () => userItemsData.IsCompleted);

        if (userItemsData != null)
        {
            DataSnapshot snapshot = userItemsData.Result;
            int[] items = new int[snapshot.ChildrenCount];
            int i = 0;
            foreach (var childSnapshot in snapshot.Children)
            {
                items[i] = int.Parse(childSnapshot.Value.ToString());
                i++;
            }
            onCallback.Invoke(items);
        }
    }
}
