using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMangerTool : MonoBehaviour
{
    public static SceneMangerTool Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        InputManager.Instance.DisableAllInputs();
        SceneManager.LoadScene(sceneName);
        InputManager.Instance.EnableAllInputs();
    }
}
