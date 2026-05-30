using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HeartChip : ChipSO
{
    public override void OnAfterPlayerCardDraw(Card card)
    {
        if (card.shape == CardShape.Heart)
        {
            GameManager.Instance.player.walletMoney += 101;
            Debug.Log(this.chipName);
        }
    }

    public override Sequence OnAfterPlayerCardDrawSequence(Card card)
    {
        Sequence seq = DOTween.Sequence();

        /*if (card.shape == CardShape.Heart)
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
        }*/

        if (card.shape == CardShape.Heart)
        {
            seq.Append(GameManager.Instance.scoreBoardController.AddScore(100));
            return seq;
        }

        return null;
    }

    public override void SetDescription()
    {
        chipDescription = "내가 하트 카드를 뽑을 때마다 +100 점";
    }

    public override void SetName()
    {
        chipName = "하트 칩";
    }

    public override void SetRarity()
    {
        chipRarity = ChipRarity.Common;
    }
}
