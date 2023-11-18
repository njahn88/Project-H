using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMangerTool : MonoBehaviour
{
    public static SceneMangerTool Instance;

    [SerializeField]
    private float _fadeSpeed;

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
    /// <summary>
    /// Handles the camera fading in and out, loading the scene with paramater name, and handles inputs 
    /// </summary>
    /// <param name="sceneName"></param>
    public async void LoadScene(string sceneName)
    {
        InputManager.Instance.DisableAllInputs();
        await UiManager.Instance.FadeIn(_fadeSpeed);
        SceneManager.LoadScene(sceneName);
        await UiManager.Instance.FadeOut(_fadeSpeed);
        InputManager.Instance.EnableGamePlayInputs();
    }

}
