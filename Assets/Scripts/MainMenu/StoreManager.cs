using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    [Header("Database")]
    public UpdateUser updateUser;
    public UserInfoManager userInfoManager;

    [Header("Items")]
    public List<ItemData> items;

    [Header("Keys")]
    private int coins;
    private string coinsKey = "Coins";
    public string itemKey = "Item";
    public string selectedItemKey = "SelectedItemID";

    void Start()
    {
        UpdateStyles();
    }

    void UpdateStyles()
    {
        foreach (ItemData item in items)
        {
            if (IsItemOwned(item.itemID))
                item.buttonText.text = "SELECT";
            else
                item.buttonText.text = "Buy: <color=#0F85E0>" + item.itemCost + "</color>";

            int selectedItem = PlayerPrefs.GetInt(selectedItemKey, 0);
            if (selectedItem == item.itemID)
                item.buttonText.text = "SELECTED";
        }
    }

    public void BuyItem(int itemID, int itemCost)
    {
        // Get coins from player prefs or database
        if (DatabaseManager.instance.IsUserLoggedIn())
        {
            StartCoroutine(
                userInfoManager.GetUserCoins(
                    (int userCoins) =>
                    {
                        coins = userCoins;
                    }
                )
            );
        }
        else
            coins = PlayerPrefs.GetInt(coinsKey, 0);

        if (coins < itemCost)
        {
            Debug.LogError("Not enough coins!");
            return;
        }

        coins -= itemCost;
        PlayerPrefs.SetInt(coinsKey, coins);
        SetItemOwned(itemID);
        UpdateStyles();
    }

    public void SelectItem(int itemID)
    {
        PlayerPrefs.SetInt(selectedItemKey, itemID);
        UpdateStyles();
    }

    public bool IsItemOwned(int itemID)
    {
        return PlayerPrefs.GetInt(itemKey + itemID.ToString(), 0) == 1;
    }

    void SetItemOwned(int itemID)
    {
        PlayerPrefs.SetInt(itemKey + itemID.ToString(), 1);
    }
}
