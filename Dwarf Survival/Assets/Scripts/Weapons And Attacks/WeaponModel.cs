using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModel : MonoBehaviour
{
    [SerializeField] private AttacksHandler attackHandler;

    public void Init()
    {
        attackHandler.Init();
    }

    public bool TryAttack(Vector2 dir)
    {
        return attackHandler.TryAttack(dir);   
    }

    public void OnEquip()
    {
        gameObject.SetActive(true);
    }

    public void OnDequip()
    {
        gameObject.SetActive(false);
    }
}
