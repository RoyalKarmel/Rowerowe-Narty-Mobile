using UnityEngine;
// using Firebase.Firestore;
// using Firebase.Extensions;

public class DatabaseManager : MonoBehaviour
{
    // void Start()
    // {
    //     Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
    //     {
    //         var dependencyStatus = task.Result;
    //         if (dependencyStatus == Firebase.DependencyStatus.Available)
    //         {
    //             Firebase.FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;

    //             FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

    //             UserData newUser = UserData.CreateUser("test2");
    //             AddUserToDatabase(db, newUser);
    //         }
    //         else
    //         {
    //             Debug.LogError(System.String.Format(
    //               "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
    //             // Firebase Unity SDK is not safe to use here.
    //         }
    //     });
    // }

    // void AddUserToDatabase(FirebaseFirestore db, UserData user)
    // {
    //     DocumentReference userRef = db.Collection("users").Document(user.id);
    //     userRef.SetAsync(user).ContinueWithOnMainThread(setTask =>
    //     {
    //         if (setTask.IsFaulted)
    //         {
    //             Debug.LogError($"Failed to add user to database: {setTask.Exception}");
    //         }
    //         else
    //         {
    //             Debug.Log("User added successfully.");
    //         }
    //     });
    // }
}
