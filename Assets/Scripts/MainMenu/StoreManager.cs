using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    [Header("Database")]
    public DatabaseManager databaseManager;
    public UpdateUser updateUser;
    public UserInfoManager userInfoManager;

    [Header("Items")]
    public List<ItemData> items;

    [Header("Keys")]
    private string coinsKey = "Coins";
    public string itemKey = "Item";
    public string selectedItemKey = "SelectedItemID";

    // private DatabaseReference dbReference;
    // private string userID;

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
        int coins = PlayerPrefs.GetInt(coinsKey, 0);

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

    #region TODO: Make this works
    // void Start()
    // {
    //     dbReference = databaseManager.GetDbReference();
    //     userID = databaseManager.GetUserID();

    //     UpdateStyles(itemKey);
    // }

    // void UpdateStyles(string category)
    // {
    //     foreach (ItemData item in items)
    //     {
    //         StartCoroutine(IsItemOwned(category, item.itemID, (bool isOwned) =>
    //         {
    //             if (isOwned)
    //                 item.buttonText.text = "SELECT";
    //             else
    //                 item.buttonText.text = "Buy: <color=#0F85E0>" + item.itemCost + "</color>";

    //             int selectedItem = PlayerPrefs.GetInt(selectedItemKey, 0);
    //             if (selectedItem == item.itemID)
    //                 item.buttonText.text = "SELECTED";
    //         }));
    //     }
    // }

    // public void BuyItem(int itemID, int itemCost, string itemType)
    // {
    //     StartCoroutine(userInfoManager.GetUserCoins((int playerCoins) =>
    //    {
    //        int coins = playerCoins;
    //        if (coins >= itemCost)
    //        {
    //            coins -= itemCost;
    //            PlayerPrefs.SetInt(coinsKey, coins);
    //            SetItemOwned(itemID, itemType);
    //            UpdateStyles(itemType);

    //            updateUser.UpdateUserCoins(coins);
    //        }
    //        else
    //            Debug.LogError("Not enough coins to buy the item.");
    //    }));
    // }

    // public void SelectItem(int itemID, string itemType)
    // {
    //     PlayerPrefs.SetInt(selectedItemKey, itemID);
    //     UpdateStyles(itemType);
    // }

    // #region Item Ownership
    // public IEnumerator IsItemOwned(string itemType, int itemID, Action<bool> onResult)
    // {
    //     var userItemsData = dbReference.Child("users").Child(userID).Child(itemType).GetValueAsync();

    //     yield return new WaitUntil(() => userItemsData.IsCompleted);

    //     if (userItemsData.Exception != null)
    //     {
    //         Debug.LogError("Failed to retrieve user items data: " + userItemsData.Exception);
    //         onResult?.Invoke(false);
    //         yield break;
    //     }

    //     DataSnapshot snapshot = userItemsData.Result;
    //     bool isOwned = snapshot.HasChild(itemID.ToString());
    //     onResult?.Invoke(isOwned);
    // }

    // void SetItemOwned(int itemID, string itemType)
    // {

    //     string key = itemType == "skins" ? "Skin" : "Music";
    //     PlayerPrefs.SetInt(key + itemID.ToString(), 1);

    //     updateUser.AddItem(itemID, itemType);
    // }
    // #endregion
    #endregion
}
