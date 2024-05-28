using Firebase.Database;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    #region Singleton

    public static DatabaseManager instance;

    public string deviceID { get; private set; }
    public DatabaseReference dbReference { get; private set; }
    public bool userExists { get; private set; }

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of Database Manager found!");
            return;
        }

        instance = this;

        deviceID = SystemInfo.deviceUniqueIdentifier;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        userExists = true;
    }

    #endregion

    void Start()
    {
        CheckUserExistence();
    }

    #region Check User Existence
    public void CheckUserExistence()
    {
        dbReference
            .Child("users")
            .Child(deviceID)
            .GetValueAsync()
            .ContinueWith(task =>
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
}
