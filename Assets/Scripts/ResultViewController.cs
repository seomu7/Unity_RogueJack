using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum GameResult { Win, Lose, Burst, DealerBurst}

public class ResultViewController : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI scoreText;
    public SelectChipController chipSelectBtn_0;
    public SelectChipController chipSelectBtn_1;
    public SelectChipController chipSelectBtn_2;
    public TextMeshProUGUI explainText;
    public TextMeshProUGUI networkErrorText;
    public TextMeshProUGUI highScoreMark;

    public Button uploadScoreBtn;
    public Button restartBtn;
    public Button leaderBoardBtn;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI rankingText;

    public UploadController uploadController;
    public LeaderBoardController leaderBoardController;

    private void Start()
    {
        restartBtn.onClick.AddListener(RestartGame);
        uploadScoreBtn.onClick.AddListener(OnUploadClick);
        leaderBoardBtn.onClick.AddListener(OnLeaderBoardClick);
    }

    public void SetResult(GameResult result, int player_number = 0, int dealer_number = 0)
    {
        this.gameObject.SetActive(true);

        if (result == GameResult.Win)
        {
            resultText.text = "이김 " + player_number +" " +dealer_number;
        }

        else if(result == GameResult.Lose)
        {
            resultText.text = "짐 " + player_number + " " + dealer_number;
        }

        else
        {
            resultText.text = "버스트 됨";
        }

        chipSelectBtn_0.SetChipInfo( ChipMaster.Instance.GetChipSOFromList());
        chipSelectBtn_1.SetChipInfo(ChipMaster.Instance.GetChipSOFromList());
        chipSelectBtn_2.SetChipInfo(ChipMaster.Instance.GetChipSOFromList());

    }

    public void SetResult(int round, GameResult result, int player_number = 0, int dealer_number = 0)
    {
        this.gameObject.SetActive(true);

        string resultString = "Round " + round;

        if (result == GameResult.Win)
        {
            this.resultText.text = resultString + " 승리!";
            scoreText.text = "딜러 " + dealer_number + " vs 플레이어 " + player_number;
        }
        else if(result == GameResult.Lose)
        {
            this.resultText.text = resultString + " 패배 ㅠ";
            scoreText.text = "딜러 " + dealer_number+ " vs 플레이어 " + player_number;
        }
        else if(result == GameResult.Burst)
        {
            this.resultText.text = resultString + " 패배 ㅠ";
            scoreText.text = "플레이어 버스트";
        }
        else if(result == GameResult.DealerBurst)
        {
            this.resultText.text = resultString + " 승리!";
            scoreText.text = "딜러 버스트 vs 플레이어 " + player_number;
        }

        Sequence showSeq = DOTween.Sequence();

        showSeq.AppendInterval(1.0f)
                .AppendCallback(() => { resultText.gameObject.SetActive(true); })
                .AppendInterval(1.0f)
                .AppendCallback(() => scoreText.gameObject.SetActive(true))
                .AppendInterval(1.0f);

        if (result == GameResult.Win || result == GameResult.DealerBurst)
        {
            showSeq.Append(GameManager.Instance.scoreBoardController.AddScore(round * 100));    
        }

        if (round < CONSTANT.DEMO_MAX_ROUND)
        {
            chipSelectBtn_0.SetChipInfo(ChipMaster.Instance.GetChipSOFromList());
            chipSelectBtn_1.SetChipInfo(ChipMaster.Instance.GetChipSOFromList());
            chipSelectBtn_2.SetChipInfo(ChipMaster.Instance.GetChipSOFromList());

            showSeq
                .AppendCallback(() =>
                {
                    chipSelectBtn_0.gameObject.SetActive(true);
                    chipSelectBtn_1.gameObject.SetActive(true);
                    chipSelectBtn_2.gameObject.SetActive(true);
                    explainText.gameObject.SetActive(true);
                });
        }

        else
        {
            showSeq
                .AppendCallback(() =>
                {
                    finalScoreText.text = "최종점수: " + GameManager.Instance.scoreBoardController.score;

                    finalScoreText.gameObject.SetActive(true);

                    if(HighScoreSaveManager.Instance.IsHighScore(GameManager.Instance.scoreBoardController.score,
                        GameManager.Instance.player.chipsList))
                    {
                        highScoreMark.gameObject.SetActive(true);
                    }
                })
                .AppendInterval(1.0f)
                .AppendCallback(() => { 
                    restartBtn.gameObject.SetActive(true);
                    uploadScoreBtn.gameObject.SetActive(true);
                    leaderBoardBtn.gameObject.SetActive(true);
                });
        }
    }

    public void SetActiveFalse()
    {
        chipSelectBtn_0.gameObject.SetActive(false);
        chipSelectBtn_1.gameObject.SetActive(false);
        chipSelectBtn_2.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        resultText.gameObject.SetActive(false);
        explainText.gameObject.SetActive(false);
        networkErrorText.gameObject.SetActive(false);

        restartBtn.gameObject.SetActive(false);
        uploadScoreBtn.gameObject.SetActive(false);
        leaderBoardBtn.gameObject.SetActive(false);
        finalScoreText.gameObject.SetActive(false);
        rankingText.gameObject.SetActive(false);

        leaderBoardController.gameObject.SetActive(false);

        EnableUploadBtn();

        this.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        /*resultText.gameObject.SetActive(false);
        scoreText .gameObject.SetActive(false);
        restartBtn.gameObject.SetActive(false);
        uploadScoreBtn.gameObject.SetActive(false);
        leaderBoardBtn.gameObject.SetActive(false);
        finalScoreText.gameObject.SetActive(false);
        highScoreMark.gameObject.SetActive(false);
        gameObject.SetActive(false);*/

        SetActiveFalse();

        GameManager.Instance.StartGameSetting();
        GameManager.Instance.StartGame();
    }

    public void OnUploadClick()
    {
        uploadController.gameObject.SetActive(true);
    }

    public void DisalbeUploadBtn()
    {
        uploadScoreBtn.interactable = false;
    }

    public void EnableUploadBtn()
    {
        uploadScoreBtn.interactable = true;
    }

    public void SetRankingText(int score)
    {
        rankingText.text = "현재순위: " + score.ToString() + "!!";
        rankingText.gameObject.SetActive(true);
    }

    public void OnLeaderBoardClick()
    {
        StartCoroutine(GameManager.Instance.jetsonNano.GetRanking(leaderBoardController));
    }

    public void ShowNetworkErrorText()
    {
        networkErrorText.gameObject.SetActive(true);
    }

    /*private IEnumerator StartGetRanking()
    {
        yield return StartCoroutine(GameManager.Instance.jetsonNano.GetRanking(leaderBoardController));
        leaderBoardController.ToggleActive();
    }*/
}
