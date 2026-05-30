using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardSO cardData;

    public Image image;
    public Sprite backSprite;
    public CardShape shape;
    public string numberAsString;
    public int number;
    public string ID;

    public bool isBack;
    public bool isFace { get
        { return (numberAsString == "Q" || numberAsString =="K" || numberAsString =="J"); } 
    }

    private float flipDuration = 0.5f;

    private void OnEnable()
    {
        Initialize();
    }

    private void OnDisable()
    {
        TreatAceAs1();
    }

    public void Initialize()
    {
        backSprite = CardMaster.Instance.masterBackSprite[0];
        image.sprite = backSprite;
        shape = cardData.cardShape;
        number = cardData.cardNumber;
        numberAsString = cardData.cardNumberAsString;
        this.isBack = true;
        ID = cardData.cardID;

        foreach (Chip chip in GameManager.Instance.player.chipsList)
        {
            chip.SO.OnCardCreated(this);
        }
    }

    public void TreatAceAs1()
    {
        if (number != 1 && number != 11) return;

        number = 1;
    }

    public void TreatAceAs11()
    {
        if (number != 1 && number != 11) return;

        number = 11;
    }

    public Sequence Flip()
    {
        Sequence flipSeq = DOTween.Sequence();

        return flipSeq.Append(transform.DOScaleX(0f, flipDuration).SetEase(Ease.InQuad))
            .AppendCallback(() =>
            {
                isBack = !isBack;
                image.sprite = isBack ? backSprite : cardData.cardImage;
            })
            .Append(transform.DOScaleX(1f, flipDuration).SetEase(Ease.OutBack));
    }

    public Tween MoveToPosition(Transform tran)
    {
        Sequence seq = DOTween.Sequence();

        return seq.Append(transform.DOMove(tran.position, 1).SetEase(Ease.OutExpo));
    }

    public Sequence Highlight()
    {
        Sequence highlightSeq = DOTween.Sequence();

        highlightSeq.Append(this.transform.DOPunchScale(Vector3.one * 0.2f, 0.4f));
        highlightSeq.Join(this.transform.DOPunchRotation(new Vector3(0, 0, 20f), 0.4f));

        return highlightSeq;
    }

    [Obsolete]
    public IEnumerator FlipCoroutine()
    {
        Sequence flipSeq = DOTween.Sequence();

        flipSeq.Append(transform.DOScaleX(0f, flipDuration).SetEase(Ease.InQuad))
            .AppendCallback(() =>
            {
                isBack = !isBack;
                image.sprite = isBack ? backSprite : cardData.cardImage;
            })
            .Append(transform.DOScaleX(1f, flipDuration).SetEase(Ease.OutBack));

        yield return flipSeq.WaitForCompletion();
    }

    [Obsolete]
    public IEnumerator MoveToPositionCoroutine(Transform trans)
    {
        transform.SetParent(trans, true);

        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOMove(trans.position, 1).SetEase(Ease.OutExpo));

        yield return seq.WaitForCompletion();
    }
}
