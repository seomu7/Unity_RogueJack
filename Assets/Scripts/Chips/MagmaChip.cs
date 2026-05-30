using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaChip : ChipSO
{
    private int magmaBurstCap = 22;

    public override void OnSelectChip()
    {
        GameManager.Instance.player.SetPlayerBurstCap(magmaBurstCap);
    }

    public override void SetDescription()
    {
        chipDescription = "획득시, 22를 초과해야 버스트됩니다";
    }

    public override void SetName()
    {
        chipName = "마그마 칩";
    }

    public override void SetRarity()
    {
        chipRarity = ChipRarity.Uncommon;
    }
}
