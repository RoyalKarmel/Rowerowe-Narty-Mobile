using UnityEngine;
using Firebase.Database;

public class DatabaseManager : MonoBehaviour
{
    private string deviceID;
    private DatabaseReference dbReference;
    private bool userExists = true;

    void Awake()
    {
        deviceID = SystemInfo.deviceUniqueIdentifier;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    void Start()
    {
        CheckUserExistence();
    }

    #region Check User Existence
    public void CheckUserExistence()
    {
        dbReference.Child("users").Child(deviceID).GetValueAsync().ContinueWith(task =>
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

    public string GetDeviceID()
    {
        return deviceID;
    }

    public bool GetUserExistence()
    {
        return userExists;
    }
    #endregion
}
