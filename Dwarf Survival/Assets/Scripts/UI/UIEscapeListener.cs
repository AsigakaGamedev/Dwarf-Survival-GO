using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIEscapeListener : MonoBehaviour
{   
    [SerializeField] private string escapeScreen;

    private UIManager uiManager;

    [Inject]
    private void Construct(UIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            uiManager.ChangeScreen(escapeScreen);
        }
    }
}
