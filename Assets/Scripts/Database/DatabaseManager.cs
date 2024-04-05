using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using System;
using System.Collections.Generic;

public class DatabaseManager : MonoBehaviour
{
    Firebase.FirebaseApp app;
    FirebaseFirestore database;
    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                app = Firebase.FirebaseApp.DefaultInstance;
                database = FirebaseFirestore.DefaultInstance;

                UserData testUser = UserData.CreateUser("test2");
                SaveUserDataToDatabase(testUser);
            }
            else
            {
                Debug.LogError(string.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    void SaveUserDataToDatabase(UserData user)
    {
        DocumentReference userDocRef = database.Collection("users").Document(user.id);
        Debug.Log($"user ref: {userDocRef}");

        userDocRef.SetAsync(user).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError($"Failed to add user to database: {task.Exception}");
            }
            else
            {
                Debug.Log($"User added successfully with ID: {user.id}");
            }
        });
    }
}
