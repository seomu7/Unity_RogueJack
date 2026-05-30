using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombChip : ChipSO
{
    private int dealerBurstAddScore = 500;

    public override Sequence OnDealerBurst()
    {
        Sequence seq = GameManager.Instance.scoreBoardController.AddScore(dealerBurstAddScore);

        return seq;
    }

    public override void SetDescription()
    {
        chipDescription = $"µô·¯°¡ ¹ö½ºÆ®ÇÏ¸é + {dealerBurstAddScore}Á¡";
    }

    public override void SetName()
    {
        chipName = "ÆøÅº Ä¨";
    }

    public override void SetRarity()
    {
        chipRarity = ChipRarity.Uncommon;
    }
}
