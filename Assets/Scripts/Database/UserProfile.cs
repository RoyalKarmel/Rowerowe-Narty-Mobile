using System.Collections;
using TMPro;
using UnityEngine;

public class UserProfile : MonoBehaviour
{
    [Header("Database")]
    public DatabaseManager databaseManager;
    public AuthManager authManager;
    public UserInfoManager userInfoManager;
    public UpdateUser updateUser;

    [Header("Text values")]
    public TMP_Text usernameText;
    public TMP_Text scoreText;
    public TMP_Text coinsText;
    public TMP_InputField newNameField;
    public TMP_InputField nameField;

    [Header("Player panels")]
    public GameObject createUser;
    public GameObject userProfile;

    [Header("Sign Out Button")]
    public GameObject signOutButton;

    void Start()
    {
        if (authManager.IsUserLoggedIn())
            UserProfileActive();
        else
            CreateUserActive();
    }

    // Create new user in database
    public void SignIn()
    {
        string playerName = nameField.text;
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.Log("Name field can not be empty!");
            return;
        }

        authManager.SignInWithGoogle(nameField.text);
        UserProfileActive();
    }

    public void SignOut()
    {
        authManager.SignOut();

        CreateUserActive();
    }

    #region Get Info
    // Get user info from database
    void GetUserInfo()
    {
        StartCoroutine(userInfoManager.GetUserName((string name) =>
        {
            usernameText.text = name;
        }));

        StartCoroutine(userInfoManager.GetUserCoins((int coins) =>
        {
            coinsText.text = coins.ToString();
        }));

        StartCoroutine(userInfoManager.GetUserScore((int score) =>
        {
            scoreText.text = score.ToString();
        }));
    }
    #endregion

    #region Update username
    public void UpdateUserName()
    {
        string newUsername = newNameField.text;
        if (string.IsNullOrEmpty(newUsername))
        {
            Debug.LogError("Username cannot be empty.");
            return;
        }

        StopAllCoroutines();

        StartCoroutine(UpdateUserNameCoroutine(newUsername));
    }

    IEnumerator UpdateUserNameCoroutine(string newUsername)
    {
        yield return StartCoroutine(updateUser.UpdateUserName(newUsername, (success) =>
        {
            if (success)
            {
                usernameText.text = newUsername;
                newNameField.text = "";
                Debug.Log("Updated username.");
            }
            else
            {
                Debug.LogError("Failed to update username.");
            }
        }));
    }
    #endregion

    #region Utils
    void CreateUserActive()
    {
        createUser.SetActive(true);

        userProfile.SetActive(false);
    }

    void UserProfileActive()
    {
        createUser.SetActive(false);

        userProfile.SetActive(true);
        signOutButton.SetActive(true);
        GetUserInfo();
    }
    #endregion
}
