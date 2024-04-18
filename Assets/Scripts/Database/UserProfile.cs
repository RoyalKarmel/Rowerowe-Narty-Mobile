using System.Collections;
using TMPro;
using UnityEngine;

public class UserProfile : MonoBehaviour
{
    public UserInfoManager userInfoManager;
    public UpdateUser updateUser;

    [Header("Text values")]
    public TMP_Text usernameText;
    public TMP_Text scoreText;
    public TMP_Text coinsText;
    public TMP_InputField newNameField;

    void Start()
    {
        GetUserInfo();
    }

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
}
