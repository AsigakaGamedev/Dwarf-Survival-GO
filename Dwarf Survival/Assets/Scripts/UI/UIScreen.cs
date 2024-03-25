using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIScreen : MonoBehaviour
{
    [SerializeField] private string screenName;

    public string ScreenName { get => screenName; }

    public void Init()
    {
        gameObject.SetActive(false);
    }

    public void ShowScreen()
    {
        gameObject.SetActive(true);
    }

    public void HideScreen()
    {
        gameObject.SetActive(false);
    }
}
