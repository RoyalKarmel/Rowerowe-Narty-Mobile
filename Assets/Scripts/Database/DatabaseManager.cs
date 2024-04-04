using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;

public class DatabaseManager : MonoBehaviour
{
    Firebase.FirebaseApp app;
    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                app = Firebase.FirebaseApp.DefaultInstance;
                FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

                UserData newUser = UserData.CreateUser("test2");
                AddUserToDatabase(db, newUser);
            }
            else
            {
                Debug.LogError(string.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    void AddUserToDatabase(FirebaseFirestore db, UserData user)
    {
        CollectionReference usersRef = db.Collection("users");
        usersRef.AddAsync(user).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError($"Failed to add user to database: {task.Exception}");
            }
            else
            {
                DocumentReference newUserRef = task.Result;
                Debug.Log($"User added successfully with ID: {newUserRef.Id}");
            }
        });
    }
}
