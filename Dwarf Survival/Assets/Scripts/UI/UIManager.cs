using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, IInitListener, IDeinitListener
{
    [SerializeField] private string startScreen;
    [SerializeField] private UIScreen[] screens;

    [Space]
    [ReadOnly, SerializeField] private UIScreen curScreen;

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    public void OnInitialize()
    {
        foreach (UIScreen screen in screens)
        {
            screen.Init();
        }

        ChangeScreen(startScreen);
    }

    public void OnDeinitialize()
    {

    }

    private UIScreen GetScreen(string screenName)
    {
        foreach (UIScreen screen in screens)
        {
            if (screen.ScreenName == screenName) return screen;
        }

        throw new System.Exception($"{screenName} экрана не существует!");
    }

    public void ChangeScreen(string screenName)
    {
        UIScreen nextScreen = GetScreen(screenName);

        if (curScreen && nextScreen != curScreen)
        {
            curScreen.HideScreen();
        }

        curScreen = nextScreen;
        curScreen.ShowScreen();
        Cursor.lockState = curScreen.CursorLockMode;
    }
}
