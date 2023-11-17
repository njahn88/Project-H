using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;


public class UiManager : MonoBehaviour
{
    public static UiManager Instance;
    [SerializeField]
    private GameObject _mainMenu;

    [SerializeField]
    private Image _screenCover;

    private Color _blackColor = Color.black;
    private Color _clearColor = Color.clear;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        MainMenuManager.OnGameStarted += RemoveMainMenu;
    }

    private void OnDisable()
    {
        MainMenuManager.OnGameStarted -= RemoveMainMenu;
    }

    private void RemoveMainMenu()
    {
        _mainMenu.SetActive(false);
    }
    #region Fade In/ Out
    public async Task FadeIn(float fadeTime)
    {
        float elapsedTime = 0;
        while(elapsedTime < fadeTime)
        {
            float t = elapsedTime / fadeTime;
            _screenCover.color = Color.Lerp(_clearColor, _blackColor, t);
            elapsedTime += Time.deltaTime;
            await Task.Yield();
        }
    }

    public async Task FadeOut(float fadeTime)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeTime)
        {
            float t = elapsedTime / fadeTime;
            _screenCover.color = Color.Lerp(_blackColor, _clearColor, t);
            elapsedTime += Time.deltaTime;
            await Task.Yield();
        }
    }
    #endregion
}
