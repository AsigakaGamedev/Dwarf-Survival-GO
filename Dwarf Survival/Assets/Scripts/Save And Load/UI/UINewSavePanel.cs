using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UINewSavePanel : MonoBehaviour
{
    [SerializeField] private Button newSaveBtn;
    [SerializeField] private TMP_InputField saveNameInp;

    private SavesManager savesManager;

    [Inject]
    private void Construct(SavesManager savesManager)
    {
        this.savesManager = savesManager;
    }

    private void Start()
    {
        newSaveBtn.onClick.AddListener(() =>
        {
            savesManager.CreateSave(saveNameInp.text);
        });
    }
}
