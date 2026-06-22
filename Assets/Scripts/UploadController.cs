using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UploadController : MonoBehaviour
{
    [SerializeField]
    private Button okBtn;
    [SerializeField]
    private Button cancelBtn;

    [SerializeField]
    private TMP_InputField inputIDField;
    [SerializeField]
    private TextMeshProUGUI rankingText;

    [SerializeField]
    private ResultViewController resultViewController;

    private void Start()
    {
        resultViewController = GetComponentInParent<ResultViewController>();

        cancelBtn.onClick.AddListener(OnCancelClick);
        okBtn.onClick.AddListener(OnOkClick);
    }

    private void OnCancelClick()
    {
        this.gameObject.SetActive(false);
    }

    private void OnOkClick()
    {
        ScoreData scoreData = new ScoreData()
        {
            player_name = inputIDField.text.ToString(),
            score = GameManager.Instance.scoreBoardController.score
        };

        StartCoroutine(GameManager.Instance.jetsonNano.Submit(
            scoreData, (_score) => {
                resultViewController.DisalbeUploadBtn();
                resultViewController.SetRankingText(_score);
                this.gameObject.SetActive(false);
            }));
    }

    public void SetActiveFalse()
    {
        resultViewController.DisalbeUploadBtn();
        this.gameObject.SetActive(false);
    }
}
