using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null )
            {
                _instance = FindObjectOfType<GameManager>();
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
        else if (_instance == this)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [Header("User")]
    public Player player;
    public Dealer dealer;

    public ResultViewController resultViewController;
    public ScoreBoardController scoreBoardController;
    public StartViewController startViewController;
    public ChipPediaView pediaView;

    [Header("GameInfo")]
    public int round = 0;

    [Header("View Transform")]
    public List<Transform> dealerCardTransforms = new List<Transform>();
    public List<Transform> playerCardTransforms = new List<Transform>();
    public List<Transform> playerChipTransforms = new List<Transform>();

    //Strategies
    public CalculateWinnerStrategy CW_Startegy = new CW_Default();

    private void InitializeStrategy()
    {
        CW_Startegy = new CW_Default();
    }

    public void StartGameSetting()
    {
        round = 0;
        scoreBoardController.gameObject.SetActive(true);
        player.ResetGame();
        dealer.ResetGame();
        scoreBoardController.ResetGame();
        ChipMaster.Instance.Initialize();
    }

    public void StartGame()
    {
        OnRoundStart();
    }

    public void OnRoundStart()
    {
        round++;

        CardMaster.Instance.ReturnNormalDeck();

        foreach (Chip chip in player.chipsList)
        {
            chip.SO.OnDeckCreated();
        }

        CardMaster.Instance.ShuffleCurrentDeck();

        CardMaster.Instance.DrawNewTopCard();

        Sequence mainSeq = DOTween.Sequence();
        mainSeq.Append(dealer.DrawCard(isBack:false))
            .Append(dealer.DrawCard(isBack:true))
            .Append(player.DrawCard())   
            .Append(player.DrawCard())
            .OnComplete(() => OnPlayerTurn());
    }

    public void OnPlayerTurn()
    {
        player.OnPlayerTurnStart();
    }

    public void Hit()
    {
        player.Hit();
    }

    public void Stay()
    {
        Sequence staySeq = DOTween.Sequence();

        foreach (Chip chip in player.chipsList)
        {
            Sequence seq = chip.SO.OnStaySequence();
            if (seq != null)
            {
                staySeq.Append(seq);
                staySeq.Join(chip.Highlight());
            }
        }

        staySeq.OnComplete(() => {
            player.OnPlayerTurnEnd();
            dealer.OnDealerTurn();
        });
    }

    public void Bursted()
    {
        resultViewController.SetResult(round, GameResult.Burst);
    }

    public void CalculateRoundResult(bool isDealerBurst)
    {
        int dealer_number = dealer.totalNumber;
        int player_number = player.ReturnTotalMaxNumber();

        GameResult roundResult = CW_Startegy.ReturnRoundWinner(player_number, dealer_number);

        if(isDealerBurst)
        {
            resultViewController.SetResult(round, GameResult.DealerBurst, player_number);
        }
        /*else if (dealer_number >= player_number && dealer_number <=21)
        {
            resultViewController.SetResult(round, GameResult.Lose, player_number, dealer_number);
        }*/
        else resultViewController.SetResult(round, roundResult, player_number, dealer_number);
    }

    public void OnRoundEnd()
    {
        Sequence roundSeq = DOTween.Sequence();

        foreach(Chip chip in player.chipsList)
        {
            Sequence seq = chip.SO.OnRoundEndSequence();
            if(seq != null)
            {
                roundSeq.Append(seq);
                roundSeq.Join(chip.Highlight());
            }
        }

        roundSeq.OnComplete(() =>
        {
            player.ResetRound();
            dealer.ResetRound();
            CardMaster.Instance.ReturnTopDrawCardToCardPool();
            OnRoundStart();
        });
    }

   /* private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 50), "Test"))
        {
            OnRoundStart();
        }

        if (GUI.Button(new Rect(0, 200, 100, 50), "OnSelectChip"))
        {
            foreach(Chip chip in player.chipsList)
            {
                chip.SO.OnSelectChip();
                chip.Highlight();
            }
        }
    }*/
}
