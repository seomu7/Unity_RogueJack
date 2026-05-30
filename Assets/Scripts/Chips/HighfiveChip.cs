using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighfiveChip : ChipSO
{
    public override void OnSelectChip()
    {
        GameManager.Instance.CW_Startegy = new CW_Equal();
    }

    public override void SetDescription()
    {
        chipDescription = "획득시 동점이어도 내가 승리합니다";
    }

    public override void SetName()
    {
        chipName = "하이파이브 칩";
    }

    public override void SetRarity()
    {
        chipRarity = ChipRarity.Uncommon;
    }
}
