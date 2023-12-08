using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public Item item { get; private set; }

    Button button;

    private bool isPurchased = false;

    public void Init(Item item)
    {
        if (item == null) return;
        this.item = item;
        this.item.Init();

        button = GetComponent<Button>();
        button.onClick.AddListener(() => SelectShopItem());
    }
    
    public void SelectShopItem()
    {
        UIManager.Instance.ShopPrefab.GetComponent<ShopComponent>().SelectShopItem(this);
        Debug.Log($"Selecting Item: {this}");
    }

    public void SetPurchased(bool state)
    {
        isPurchased = state;
    }

    public bool IsPurchased()
    {
        return isPurchased;
    }

}
