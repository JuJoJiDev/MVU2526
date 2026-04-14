using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LevelConfig nextScene;
    
    private AudioSystem audioSystem;
    private LevelLoader leverLoader;

    [Inject]
    public void SetDependecies(AudioSystem audioSystem, LevelLoader sceneLoader)
    {
        this.audioSystem = audioSystem;
        this.leverLoader = sceneLoader;
    }
    public void Update()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            audioSystem.EmitSound(("Jump sfx"));
        }

        if (Keyboard.current.f1Key.wasReleasedThisFrame)
        {
            leverLoader.LoadLevel(nextScene);
        }
    }
}
