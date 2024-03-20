using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryCellEntity 
{
    [SerializeField] private ItemEntity itemInCell;

    public ItemEntity ItemInCell { get => itemInCell; set => itemInCell = value; }
}
