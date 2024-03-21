using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkinData : MonoBehaviour
{
    public int skinID;
    public int skinCost;

    public TMP_Text buttonText;
    public SkinManager skinManager;

    public void ClickSkin()
    {
        if (!skinManager.IsSkinOwned(skinID))
            skinManager.BuySkin(skinID, skinCost);
        else
            skinManager.SelectSkin(skinID);
    }
}
