using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    
    private List<Item> itemsList = new List<Item>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }



}
