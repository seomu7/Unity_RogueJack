using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardShape { Spade, Diamond, Heart, Club }

public class CardSO : ScriptableObject
{
    [field:SerializeField]
    public Sprite cardImage { get; private set; }
    [field: SerializeField]
    public CardShape cardShape { get; private set; }
    [field: SerializeField]
    public int cardNumber { get; private set; }
    [field: SerializeField]
    public string cardNumberAsString { get; private set; }
    [field: SerializeField]
    public string cardID { get; private set; }

    public void Initialize(Sprite cardImage, CardShape cardShape, int cardNumber, string cardNumberAsString)
    {
        this.cardImage = cardImage;
        this.cardShape = cardShape;
        this.cardNumber = cardNumber;
        this.cardNumberAsString = cardNumberAsString;

        this.cardID = cardShape.ToString() + cardNumberAsString;
    }
}
