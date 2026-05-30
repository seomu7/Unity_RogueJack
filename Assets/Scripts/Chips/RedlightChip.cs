using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedlightChip : ChipSO
{
    private int redlightBurstCap = 16;

    public override void OnSelectChip()
    {
        GameManager.Instance.dealer.SetPlayerBurstCap(redlightBurstCap);
    }

    public override void SetDescription()
    {
        chipDescription = $"획득 시 딜러가 {redlightBurstCap}부터 스테이합니다";
    }

    public override void SetName()
    {
        chipName = "비상벨 칩";
    }

    public override void SetRarity()
    {
        chipRarity = ChipRarity.Rare;
    }
}
