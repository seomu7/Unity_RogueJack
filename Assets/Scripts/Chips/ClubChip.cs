using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubChip : ChipSO
{
    public override void OnAfterPlayerCardDraw(Card card)
    {
        if(card.shape == CardShape.Club)
        {
            GameManager.Instance.scoreBoardController.AddScore(100);
            Debug.Log(this.chipName);
        }
    }

    public override Sequence OnAfterPlayerCardDrawSequence(Card card)
    {
        Sequence seq = DOTween.Sequence();

        if (card.shape == CardShape.Club)
        {
            seq.Append(GameManager.Instance.scoreBoardController.AddScore(100));
            return seq;
        }

        return null;
    }

    public override void SetDescription()
    {
        chipDescription = "내가 클럽 카드를 뽑을 때마다 +100 점";
    }

    public override void SetName()
    {
        chipName = "클럽 칩";
    }

    public override void SetRarity()
    {
        chipRarity = ChipRarity.Common;
    }
}
