using UnityEngine;
using UnityEngine.SceneManagement;

namespace Snake.AI.Core
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance { get; private set; }

        // Scene names - match with actual Unity scene names
        public const string MAIN_MENU_SCENE = "MainMenu";
        public const string POWER_SELECT_SCENE = "PowerSelect";
        public const string GAMEPLAY_SCENE = "Gameplay";
        public const string GAME_OVER_SCENE = "GameOver";

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void LoadMainMenu()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(MAIN_MENU_SCENE);
        }

        public void LoadPowerSelect()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(POWER_SELECT_SCENE);
        }

        public void StartNewRun()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(GAMEPLAY_SCENE);
        }

        public void ShowGameOver()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(GAME_OVER_SCENE);
        }
    }
}