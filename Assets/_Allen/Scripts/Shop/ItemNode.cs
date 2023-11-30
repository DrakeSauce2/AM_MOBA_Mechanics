using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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

        float xPos = ((children.Count + 1) / 2) - 35f;
        for (int i = 0; i < children.Count; i++)
        {
            float positionStep;

            if (IsRoot()) xPos = 75;
            else if (children.Count == 1) xPos = 0;

            positionStep = xPos * Mathf.Pow(-1, i + 1);

            Vector2 newPos = new Vector2(position.x + (positionStep), position.y + 75);
            children[i].Init(this, newPos);

            CreateLines(position, positionStep, i);
        }
    }

    private void CreateLines(Vector2 position, float step, int index)
    {
        Vector3 lineDir = Vector3.zero;
        Vector3 lineScale = Vector3.one;
        Vector3 linePos = Vector3.zero;

        if (IsRoot())
        {
            linePos = new Vector3(position.x - 50 * Mathf.Pow(-1, index + 1), position.y, 0);
            lineDir = new Vector3(0, 0, 35 * Mathf.Pow(-1, index + 1));
            lineScale = new Vector3(1, 1.25f, 1);
        }
        else if (children.Count != 1)
        {
            linePos = new Vector3(position.x, position.y, 0);
            lineDir = new Vector3(0, 0, -25 * Mathf.Pow(-1, index + 1));
        }

        GameObject itemLine = Instantiate(UIManager.Instance.ItemLinePrefab, linePos, Quaternion.identity, UIManager.Instance.ShopTreeTransform);
        itemLine.transform.localScale = lineScale;
        itemLine.transform.Rotate(lineDir);

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
