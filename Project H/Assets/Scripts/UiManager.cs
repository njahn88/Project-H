using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;


public class UiManager : MonoBehaviour
{
    public static UiManager Instance;
    [SerializeField]
    private GameObject _mainMenu, _dialogueBox;

    [SerializeField]
    private float _textScrollTime;

    private TextMeshProUGUI _dialogueText;

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
        _dialogueBox.SetActive(false);
        _dialogueText = _dialogueBox.GetComponentInChildren<TextMeshProUGUI>();
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        MainMenuManager.OnGameStarted += RemoveMainMenu;
        NPC.OnDialogueChange += UpdateDialogue;
        NPC.OnDialogueFinished += RemoveDialogueBox;
    }

    private void OnDisable()
    {
        MainMenuManager.OnGameStarted -= RemoveMainMenu;
        NPC.OnDialogueChange -= UpdateDialogue;
        NPC.OnDialogueFinished -= RemoveDialogueBox;
    }

    private void RemoveMainMenu()
    {
        _mainMenu.SetActive(false);
    }

    private void UpdateDialogue(string newDialogue)
    {
        StopAllCoroutines();
        _dialogueBox.SetActive(true);
        StartCoroutine(ScrollText(newDialogue));
    }

    private IEnumerator ScrollText(string textToScroll)
    {
        _dialogueText.text = "";
        foreach (char text in textToScroll)
        {
            _dialogueText.text += text;
            yield return new WaitForSeconds(_textScrollTime);
        }
    }

    private void RemoveDialogueBox()
    {
        _dialogueBox.SetActive(false);
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
