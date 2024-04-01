using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIWorldCreator : MonoBehaviour
{
    [Scene, SerializeField] private string gameSceneName = "GameScene";

    [Space]
    [SerializeField] private Button createWorldBtn;

    private LoadingManager loadingManager;

    [Inject]
    private void Construct(LoadingManager loadingManager)
    {
        this.loadingManager = loadingManager;   
    }

    private void Awake()
    {
        createWorldBtn.onClick.AddListener(async () =>
        {
            LoadingTask[] startLoadingTasks = new LoadingTask[]
            {
               
            };

            LoadingTask generateWorldTask = new LoadingTask("World Generating", () =>
            {

            });

            LoadingTask generateObjectsTask = new LoadingTask("Objects Generating", () =>
            {

            });

            LoadingTask generatePlayerTask = new LoadingTask("Aznor Generating", () =>
            {

            });

            LoadingTask[] endLoadingTasks = new LoadingTask[]
            {
                generateWorldTask,
                generateObjectsTask,
                generatePlayerTask
            };

            await loadingManager.LoadSceneAsync(gameSceneName, startLoadingTasks, endLoadingTasks);
        });
    }
}
