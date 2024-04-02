using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UISelectedItemPopup : APopup
{
    [SerializeField] private Image iconImg;

    [Space]
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescText;

    [Space]
    [SerializeField] private Button dropItemBtn;
    [SerializeField] private Button useItemBtn;
    [SerializeField] private Button equipItemBtn;

    private UIPopupsManager popupsManager;

    private ItemEntity selectedItem;

    [Inject]
    private void Construct(UIPopupsManager popupsManager)
    {
        this.popupsManager = popupsManager;
    }

    public override void OnInit()
    {
        base.OnInit();

        dropItemBtn.onClick.AddListener(() =>
        {
            selectedItem.Drop();
            popupsManager.CloseCurrentPopup();
        });

        useItemBtn.onClick.AddListener(() =>
        {
            selectedItem.Use();
            popupsManager.CloseCurrentPopup();
        });

        equipItemBtn.onClick.AddListener(() =>
        {
            selectedItem.Equip();
            popupsManager.CloseCurrentPopup();
        });
    }

    public void SelectItem(ItemEntity item)
    {
        selectedItem = item;
        ItemInfo itemInfo = selectedItem.Info;

        iconImg.sprite = itemInfo.CellIcon;

        itemNameText.text = itemInfo.NameKey;
        itemDescText.text = itemInfo.DescKey;

        dropItemBtn.gameObject.SetActive(true);
        useItemBtn.gameObject.SetActive(itemInfo.IsUsable);
        equipItemBtn.gameObject.SetActive(itemInfo.IsEquipable || itemInfo.IsWeapon);
    }
}
