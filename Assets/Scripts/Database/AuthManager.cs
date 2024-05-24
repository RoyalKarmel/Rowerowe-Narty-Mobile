using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

public class AuthManager : MonoBehaviour
{
    #region Singleton

    public static AuthManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    private DatabaseReference dbReference;
    private FirebaseAuth auth;
    private string deviceID;

    void Start()
    {
        dbReference = DatabaseManager.instance.dbReference;
        deviceID = DatabaseManager.instance.deviceID;
        auth = FirebaseAuth.DefaultInstance;
    }

    // Register user in Firebase
    public void RegisterUser(string name)
    {
        RegisterUserAsync()
            .ContinueWith(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.LogError("Failed to register user: " + task.Exception);
                    return;
                }

                FirebaseUser newUser = task.Result;
                CreateUserInDatabase(newUser.UserId, deviceID, name);
            });
    }

    private Task<FirebaseUser> RegisterUserAsync()
    {
        return auth.SignInAnonymouslyAsync()
            .ContinueWith(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.LogError("Failed to sign in anonymously: " + task.Exception);
                    return null;
                }

                return task.Result.User;
            });
    }

    // Create user in database
    private void CreateUserInDatabase(string authID, string deviceID, string name)
    {
        User newUser = new User(authID, name);
        string json = JsonUtility.ToJson(newUser);

        dbReference
            .Child("users")
            .Child(deviceID)
            .SetRawJsonValueAsync(json)
            .ContinueWith(setTask =>
            {
                if (setTask.IsFaulted || setTask.IsCanceled)
                {
                    Debug.LogError("Failed to create user in database: " + setTask.Exception);
                    return;
                }

                Debug.Log("Created new user with ID: " + deviceID);
            });
    }
}
