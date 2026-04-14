using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DefaultNamespace
{
    public class LoadingSceneUILogic : MonoBehaviour
    {
        [SerializeField] private Image loadingBar;
        LevelLoader levelLoader;
        
        [Inject]
        public void SetDependencies(LevelLoader levelLoader)
        {
            this.levelLoader = levelLoader;
        }

        public void Update()
        {
            loadingBar.fillAmount = levelLoader.ReportProgress;
        }
    }
    
}