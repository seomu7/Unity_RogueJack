using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class CardMaster : MonoBehaviour
{
    private static CardMaster _instance;
    public static CardMaster Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CardMaster>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(_instance == this)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        playerCardTransforms = GameManager.Instance.playerCardTransforms;
    }

    [SerializeField]
    private Transform topDrawCardPosition;
    private Card topDrawCard;
    public Card cardPrefab;

    [SerializeField]
    private List<CardSO> masterDeck = new List<CardSO>();
    private List<CardSO> currentDeck = new List<CardSO>();
    private Dictionary<string, Card> cardPool = new Dictionary<string, Card>();
    public List<Sprite> masterBackSprite = new List<Sprite>();

    private List<Transform> playerCardTransforms;

    public List<CardSO> ReturnNormalDeck()
    {
        currentDeck.Clear();

        foreach(CardSO data in masterDeck)
        {
            currentDeck.Add(data);
        }

        return currentDeck;
    }

    public List<CardSO> ReturnRedDeck()
    {
        currentDeck.Clear();

        foreach(CardSO data in masterDeck)
        {
            if(data.cardShape == CardShape.Diamond || data.cardShape == CardShape.Heart)
            {
                currentDeck.Add(data);
                currentDeck.Add(data);
            }
        }

        return currentDeck;
    }

    public List<CardSO> ReturnBlackDeck()
    {
        currentDeck.Clear();

        foreach (CardSO data in masterDeck)
        {
            if (data.cardShape == CardShape.Spade || data.cardShape == CardShape.Club)
            {
                currentDeck.Add(data);
                currentDeck.Add(data);
            }
        }

        return currentDeck;
    }

    public void ShuffleCurrentDeck()
    {
        currentDeck = StaticCalculator.Shuffle<CardSO>(currentDeck);
    }

    public Card GiveCardToDealer(bool isBack)
    {
        Card newCard = topDrawCard;
        topDrawCard = DrawNewTopCard();

        return newCard;
    }

    public Card GiveCardToPlayer()
    {
        Card newCard = topDrawCard;
        topDrawCard = DrawNewTopCard();

        return newCard;
    }

    public Card DrawNewTopCard()
    {
        CardSO so = currentDeck[0];
        cardPrefab.cardData = so;
        currentDeck.RemoveAt(0);

        string ID = so.cardID;
        //Try to get card from pool
        if(!cardPool.TryGetValue(ID, out Card newCard))
        {
            newCard = Instantiate(cardPrefab);
            cardPool.TryAdd(ID, newCard);
            Debug.Log("Card Generated: " + ID);
        }
        else
        {
            Debug.Log("Card from Pool: " + ID);
        }

        newCard.Initialize();

        newCard.gameObject.SetActive(true);
        newCard.gameObject.name = ID;

        topDrawCard = newCard;
        topDrawCard.gameObject.transform.SetParent(topDrawCardPosition, false);

        foreach(Chip chip in GameManager.Instance.player.chipsList)
        {
            chip.SO.OnTopCardDraw(topDrawCard);
        }

        return newCard;
    }

    public void ReturnToCardPool(List<Card> cardsList)
    {
        foreach(Card card in cardsList)
        {
            card.transform.SetParent(this.transform, false);
            card.gameObject.SetActive(false);
        }
    }

    public void ReturnToCardPool(Card card)
    {
        card.transform.SetParent(this.transform, false);
        card.gameObject.SetActive(false);
    }

    public void ReturnTopDrawCardToCardPool()
    {
        topDrawCard.transform.SetParent(this.transform, false);
        topDrawCard.gameObject.SetActive(false);
    }

    [Obsolete]
    public IEnumerator TempDraw()
    {
        Card newCard = topDrawCard;
        topDrawCard = DrawNewTopCard();

        /*Sequence drawSeq = DOTween.Sequence();

        drawSeq
            .AppendCallback(() => newCard.MoveToPositionCoroutine(playerCardTransforms[0]))
            .AppendCallback(() => newCard.FlipCoroutine());

        yield return drawSeq.WaitForCompletion();*/

        yield return StartCoroutine(newCard.MoveToPositionCoroutine(playerCardTransforms[0]));
        yield return StartCoroutine(newCard.FlipCoroutine());
    }

    private void OnGUI()
    {
       
    }
}
