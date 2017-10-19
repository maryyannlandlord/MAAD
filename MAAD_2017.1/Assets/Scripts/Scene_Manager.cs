using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Restarts Scene & sets world 

namespace HoloToolkit.Unity
{
    public class Scene_Manager : Singleton<Scene_Manager>
    {

        private int SceneLauncherBuildIndex { get; set; }

        protected override void Awake()
        {
            // If we have already initialized,
            // then we've created a second one.
            if (IsInitialized)
            {
                Destroy(gameObject);
            }
            else
            {
                base.Awake();
            }
        }

        // Use this for initialization
        void Start()
        {
            SceneLauncherBuildIndex = SceneManager.GetActiveScene().buildIndex;
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void Restart()
        {
            Debug.LogFormat("SceneLauncher: Returning to SceneLauncher scene {0}.", SceneLauncherBuildIndex);
            SceneManager.LoadSceneAsync(SceneLauncherBuildIndex, LoadSceneMode.Single);
        }


    }
}
