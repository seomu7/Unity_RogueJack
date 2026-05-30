using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public partial class Player : MonoBehaviour
{
    public int currentMinTotalNumber { get; private set; }
    public int walletMoney = 0;

    public List<Card> cardsList = new List<Card>();
    public List<Chip> chipsList = new List<Chip>();
    private List<Transform> playerCardTransforms;

    [SerializeField]
    private GameObject HitStayBtnView;

    [Header("RougeLite Properties")]
    private int burstCap = CONSTANT.BURST_CAP;

    private void Start()
    {
        playerCardTransforms = GameManager.Instance.playerCardTransforms;
        tmp();
    }

    private void InitializeAllStatus()
    {
        burstCap = CONSTANT.BURST_CAP;
    }


    public void ResetRound()
    {
        currentMinTotalNumber = 0;

        CardMaster.Instance.ReturnToCardPool(cardsList);
        cardsList.Clear();
    }

    public void ResetGame()
    {
        ResetRound();

        InitializeAllStatus();
        walletMoney = 0;

        foreach (Chip chip in chipsList)
        {
            chip.gameObject.SetActive(false);
        }

        chipsList.Clear();
    }

    public Sequence DrawCard()
    {
        HitStayBtnView.SetActive(false);

        Card newCard = CardMaster.Instance.GiveCardToPlayer();
        cardsList.Add(newCard);
        newCard.transform.SetParent(playerCardTransforms[cardsList.Count - 1], true);

        currentMinTotalNumber += newCard.number;

        Sequence drawSeq = DOTween.Sequence();

        drawSeq
            .Append(newCard.MoveToPosition(playerCardTransforms[cardsList.Count - 1].transform))
            .Append(newCard.Flip());

        foreach(Chip chip in chipsList)
        {
            Sequence seq = chip.SO.OnAfterPlayerCardDrawSequence(newCard);
            if(seq != null)
            {
                drawSeq.Append(seq);
                drawSeq.Join(chip.Highlight());
            }
        }

        return drawSeq;
    }

    public void Check()
    {
        if(currentMinTotalNumber > burstCap)
        {
            GameManager.Instance.Bursted();
        }
        else
        {
            HitStayBtnView.SetActive(true);
        }
    }

    public void OnPlayerTurnStart()
    {
        HitStayBtnView.SetActive(true);
    }

    public void OnPlayerTurnEnd()
    {
        HitStayBtnView.SetActive(false);
    }

    public Sequence Hit()
    {
        Sequence hitSeq = DrawCard();

        foreach (Chip chip in chipsList)
        {
            Sequence seq = chip.SO.OnHitSequence();
            if (seq != null)
            {
                hitSeq.Append(seq);
                hitSeq.Join(chip.Highlight());
            }
        }

        return hitSeq
            .OnComplete(() => Check());
    }

    public int ReturnTotalMaxNumber()
    {
        int maxTotalNumber = currentMinTotalNumber;

        foreach (Card card in cardsList)
        {
            if (card.numberAsString == "A")
            {
                if (maxTotalNumber + 10 <= burstCap)
                {
                    card.TreatAceAs11();
                    maxTotalNumber += 10;
                }
            }
        }

        return maxTotalNumber;
    }

    [Obsolete]
    public IEnumerator DrawCardCoroutine()
    {
        Card newCard = CardMaster.Instance.GiveCardToPlayer();
        cardsList.Add(newCard);
        newCard.transform.SetParent(playerCardTransforms[cardsList.Count - 1], true);

        currentMinTotalNumber += newCard.number;

        Sequence drawSeq = DOTween.Sequence();

        yield return drawSeq.Append(newCard.transform.DOMove(playerCardTransforms[cardsList.Count - 1].transform.position, 1).SetEase(Ease.OutExpo))
            .AppendCallback(() => newCard.MoveToPosition(playerCardTransforms[cardsList.Count - 1].transform))
            .AppendCallback(() => newCard.Flip())
            .OnComplete(() => { Check(); })
            .WaitForCompletion();
    }
}
