using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellChip : ChipSO
{
    private int hitAddSocre = 50;

    public override Sequence OnHitSequence()
    {
        Sequence seq = GameManager.Instance.scoreBoardController.AddScore(hitAddSocre);

        return seq;
    }

    public override void SetDescription()
    {
        chipDescription = $"내가 히트할 때마다 + {hitAddSocre}점";
    }

    public override void SetName()
    {
        chipName = "벨 칩";
    }

    public override void SetRarity()
    {
        chipRarity = ChipRarity.Common;
    }
}
