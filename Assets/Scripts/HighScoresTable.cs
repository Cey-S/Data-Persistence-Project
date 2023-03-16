using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoresTable : MonoBehaviour
{
    [SerializeField]
    private Transform highScorePrefab;
    [SerializeField]
    private Transform content;

    private List<Score> scores;

    void Start()
    {
        scores = HighScoreManager.Instance.GetScores();

        for (int i = 0; i < scores.Count; i++)
        {
            Transform entry = Instantiate(highScorePrefab, content);
            entry.GetChild(0).GetComponent<Text>().text = scores[i].playerName;
            entry.GetChild(1).GetComponent<Text>().text = scores[i].score.ToString();
        }
    }
}
