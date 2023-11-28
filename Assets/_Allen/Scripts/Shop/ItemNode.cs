using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemNode : ScriptableObject
{
    public Item item;
    public ItemNode parent;
    public List<ItemNode> children = new List<ItemNode>();

    public ItemNode(Item item)
    {
        this.item = item;
        children = new List<ItemNode>();
    }

    public ItemNode AddChild(Item item)
    {
        ItemNode node = new ItemNode(item) { parent = this };
        children.Add(node);
        return node;
    }

    public List<ItemNode> AddChildren(List<ItemNode> children)
    {
        this.children.AddRange(children);
        return this.children;
    }

}
