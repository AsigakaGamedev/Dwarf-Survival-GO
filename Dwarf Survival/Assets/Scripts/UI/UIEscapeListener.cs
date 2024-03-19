using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEscapeListener : MonoBehaviour
{   
    [SerializeField] private string escapeScreen;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ServiceLocator.GetService<UIManager>().ChangeScreen(escapeScreen);
        }
    }
}
