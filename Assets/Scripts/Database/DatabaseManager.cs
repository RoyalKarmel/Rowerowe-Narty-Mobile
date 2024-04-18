using UnityEngine;
using Firebase.Database;

public class DatabaseManager : MonoBehaviour
{
    private string userID;
    private DatabaseReference dbReference;

    void Awake()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    void Start()
    {
        CheckUserExistence();
    }

    #region CreateUser
    void CheckUserExistence()
    {
        dbReference.Child("users").Child(userID).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to check user existence: " + task.Exception);
                return;
            }
            if (!task.Result.Exists)
                CreateUser();
        });
    }

    // Create new user in database
    void CreateUser()
    {
        User newUser = new User(userID, "test");
        string json = JsonUtility.ToJson(newUser);

        dbReference.Child("users").Child(userID).SetRawJsonValueAsync(json);

        Debug.Log("Created new user");
    }
    #endregion

    #region Utils
    public DatabaseReference GetDbReference()
    {
        return dbReference;
    }

    public string GetUserID()
    {
        return userID;
    }
    #endregion
}
