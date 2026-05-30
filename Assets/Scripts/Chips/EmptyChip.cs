using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyChip : ChipSO
{
    public override void SetDescription()
    {
        chipDescription = "이런, 남아있는 칩이 없는 것 같네요!";
    }

    public override void SetName()
    {
        chipName = "빈 칩";
    }

    public override void SetRarity()
    {
        chipRarity = ChipRarity.Common;
    }
}
