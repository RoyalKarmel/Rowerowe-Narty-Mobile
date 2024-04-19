using TMPro;
using UnityEngine;

public class StorePage : MonoBehaviour
{
    public DatabaseManager databaseManager;
    public UserInfoManager userInfoManager;
    public TMP_Text coinsText;

    private string coinsKey = "Coins";

    void Start()
    {
        if (databaseManager.GetUserExistence())
        {
            StartCoroutine(userInfoManager.GetUserCoins((int userCoins) =>
            {
                coinsText.text = userCoins.ToString();
            }));
        }
        else
        {
            int coins = PlayerPrefs.GetInt(coinsKey, 0);
            coinsText.text = coins.ToString();
        }
    }
}
