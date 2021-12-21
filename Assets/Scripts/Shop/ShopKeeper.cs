using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    public GameObject shopPanel;
    private int _itemSelected;
    private int _itemSelectedCost;

    private Player _player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _player = other.GetComponent<Player>();
            if (_player != null)
            {
                UIManager.Instance.OpenShop(_player.diamonds);
            }
            shopPanel.SetActive(true);
            AdsManager.Instance.LoadAd();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }

    public void SelectItme(int item)
    {
        //0 = flame sword
        //1 = boots of flight
        //2 = key to castle
        //Debug.Log("SelectItem() : " + item);

        switch (item)
        {
            case 0://flame sword
                UIManager.Instance.UpdateShopSelection(82);
                _itemSelected = 0;
                _itemSelectedCost = 200;
                break;
            case 1: // boots of flight
                UIManager.Instance.UpdateShopSelection(-28);
                _itemSelected = 1;
                _itemSelectedCost = 400;
                break;
            case 2: // key to castle
                UIManager.Instance.UpdateShopSelection(-137);
                _itemSelected = 2;
                _itemSelectedCost = 100;
                break;
        }
        Debug.Log(_itemSelected);
    }

    //ButItem Method
    //check if player gems is greater than or equal to itemCost.
    //if it is, then awarditem (subtract cost from player gems
    //else cancel sale, close shop
    public void BuyItem()
    {
        Debug.Log("Called");
        if (_player.diamonds >= _itemSelectedCost)
        {
            //award item
            switch (_itemSelected)
            {
                case 0://flamesword
                    Debug.Log("Flame Sword Awarded");
                    _player.FlameAttackActive();
                    break;
                case 1://bootsofflight
                    Debug.Log("Boots of Flight Awarded");
                    _player.DoubleJumpActive();
                    break;
                case 2://KeyToTheCastle
                    GameManager.Instance.HasKeyToCastle = true;
                    break;
            }
            _player.diamonds -= _itemSelectedCost;
            UIManager.Instance.ItemBought(_player.diamonds);
            Debug.Log("Purchased " + _itemSelected);
            Debug.Log("Remaining Gems " + _player.diamonds);
        }
        else
        {
            Debug.Log("You do not have enough gems");
            shopPanel.SetActive(false);
        }
    }
}
