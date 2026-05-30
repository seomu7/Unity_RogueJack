using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwofaceChip : ChipSO
{
    public override void OnTopCardDraw(Card card)
    {
        if(card.isFace)
        {
            card.backSprite = CardMaster.Instance.masterBackSprite[1];
            card.image.sprite = card.backSprite;
        }
    }

    public override void SetDescription()
    {
        chipDescription = "페이스 카드의 뒷면은 빨간색으로 보입니다";
    }

    public override void SetName()
    {
        chipName = "투페이스 칩";
    }

    public override void SetRarity()
    {
        chipRarity = ChipRarity.Rare;
    }
}
