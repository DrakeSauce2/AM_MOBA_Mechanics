using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Item/BaseItem")]
public class Item : ScriptableObject
{
    [SerializeField] private Image itemImage;

    [SerializeField] private Stats itemStats;

    public void ApplyStats()
    {

    }
}
