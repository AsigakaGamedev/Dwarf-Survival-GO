using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class UIScreenChanger : MonoBehaviour
{
    [SerializeField] private string screenName;

    private UIManager uiManager;
    private Button button;

    [Inject]
    private void Construct(UIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            uiManager.ChangeScreen(screenName);
        });
    }
}
