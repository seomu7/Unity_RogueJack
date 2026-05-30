using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public partial class Dealer : MonoBehaviour
{
    public List<Card> cardsList = new List<Card>();

    public int totalNumber { 
        get
        {
            int n = 0;
            
            foreach(Card card in cardsList)
            {
                n += card.number;
            }
            return n;
        } 
    }

    public int shownNumber
    {
        get
        {
            int n = 0;

            foreach (Card card in cardsList)
            {
                if (!card.isBack) n += card.number;
            }
            return n;
        }
    }

    private List<Transform> dealerCardTransforms;

    [Header("RougeLite Properties")]
    private int stayCap = CONSTANT.DEALER_STAY_CAP;

    private void Start()
    {
        dealerCardTransforms = GameManager.Instance.dealerCardTransforms;
    }

    public void ResetRound()
    {
        CardMaster.Instance.ReturnToCardPool(cardsList);
        cardsList.Clear();
    }

    public void ResetGame()
    {
        ResetRound();
        InitializeAllStatus();
    }

    private void InitializeAllStatus()
    {
        stayCap = CONSTANT.DEALER_STAY_CAP;
    }

    public Sequence DrawCard(bool isBack)
    {
        Card newCard = CardMaster.Instance.GiveCardToDealer(isBack);
        cardsList.Add(newCard);
        newCard.transform.SetParent(dealerCardTransforms[cardsList.Count - 1], true);

        Sequence drawSeq = DOTween.Sequence();

        drawSeq.Append(newCard.MoveToPosition(dealerCardTransforms[cardsList.Count - 1].transform));

        if (!isBack) drawSeq.Append(newCard.Flip());

        //drawSeq.OnComplete(() => { CheckAce(newCard); });
        drawSeq.AppendCallback(() => { CheckAce(newCard); });

        return drawSeq;
    }

    public void CheckAce(Card drawCard)
    {
        if(drawCard.numberAsString == "A")
        {
            if(shownNumber + 10 < 21)
            {
                drawCard.TreatAceAs11();
            }
        }
    }

    public void OnDealerTurn()
    {
        Sequence flipSeq = DOTween.Sequence();

        foreach(Card card in cardsList)
        {
            if(card.isBack)
            {
                flipSeq.Append(card.Flip());
            }
        }

        flipSeq.OnComplete(() => TurnRecursive());
    }

    private void TurnRecursive()
    {
        if(totalNumber  < stayCap)
        {
            Sequence drawSeq = DrawCard(isBack: false);

            drawSeq.OnComplete(() => { TurnRecursive(); });
        }
        else if(totalNumber > CONSTANT.DEALER_BURST_CAP)
        {
            Sequence burstSeq = DOTween.Sequence();

            foreach(Chip chip in GameManager.Instance.player.chipsList)
            {
                Sequence seq = chip.SO.OnDealerBurst();
                if(seq != null)
                {
                    burstSeq.Append(seq);
                    burstSeq.Join(chip.Highlight());
                }
            }

            burstSeq.OnComplete(() => GameManager.Instance.CalculateRoundResult(isDealerBurst:true));
        }
        else
        {
            GameManager.Instance.CalculateRoundResult(isDealerBurst:false);
        }
    }
}
