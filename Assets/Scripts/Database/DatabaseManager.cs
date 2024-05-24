using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    #region Singleton
    public static DatabaseManager instance;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public DatabaseReference dbReference { get; private set; }
    private FirebaseAuth auth;

    void Start()
    {
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        auth = FirebaseAuth.DefaultInstance;
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
    // Sign Up
    public void SignUp(string email, string password, string name)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password)
            .ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("SignUp was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SignUp encountered an error: " + task.Exception);
                    return;
                }

                FirebaseUser newUser = task.Result.User;
                Debug.LogFormat(
                    "User signed up successfully: {0} ({1})",
                    newUser.DisplayName,
                    newUser.UserId
                );

                CheckUserInDatabase(newUser.UserId, email, name);
                SignIn(email, password);
            });
    }

    // Sign In
    public void SignIn(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password)
            .ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("SignIp was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SignIp encountered an error: " + task.Exception);
                    return;
                }

                FirebaseUser user = task.Result.User;
                Debug.LogFormat(
                    "User signed in successfully: {0} ({1})",
                    user.DisplayName,
                    user.UserId
                );
            });
    }

    // Sign Out
    public void SignOut()
    {
        auth.SignOut();
        Debug.Log("User signed out successfully.");
    }
    #endregion

    #region User in database
    // Check if user exists in database
    void CheckUserInDatabase(string userID, string email, string name)
    {
        dbReference
            .Child("users")
            .Child(userID)
            .GetValueAsync()
            .ContinueWith(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.LogError("Failed to check user existence in database: " + task.Exception);
                    return;
                }

                if (!task.Result.Exists)
                    CreateUserInDatabase(userID, email, name);
                else
                    Debug.Log("User already exists in database.");
            });
    }

    // Create user in database
    void CreateUserInDatabase(string userID, string email, string name)
    {
        User newUser = new User(email, name);
        string json = JsonUtility.ToJson(newUser);

        dbReference
            .Child("users")
            .Child(userID)
            .SetRawJsonValueAsync(json)
            .ContinueWith(setTask =>
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
}
