using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI firstText;
    [SerializeField]
    private TextMeshProUGUI secondText;
    [SerializeField]
    private TextMeshProUGUI thirdText;

    public void ChangeFirstText(string txt)
    {
        firstText.text = txt;
    }

    public void ChangeSecondText(string txt)
    {
        secondText.text = txt;
    }

    public void ChangeThirdText(string txt)
    {
        thirdText.text = txt;
    }

    public void ToggleActive()
    {
        if (this.gameObject.activeSelf) this.gameObject.SetActive(false);
        else this.gameObject.SetActive(true);
    }

    public void SetActiveTrue()
    {
        this.gameObject.SetActive(true);
    }

    public void SetActiveFalse()
    {
        this.gameObject.SetActive(false);
    }
}
