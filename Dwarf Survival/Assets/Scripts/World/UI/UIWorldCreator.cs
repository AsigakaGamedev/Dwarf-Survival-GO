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
        createWorldBtn.onClick.AddListener(() =>
        {
            LoadingAction[] startLoadingTasks = new LoadingAction[]
            {
               
            };

            LoadingAction generateWorldTask = new LoadingAction("World Generating", () =>
            {
                WorldManager worldManager = GameplaySceneInstaller.Instance.WorldManager;
                worldManager.GenerateWorld();
            });

            LoadingAction generateObjectsTask = new LoadingAction("Objects Generating", () =>
            {
                WorldManager worldManager = GameplaySceneInstaller.Instance.WorldManager;
                worldManager.GenerateObjects();
            });

            LoadingAction generatePlayerTask = new LoadingAction("Aznor Generating", () =>
            {
                PlayerManager playerManager = GameplaySceneInstaller.Instance.PlayerManager;
                playerManager.FirstSpawnPlayer();
            });

            LoadingAction[] endLoadingTasks = new LoadingAction[]
            {
                generateWorldTask,
                generateObjectsTask,
                generatePlayerTask
            };

            loadingManager.LoadSceneAsync(gameSceneName, startLoadingTasks, endLoadingTasks);
        });
    }
}
