using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;

    public int currentSelectedItem;
    public int currentItemCost;
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
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }

    public void SelectItem(int item)
    {
        Debug.Log("SelectItem" + item);
        switch (item)
        {
            case 0: // Flame Sword
                UIManager.Instance.UpdateShopSelection(95.4f);
                currentSelectedItem = 0;
                currentItemCost = 100;
                break;

            case 1: // Boots
                UIManager.Instance.UpdateShopSelection(-6.4f);
                currentSelectedItem = 1;
                currentItemCost = 400;
                break;

            case 2: // Castle Key
                UIManager.Instance.UpdateShopSelection(-106.4f);
                currentSelectedItem = 2;
                currentItemCost = 100;
                break;
        }
    }

    public void BuyItem()
    {
        if (_player.diamonds >= currentItemCost)
        {
            if (currentSelectedItem == 2)
            {
                GameManager.Instance.HasKeyToCastle = true;
            }
            _player.diamonds -= currentItemCost;
        }
        else
        {
            Debug.Log("You do not have enough gems. Closing shop");
            shopPanel.SetActive(false);
        }
    }
}
