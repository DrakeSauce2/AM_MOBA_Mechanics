using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemNode/ItemNode")]
public class ItemNode : ScriptableObject
{
    [SerializeField] private Item item;
    public Item NodeItem { get { return item; } }
    public ItemNode parent { get; private set; }
    public List<ItemNode> children = new List<ItemNode>();

    private GameObject slotPrefab = null;
    private ShopItem shopItem = null;

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

        shopItem = slotPrefab.GetComponent<ShopItem>(); 
        slotPrefab.name = name;
    }

    public void CreateNodes(Vector2 position)
    {
        CreateIcon(position);

        if (children.Count == 0) return;

        float xPos = ((children.Count + 1) / 2) - 25f;
        for (int i = 0; i < children.Count; i++)
        {
            float positionStep;

            if (IsRoot()) xPos = 75;
            else if (children.Count == 1) xPos = 0;

            positionStep = xPos * Mathf.Pow(-1, i + 1);

            children[i].Init(this, new Vector2(position.x + (positionStep), position.y + 50));
        }
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

        slotPrefab = null;
    }
}
