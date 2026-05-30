using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpadeChip : ChipSO, IAfterPlayerCardDraw
{
    Sequence IAfterPlayerCardDraw.OnAfterPlayerCardDraw(Card card)
    {
        Sequence seq = DOTween.Sequence();

        if (card.shape == CardShape.Spade)
        {
            int scoreToAdd = 100;
            int startScore = GameManager.Instance.scoreBoardController.score;
            int endScore = startScore + scoreToAdd;

            GameManager.Instance.scoreBoardController.score = endScore;

            seq
                .Append(DOTween.To(
                    () => startScore,
                    x => { GameManager.Instance.scoreBoardController.scoreText.text = ((int)x).ToString(); },
                    endScore, 0.5f)
                .SetEase(Ease.Linear));
        }

        return seq;
    }

    public override Sequence OnAfterPlayerCardDrawSequence(Card card)
    {
        Sequence seq = DOTween.Sequence();

        if (card.shape == CardShape.Spade)
        {
            seq.Append(GameManager.Instance.scoreBoardController.AddScore(100));
            return seq;
        }

        return null;
    }

    public override void SetDescription()
    {
        chipDescription = "내가 스페이드 카드를 뽑을 때마다 +100 점";
    }

    public override void SetName()
    {
        chipName = "스페이드 칩";
    }

    public override void SetRarity()
    {
        chipRarity = ChipRarity.Common;
    }
}

