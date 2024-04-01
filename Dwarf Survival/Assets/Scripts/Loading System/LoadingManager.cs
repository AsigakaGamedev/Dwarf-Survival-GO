using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public Action onLoadingStart;
    public Action onLoadingFinish;
    public Action<float> onLoadingProgressUpd;
    public Action<string> onLoadingTextUpd;

    public async Task LoadSceneAsync(string sceneName)
    {
        await LoadSceneAsyncTask(sceneName, null, null);
    }

    public async Task LoadSceneAsync(string sceneName, LoadingTask[] startTasks, LoadingTask[] endTasks)
    {
        await LoadSceneAsyncTask(sceneName, startTasks, endTasks);
    }

    private async Task LoadSceneAsyncTask(string sceneName, LoadingTask[] startTasks, LoadingTask[] finishTasks)
    {
        onLoadingStart?.Invoke();

        await LoadTasks(startTasks);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        onLoadingTextUpd?.Invoke("Загрузка сцены");

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            onLoadingProgressUpd?.Invoke(progress);

            await Task.Yield();
        }

        await LoadTasks(finishTasks);

        onLoadingFinish?.Invoke();
    }

    private async Task LoadTasks(LoadingTask[] tasks)
    {
        if (tasks == null) return;

        onLoadingProgressUpd?.Invoke(0);
        onLoadingTextUpd?.Invoke("Загрузка сервисов");
        await Task.Delay(1000);
        float progressStep = 1f / tasks.Length;
        float tasksProgress = 0;

        foreach (LoadingTask task in tasks)
        {
            print(1);
            onLoadingTextUpd?.Invoke(task.HintText);
            print(2);
            await Task.Run(task.Action);
            print(3);
            tasksProgress += progressStep;
            onLoadingProgressUpd?.Invoke(tasksProgress);
        }
    }
}

public struct LoadingTask
{
    public Action Action;
    public string HintText;

    public LoadingTask(string hintText, Action task)
    {
        HintText = hintText;
        Action = task;
    }
}
