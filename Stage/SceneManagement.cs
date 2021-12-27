using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;


namespace PipLib.Stage
{
    public class SceneControls
    {
        public static event Action onStageChange;
        public static event Action onStageReset;

        public static void NextLevel()
        {
            onStageChange?.Invoke();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public static void PreviousLevel()
        {
            onStageChange?.Invoke();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        public static void RandomLevel(int? min = 0,int? max = 0)
        {
            onStageChange?.Invoke();
            SceneManager.LoadScene(UnityEngine.Random.Range(min??0,max?? SceneManager.sceneCount));
        }

        public static void To(int index)
        {
            onStageChange?.Invoke();
            SceneManager.LoadScene(index);
        }

        public static void Restart()
        {
            onStageChange?.Invoke();
            onStageReset?.Invoke();
            Debug.Log("SCENE RESTARTED");
            Debug.Log("Reminder: Static variables don't reset on Restart");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }
}