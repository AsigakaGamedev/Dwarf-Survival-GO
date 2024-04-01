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

    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(ELoadSceneAsyncTask(sceneName, null, null));
    }

    public void LoadSceneAsync(string sceneName, LoadingAction[] startTasks, LoadingAction[] endTasks)
    {
        StartCoroutine(ELoadSceneAsyncTask(sceneName, startTasks, endTasks));
    }

    private IEnumerator ELoadSceneAsyncTask(string sceneName, LoadingAction[] startTasks, LoadingAction[] finishTasks)
    {
        onLoadingStart?.Invoke();

        LoadTasks(startTasks);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        onLoadingTextUpd?.Invoke("Загрузка сцены");

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            onLoadingProgressUpd?.Invoke(progress);
            yield return null;
        }

        LoadTasks(finishTasks);

        onLoadingFinish?.Invoke();
    }

    private void LoadTasks(LoadingAction[] tasks)
    {
        if (tasks == null) return;

        onLoadingProgressUpd?.Invoke(0);
        onLoadingTextUpd?.Invoke("Загрузка сервисов");

        float progressStep = 1f / tasks.Length;
        float tasksProgress = 0;

        foreach (LoadingAction task in tasks)
        {
            onLoadingTextUpd?.Invoke(task.HintText);
            task.Action();
            tasksProgress += progressStep;
            onLoadingProgressUpd?.Invoke(tasksProgress);
        }
    }
}

public struct LoadingAction
{
    public Action Action;
    public string HintText;

    public LoadingAction(string hintText, Action action)
    {
        HintText = hintText;
        Action = action;
    }
}
