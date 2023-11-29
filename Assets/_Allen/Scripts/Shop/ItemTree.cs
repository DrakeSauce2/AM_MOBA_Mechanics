using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTree : MonoBehaviour
{
    [SerializeField] private ItemNode root;

    private Vector2 rootPosition;

    private void Start()
    {
        rootPosition = UIManager.Instance.ShopTreeTransform.position;

        ConstructTree();
    }

    public void ConstructTree()
    {
        root.Init(null, rootPosition);
    }

    public void DeconstructTree()
    {
        root.Deconstruct();
    }
}
