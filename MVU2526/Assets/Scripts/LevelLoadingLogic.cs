using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LevelLoadingLogic : MonoBehaviour
{
    private LevelLoader levelLoader;
    
    private List<AsyncOperation> loadingOperations = new();

    [Inject]
    public void SetDependencies(LevelLoader levelLoader)
    {
        this.levelLoader = levelLoader;
    }

    Coroutine loadingCorroutine;
    private void Update()
    {
        
        if (loadingCorroutine == null && levelLoader.HasLoadingRequest)
        {
            levelLoader.Clear();
            loadingCorroutine = StartCoroutine(StartToLoadScene());
            levelLoader.ConsumeRequest();
        }
    }

    private IEnumerator StartToLoadScene()
    {
        var currentConfig = levelLoader.CurrentRequest;
        yield return LoadScene(currentConfig.loadingScenePath, LoadSceneMode.Single);
        loadingOperations[0].allowSceneActivation = true;
        yield return new WaitUntil(() => loadingOperations[0].isDone);
        loadingOperations.Clear();
        yield return LoadScene(currentConfig.logicScenePath, LoadSceneMode.Additive);
        levelLoader.SetProgress(0.25f);
        yield return LoadScene(currentConfig.audioScenePath, LoadSceneMode.Additive);
        levelLoader.SetProgress(0.5f);
        yield return LoadScene(currentConfig.artScenePath, LoadSceneMode.Additive);
        levelLoader.SetProgress(0.75f);
        yield return LoadScene(currentConfig.designScenePath, LoadSceneMode.Additive);
        levelLoader.SetProgress(1f);

        foreach (var operation in loadingOperations)
        {
            operation.allowSceneActivation = true;
        }
        
        yield return new WaitUntil(() => loadingOperations.All(ao => ao.isDone));
        SceneManager.UnloadSceneAsync(currentConfig.loadingScenePath);
    }

    private IEnumerator LoadScene(string scenePath, LoadSceneMode mode)
    {
        // AsyncOperation asyncOperation = SceneManager

        var asyncOperation = SceneManager.LoadSceneAsync(scenePath,mode);
        loadingOperations.Add(asyncOperation);
        asyncOperation.allowSceneActivation = false;
        yield return new WaitForSeconds(1f);
        while (asyncOperation.progress < 0.9f)
        {
            yield return null;
        }
        
    }
}