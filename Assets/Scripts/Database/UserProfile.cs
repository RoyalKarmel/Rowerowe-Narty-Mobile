using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class UserProfile : MonoBehaviour
{
    [Header("Database")]
    public UserInfoManager userInfoManager;
    public UpdateUser updateUser;

    [Header("Text values")]
    public TMP_Text usernameText;
    public TMP_Text scoreText;
    public TMP_Text coinsText;

    [Header("Input fields")]
    public TMP_InputField emailField;
    public TMP_InputField passwordField;
    public TMP_InputField nameField;
    public TMP_InputField newNameField;

    [Header("Player panels")]
    public GameObject createUser;
    public GameObject userProfile;

    [Header("Sign Out Button")]
    public GameObject signOutButton;

    void Start()
    {
        if (DatabaseManager.instance.IsUserLoggedIn())
            UserProfileActive();
        else
            CreateUserActive();
    }

    public void SignUp()
    {
        string email = emailField.text;
        string password = passwordField.text;
        string playerName = nameField.text;

        if (
            string.IsNullOrEmpty(email)
            || string.IsNullOrEmpty(password)
            || string.IsNullOrEmpty(playerName)
        )
        {
            Debug.LogError("Email, password and name fields cannot be empty!");
            return;
        }

        DatabaseManager.instance.SignUp(email, password, playerName);
    }

    // Create new user in database
    public void SignIn()
    {
        DatabaseManager.instance.SignIn(nameField.text, passwordField.text);
        UserProfileActive();
    }

    public void SignOut()
    {
        DatabaseManager.instance.SignOut();

        CreateUserActive();
    }

    #region Get Info
    // Get user info from database
    void GetUserInfo()
    {
        StartCoroutine(
            userInfoManager.GetUserName(
                (string name) =>
                {
                    usernameText.text = name;
                }
            )
        );

        StartCoroutine(
            userInfoManager.GetUserCoins(
                (int coins) =>
                {
                    coinsText.text = coins.ToString();
                }
            )
        );

        StartCoroutine(
            userInfoManager.GetUserScore(
                (int score) =>
                {
                    scoreText.text = score.ToString();
                }
            )
        );
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
        yield return StartCoroutine(
            updateUser.UpdateUserName(
                newUsername,
                (success) =>
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
                }
            )
        );
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
