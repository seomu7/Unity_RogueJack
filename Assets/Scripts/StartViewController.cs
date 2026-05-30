using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartViewController : MonoBehaviour
{
    [SerializeField]
    private Image fadeImage;
    [SerializeField]
    private Image gameNameImage;
    private Button startBtn;
    private Button pediaBtn;
    private Button exitBtn;

    public float fadeDuration = 1.0f;
    public float intervalDurtaion = 1.0f;

    private void Awake()
    {
        DOTween.Init();

        startBtn = GetComponentsInChildren<Button>()[0];
        pediaBtn = GetComponentsInChildren<Button>()[1];
        exitBtn = GetComponentsInChildren<Button>()[2];

        startBtn.onClick.AddListener(OnStartClick);
        pediaBtn.onClick.AddListener(OnPediaClick);
        exitBtn.onClick.AddListener(OnExitClick);
    }

    private void OnStartClick()
    {
        startBtn.interactable = false;

        fadeImage.fillAmount = 0f;
        fadeImage.fillClockwise = true;
        fadeImage.gameObject.SetActive(true);

        Sequence seq = DOTween.Sequence();

        seq.Append(fadeImage.DOFillAmount(1f, fadeDuration).SetEase(Ease.InOutQuint))
            .AppendCallback(() =>
            {
                startBtn.gameObject.SetActive(false);
                gameNameImage.gameObject.SetActive(false);
                pediaBtn.gameObject.SetActive(false);
                exitBtn.gameObject.SetActive(false);
                GameManager.Instance.StartGameSetting();
            })
            .AppendInterval(intervalDurtaion)
            .AppendCallback(() =>
            {
                fadeImage.fillClockwise = false;
            })
            .Append(fadeImage.DOFillAmount(0f, fadeDuration).SetEase(Ease.InOutQuint))
            .AppendInterval(intervalDurtaion)
            .OnComplete(() =>
            {
                fadeImage.gameObject.SetActive(false);
                GameManager.Instance.StartGame();
            });
    }

    private void OnPediaClick()
    {
        GameManager.Instance.pediaView.gameObject.SetActive(true);
        GameManager.Instance.pediaView.GeneratePedia();
    }

    private void OnExitClick()
    {
        Application.Quit();
    }
}