using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private Item item;
    [Header("Graphic")]
    [SerializeField] private Image itemImage;

    Button button;

    private bool isPurchased = false;

    private void Awake()
    {
        itemImage.sprite = item.ItemIcon;

        button.onClick.AddListener(() => BuyItem());
    }
    
    private void BuyItem()
    {
        SetPurchased(true);
        UIManager.Instance.ShopPrefab.GetComponent<ShopComponent>().BuyItem(item);
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
