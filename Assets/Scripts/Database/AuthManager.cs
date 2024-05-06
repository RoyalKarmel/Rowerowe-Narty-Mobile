using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

public class AuthManager : MonoBehaviour
{
    public DatabaseManager databaseManager;
    private DatabaseReference dbReference;
    private FirebaseAuth auth;

    void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    void Start()
    {
        dbReference = databaseManager.GetDbReference();
    }

    // Check if user is logged in
    public bool IsUserLoggedIn()
    {
        if (auth.CurrentUser != null)
        {
            Debug.Log("User logged in");
            return true;
        }

        return false;
    }

    #region Login
    // Sign in with Google to Firebase
    public void SignInWithGoogle(string name)
    {
        if (auth == null)
        {
            Debug.Log("Firebase auth not initialized");
            return;
        }

        // Login to Firebase
        var credential = GoogleAuthProvider.GetCredential(null, null);

        // Sign in with the credential
        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("SignInWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("SignInWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }

            // Success
            FirebaseUser newUser = task.Result;
            Debug.Log("User signed in successfully: " + newUser.DisplayName + " (" + newUser.UserId + ")");

            CheckUserInDatabase(newUser.UserId, name);
        });
    }

    // Check user existence in database
    void CheckUserInDatabase(string userID, string name)
    {
        dbReference.Child("users").Child(userID).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Failed to check user existence in database: " + task.Exception);
                return;
            }

            if (!task.Result.Exists)
                CreateUserInDatabase(userID, name);
            else
                Debug.Log("User already exists in database.");
        });
    }

    // Create user in database
    void CreateUserInDatabase(string userID, string name)
    {
        User newUser = new User(name);
        string json = JsonUtility.ToJson(newUser);

        dbReference.Child("users").Child(userID).SetRawJsonValueAsync(json).ContinueWith(setTask =>
        {
            if (setTask.IsFaulted || setTask.IsCanceled)
            {
                Debug.LogError("Failed to create user in database: " + setTask.Exception);
                return;
            }

            Debug.Log("Created new user with ID: " + userID);
        });
    }
    #endregion

    #region Utils
    public void SignOut()
    {
        auth.SignOut();
        Debug.Log("User signed out successfully.");
    }
    #endregion
}
