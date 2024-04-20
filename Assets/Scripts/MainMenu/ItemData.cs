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
}
