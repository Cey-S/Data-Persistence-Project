using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance;

    private ScoreList scoreList;

    [System.Serializable]
    class ScoreList
    {
        public List<Score> scores = new List<Score>();
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        scoreList = new ScoreList();

        LoadScores();
    }

    public void AddToScores(Score score)
    {
        scoreList.scores.Add(score);
        SortScores();
        SaveScores();
    }

    public void SaveScores()
    {
        string json = JsonUtility.ToJson(scoreList);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, scoreList);
        }
    }

    public void SortScores()
    {
        if (scoreList.scores.Any())
        {
            List<Score> sortedScores = scoreList.scores.OrderByDescending(s => s.score).ToList();
            scoreList.scores = sortedScores;
        }
        else
        {
            Debug.Log("Score list is empty");
        }
    }

    public Score GetHighestScore()
    {
        if (scoreList.scores.Any())
        {
            return scoreList.scores[0];
        }
        else
        {
            return new Score("Name", 0);
        }
    }
}
