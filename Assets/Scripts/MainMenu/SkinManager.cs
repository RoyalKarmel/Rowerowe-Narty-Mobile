using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public List<SkinData> skins;

    private string coinsKey = "Coins";
    private string skinKey = "Skin";
    private string selectedSkinKey = "SelectedSkinID";

    void Start()
    {
        UpdateStyles();
    }

    void UpdateStyles()
    {
        foreach (SkinData skin in skins)
        {
            if (IsSkinOwned(skin.skinID))
                skin.buttonText.text = "SELECT";
            else
                skin.buttonText.text = "Buy: <color=#0F85E0>" + skin.skinCost + "</color>";

            int selectedSkin = PlayerPrefs.GetInt(selectedSkinKey, 0);
            if (selectedSkin == skin.skinID)
                skin.buttonText.text = "SELECTED";
        }
    }

    public void BuySkin(int skinID, int skinCost)
    {
        int coins = PlayerPrefs.GetInt(coinsKey, 0);

        if (coins >= skinCost)
        {
            coins -= skinCost;
            PlayerPrefs.SetInt(coinsKey, coins);
            SetSkinOwned(skinID);
            UpdateStyles();
        }
    }

    public void SelectSkin(int skinID)
    {
        PlayerPrefs.SetInt(selectedSkinKey, skinID);
        UpdateStyles();
    }

    public bool IsSkinOwned(int skinID)
    {
        return PlayerPrefs.GetInt(skinKey + skinID.ToString(), 0) == 1;
    }

    void SetSkinOwned(int skinID)
    {
        PlayerPrefs.SetInt(skinKey + skinID.ToString(), 1);
    }
}
