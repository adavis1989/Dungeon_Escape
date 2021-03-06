using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager is Null!");
            }

            return _instance;
        }
    }

    public Text playerGemCountText;
    public Image selectionImg;
    public Player player;
    public Text gemCountText;
    public Image[] _lifeUnits;

    private void Awake()
    {
        _instance = this;
    }

    public void OpenShop(int gemCount)
    {
        playerGemCountText.text = "" + gemCount + "G";
    }

    public void UpdateShopSelection(int yPos)
    {
        selectionImg.rectTransform.anchoredPosition = new Vector2(selectionImg.rectTransform.anchoredPosition.x, yPos);
    }

    public void ItemBought(int update)
    {
        playerGemCountText.text = "" + player.diamonds + "G";
        gemCountText.text = "" + player.diamonds + "G";
    }

    public void UpdateGemCount(int count)
    {
        gemCountText.text = "" + count;
    }

    public void UpdateLives(int livesRemaining)
    {
        for (int i = 0; i <= livesRemaining; i++)
        {
            Debug.Log(i);

            //do nothing untill we hit the max
            if (i == livesRemaining)
            {
                _lifeUnits[i].enabled = false;
            }

        }

        //loop through lives
        //i == livesRemaining
        //hide that one
    }

}
