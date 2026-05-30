using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondChip : ChipSO
{
    public override void OnAfterPlayerCardDraw(Card card)
    {
        if (card.shape == CardShape.Diamond)
        {
            GameManager.Instance.player.walletMoney += 102;
            Debug.Log(this.chipName);
        }
    }

    public override Sequence OnAfterPlayerCardDrawSequence(Card card)
    {
        Sequence seq = DOTween.Sequence();

        if(card.shape == CardShape.Diamond)
        {
            seq.Append(GameManager.Instance.scoreBoardController.AddScore(100));
            return seq;
        }

        return null;
    }

    public override void SetDescription()
    {
        chipDescription = "내가 다이아몬드 카드를 뽑을 때마다 +100 점";
    }

    public override void SetName()
    {
        chipName = "다이아몬드 칩";
    }

    public override void SetRarity()
    {
        chipRarity = ChipRarity.Common;
    }
}
