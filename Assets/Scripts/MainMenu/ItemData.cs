using TMPro;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public int itemID;
    public int itemCost;
    public string itemName;

    public TMP_Text buttonText;
    public StoreManager storeManager;

    public void ClickSkin()
    {
        if (!storeManager.IsItemOwned(itemID))
            storeManager.BuyItem(itemID, itemCost);
        else
            storeManager.SelectItem(itemID);
    }

    public void ClickMusic()
    {
        if (!storeManager.IsItemOwned(itemID))
            storeManager.BuyItem(itemID, itemCost);
        else
            storeManager.SelectItem(itemID);
    }

    #region TODO: Make this works
    // public void ClickSkin()
    // {
    //     StartCoroutine(storeManager.IsItemOwned("skins", itemID, (bool isOwned) =>
    //     {
    //         if (!isOwned)
    //             storeManager.BuyItem(itemID, itemCost, "skins");
    //         else
    //             storeManager.SelectItem(itemID, "skins");
    //     }));
    // }

    // public void ClickMusic()
    // {
    //     StartCoroutine(storeManager.IsItemOwned("musics", itemID, (bool isOwned) =>
    //     {
    //         if (!isOwned)
    //             storeManager.BuyItem(itemID, itemCost, "musics");
    //         else
    //             storeManager.SelectItem(itemID, "musics");
    //     }));
    // }
    #endregion
}
