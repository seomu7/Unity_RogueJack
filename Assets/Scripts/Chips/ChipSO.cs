using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum ChipRarity { Common, Uncommon, Rare}

public abstract class ChipSO : ScriptableObject
{
    
    [field: SerializeField, ReadOnly, Header("Common Property")]
    public Sprite chipImage { get; protected set; }
    [field: SerializeField, ReadOnly]
    public string chipID{ get; protected set; }

    [field: SerializeField, ReadOnly, Header("Individual Property ")]
    public string chipName { get; protected set; }
    [field: SerializeField, ReadOnly]
    public string chipDescription { get; protected set; }
    [field: SerializeField, ReadOnly]
    public ChipRarity chipRarity { get; protected set; }

    public virtual void Initialize(Sprite image, string ID)
    {
        this.chipImage = image;
        Initialize(ID);
    }

    public virtual void Initialize(string ID)
    {
        this.chipID = ID;
        Initialize();
    }

    public virtual void Initialize()
    {
        SetRarity();
        SetName();
        SetDescription();
    }

    public abstract void SetRarity();
    public abstract void SetName();
    public abstract void SetDescription();

    
    public virtual void OnRoundStart() { }
    public virtual void OnDeckCreated() { }
    public virtual void OnAfterPlayerCardDraw(Card card) { }
    public virtual void OnDealerCardDraw(Card card) { }
    public virtual void OnHit() { }
    public virtual void OnStay() { }
    public virtual void OnRoundEnd() { }
    public virtual void OnCardCreated() { }
    public virtual void OnCardCreated(Card card) { }
    public virtual void OnPlusScoring() { }
    public virtual void OnMultiScoring() { }
    public virtual void OnBeginPlayerCardDraw() { }
    public virtual void OnSelectChip() { }
    public virtual void OnTopCardDraw(Card card) { }

    public virtual Sequence OnHitSequence() { return null; }
    public virtual Sequence OnStaySequence() {  return null; }
    public virtual Sequence OnRoundEndSequence() { return null; }
    public virtual Sequence OnAfterPlayerCardDrawSequence(Card card) { return null; }
    public virtual Sequence OnDealerBurst() { return null; }
}
