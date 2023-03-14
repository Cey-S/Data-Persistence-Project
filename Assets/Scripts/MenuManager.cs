using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private InputField nameInputField;
    [SerializeField]
    private Text bestScoreText;

    private void Start()
    {
        DisplayBestScore();

        nameInputField.text = HighScoreManager.Instance.GetLastEntryName();
    }

    private void DisplayBestScore()
    {
        Score highest = HighScoreManager.Instance.GetHighestScore();
        bestScoreText.text = $"Best Score: {highest.playerName} : {highest.score}";
    }

    public void EnterPlayerName()
    {
        PlayerData.Instance.playerName = nameInputField.text;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
