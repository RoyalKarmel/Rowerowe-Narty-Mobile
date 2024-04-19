using TMPro;
using UnityEngine;

public class StorePage : MonoBehaviour
{
    public UserInfoManager userInfoManager;
    public TMP_Text coinsText;

    // private string coinsKey = "Coins";

    void Start()
    {
        // int coins = PlayerPrefs.GetInt(coinsKey, 0);
        StartCoroutine(userInfoManager.GetUserCoins((int coins) =>
        {
            coinsText.text = coins.ToString();
        }));
    }
}
