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

    private void Awake()
    {
        itemImage.sprite = item.ItemIcon;

        button.onClick.AddListener(() => UIManager.Instance.ShopPrefab.GetComponent<ShopComponent>().BuyItem(item));
    }
    


}
