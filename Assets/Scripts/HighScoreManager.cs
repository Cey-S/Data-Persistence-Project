using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance;

    // scoreList is saved with JSON, in chronological order
    private ScoreList scoreList;

    // sortedScores list is a sorted list of scoreList
    private List<Score> sortedScores = new List<Score>();

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

        LoadScores(); // fills scoreList
        
        SortScores(); // fills sortedScores
    }

    public void AddToScores(Score score)
    {
        scoreList.scores.Add(score);
        SaveScores();

        SortScores();
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
        if (IsScoreListEmpty())
        {
            Debug.Log("Score list is empty");
        }
        else
        {
            sortedScores = scoreList.scores.OrderByDescending(s => s.score).ToList();            
        }
    }
    
    public List<Score> GetScores()
    {
        return sortedScores;
    }

    public Score GetHighestScore()
    {
        if (IsScoreListEmpty())
        {
            return new Score("", 0);
        }
        else
        {
            return sortedScores[0];
        }
    }

    public string GetLastEntryName()
    {
        if (IsScoreListEmpty())
        {
            return "";
        }
        else
        {
            return scoreList.scores[scoreList.scores.Count - 1].playerName;
        }
    }

    public bool IsScoreListEmpty()
    {
        return !scoreList.scores.Any();
    }
}
