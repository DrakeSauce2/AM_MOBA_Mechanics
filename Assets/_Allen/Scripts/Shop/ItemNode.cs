using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ItemNode/ItemNode")]
public class ItemNode : ScriptableObject
{
    [SerializeField] private Item item;
    public Item NodeItem { get { return item; } }
    public ItemNode parent { get; private set; }
    public List<ItemNode> children = new List<ItemNode>();

    public GameObject slotPrefab { get; private set; }
    public ShopItem shopItem { get; private set; }

    private List<GameObject> lines = new List<GameObject>();

    public void Init(ItemNode parent, Vector2 position)
    {
        this.parent = parent;

        CreateNodes(position);
    }

    public void CreateIcon(Vector2 iconPosition)
    {
        if (slotPrefab != null) return;

        slotPrefab = Instantiate(UIManager.Instance.ItemSlotPrefab, iconPosition, 
                                 Quaternion.identity, UIManager.Instance.ShopTreeTransform);

        shopItem = slotPrefab.GetComponentInChildren<ShopItem>();
        shopItem.gameObject.GetComponent<Image>().sprite = item.ItemIcon;
        shopItem.Init(item);
        shopItem.onItemPurchase += ItemPurchased;

        if (!IsRoot())
        {
            Color color = new Color(.5f, .5f, .5f);
            shopItem.gameObject.GetComponent<Image>().color = color;
        }

        slotPrefab.name = name;
    }

    private void ItemPurchased(bool state)
    {
        Debug.Log("Node Item Purchased");

        if(state == true)
        {
            Color color = new Color(.5f, .5f, .5f);
            shopItem.gameObject.GetComponent<Image>().color = color;

            foreach (ItemNode child in children)
            {
                color = new Color(1f, 1f, 1f);
                child.shopItem.gameObject.GetComponent<Image>().color = color;
            }

        }
        else
        {
            Color color = new Color(1f, 1f, 1f);
            shopItem.gameObject.GetComponent<Image>().color = color;

            foreach (ItemNode child in children)
            {
                if (child.shopItem.IsPurchased()) continue;

                color = new Color(0.5f, 0.5f, 0.5f);
                child.shopItem.gameObject.GetComponent<Image>().color = color;
            }

        }
    }

    public void CreateNodes(Vector2 position)
    {
        if (children.Count == 0)
        {
            CreateIcon(position);
            return;
        }

        float xPos = ((children.Count + 1) / 2) - 35f;
        for (int i = 0; i < children.Count; i++)
        {
            float positionStep;

            if (IsRoot()) xPos = 75;
            else if (children.Count == 1) xPos = 0;

            positionStep = xPos * Mathf.Pow(-1, i + 1);

            Vector2 newPos = new Vector2(position.x + (positionStep), position.y + 75);
            children[i].Init(this, newPos);

            MakeLines(position, newPos, 7.5f);
        }

        CreateIcon(position);
    }

    private void MakeLines(Vector3 startPosition, Vector3 endPosition, float lineThickness)
    {
        GameObject line = new GameObject("Shop Line");
        line.AddComponent<CanvasRenderer>();
        line.AddComponent<RectTransform>();
        line.transform.SetParent(UIManager.Instance.LinePrefabTransform);
        ShopItemLine itemLine = line.AddComponent<ShopItemLine>();
        itemLine.Init(startPosition, endPosition, lineThickness);

        lines.Add(line);
    }

    public bool IsRoot()
    {
        return parent == null;
    }

    public void Deconstruct()
    {
        if(children.Count == 0)
        {
            Destroy(slotPrefab);
            return;
        }

        foreach (ItemNode node in children)
        {
            node.Deconstruct();
            Destroy(slotPrefab);
        }

        foreach (GameObject line in lines)
        {
            Destroy(line);
            lines.Remove(line);
        }

        slotPrefab = null;
    }
}
