using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class ScoreBoardController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score = 0;

    [Header("Modifiers")]
    public int baseScore;
    public int absModifier;
    public float plusModifier;
    public float multiModifier;

    public Sequence AddScore(int scoreToAdd)
    {
        Sequence seq = DOTween.Sequence();

        int startScore = score;
        int endScore = startScore + scoreToAdd;

        score = endScore;

        seq.Append(DOTween.To(
            ()=> startScore,
            x => {scoreText.text = ((int)x).ToString(); },
            endScore, CONSTANT.ADD_SCORE_DURATION)
            .SetEase(Ease.Linear))
            .AppendInterval(CONSTANT.ADD_SCORE_INTERVAL_DURATION);

        return seq;
    }

    public void ResetGame()
    {
        score = 0;
        scoreText.text = "0";
    }
}
