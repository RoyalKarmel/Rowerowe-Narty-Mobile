using UnityEngine;
using Firebase.Database;

public class DatabaseManager : MonoBehaviour
{
    private string userID;
    private DatabaseReference dbReference;
    private bool userExists = true;

    void Awake()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    void Start()
    {
        CheckUserExistence();
    }

    #region Check User Existence
    public void CheckUserExistence()
    {
        dbReference.Child("users").Child(userID).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to check user existence: " + task.Exception);
                return;
            }
            if (!task.Result.Exists)
                userExists = false;
        });
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

    public bool GetUserExistence()
    {
        return userExists;
    }
    #endregion
}
