using System;
using System.Collections;
using Firebase.Database;
using TMPro;
using UnityEngine;

public class UserProfile : MonoBehaviour
{
    [Header("Database")]
    public DatabaseManager databaseManager;
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
    public GameObject profile;

    private DatabaseReference dbReference;
    private string userID;

    void Start()
    {
        dbReference = databaseManager.GetDbReference();
        userID = databaseManager.GetUserID();

        if (databaseManager.GetUserExistence())
            ProfileActive();
        else
            CreateUserActive();

        // CheckUserExistence((bool exists) =>
        // {
        //     if (exists)
        //         ProfileActive();
        //     else
        //         CreateUserActive();
        // });
    }

    #region Create User
    // Create new user in database
    public void CreateUser()
    {
        User newUser = new User(userID, nameField.text);
        string json = JsonUtility.ToJson(newUser);

        dbReference.Child("users").Child(userID).SetRawJsonValueAsync(json);

        ProfileActive();
        Debug.Log("Created new user");
    }
    #endregion

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

    #region Uitls
    void CreateUserActive()
    {
        createUser.SetActive(true);

        profile.SetActive(false);
    }

    void ProfileActive()
    {
        createUser.SetActive(false);

        profile.SetActive(true);
        GetUserInfo();
    }
    #endregion
}
