using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAfterPlayerCardDraw
{
    public Sequence OnAfterPlayerCardDraw(Card card);
}
