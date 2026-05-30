using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ScoreEntry
{
    public int highScore;
    public string timeStamp;
    public List<string> chipIDs;
}

public class HighScoreSaveManager : MonoBehaviour
{
    private string saveFilePath;

    private static HighScoreSaveManager _instance;
    public static HighScoreSaveManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<HighScoreSaveManager>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (_instance == this)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        saveFilePath = Path.Combine(Application.persistentDataPath, "HighScore.json");
    }

    public bool IsHighScore(int score, List<Chip> playerChips)
    {
        ScoreEntry scoreEntry = LoadScore();

        if (scoreEntry == null || scoreEntry.highScore < score)
        {
            SaveNewScore(score, playerChips);
            return true;
        }

        return false;
    }

    private void SaveNewScore(int score, List<Chip> playerChips)
    {
        ScoreEntry newEntry = new ScoreEntry
        {
            highScore = score,
            timeStamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"), // 현재 시각
            chipIDs = playerChips.Select(chip => chip.SO.chipID).ToList() // 칩에서 ID만 추출
        };

        string json = JsonUtility.ToJson(newEntry);
        File.WriteAllText(saveFilePath, json);
    }

    public ScoreEntry LoadScore()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            return JsonUtility.FromJson<ScoreEntry>(json);
        }

        return null;
    }
}
