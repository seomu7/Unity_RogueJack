using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinChip : ChipSO
{
    private int plusAddSocre = 150;

    public override Sequence OnStaySequence()
    {
        if(GameManager.Instance.player.ReturnTotalMaxNumber() % 2 == 0)
        {
            return GameManager.Instance.scoreBoardController.AddScore(plusAddSocre);
        }

        return null;
    }

    public override void SetDescription()
    {
        chipDescription = $"스테이 할 때 내 카드 합이 짝수면, + {plusAddSocre}점";
    }

    public override void SetName()
    {
        chipName = "쌍둥이 칩";
    }

    public override void SetRarity()
    {
        chipRarity = ChipRarity.Common;
    }
}
