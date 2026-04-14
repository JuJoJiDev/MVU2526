using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneInitializer : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<AudioSystem>().AsSingle();
        Container.Bind<LevelLoader>().AsSingle();
    }
}
public class AudioSystem
{
    public void EmitSound(string id)
    {
        Debug.Log($"Sound Emited {id}");
    }
}

public class LevelLoader
{

    public bool HasLoadingRequest => CurrentRequest != null;
    public LevelConfig CurrentRequest { get; private set; }

    public float  ReportProgress { get; private set; }

    public void SetProgress(float progress)
    {
        ReportProgress = progress;
    }
    public void LoadLevel(LevelConfig nextScene)
    {
        //Crear el cargador -> Ejecutor de corrutina
        CurrentRequest = nextScene;
    }

    
    public void ConsumeRequest()
    {
        CurrentRequest = null;
    }

    public void Clear()
    {
        ReportProgress = 0;
    }
}
