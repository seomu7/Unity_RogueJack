using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectChipController : MonoBehaviour
{
    [SerializeField]
    private Image chipImage;
    [SerializeField]
    private TextMeshProUGUI chipName;
    [SerializeField]
    private TextMeshProUGUI chipDescription;
    private Button btn;
    private ResultViewController resultViewController;
    private ChipSO SO_Container;

    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        resultViewController = GetComponentInParent<ResultViewController>();
        btn.onClick.AddListener(OnSelectChip);
    }

    public void SetChipInfo(ChipSO SO)
    {
        SO_Container = SO;
        chipImage.sprite = SO.chipImage;
        chipName.text = SO.chipName;
        chipDescription.text = SO.chipDescription;
    }

    private void OnSelectChip()
    {
        Sequence seq = SelectChipSequnce(SO_Container);

        seq.OnComplete(() =>
        {
            GameManager.Instance.OnRoundEnd();
            resultViewController.SetActiveFalse();
        });
        
    }

    public Sequence SelectChipSequnce(ChipSO SO)
    {
        Chip newChip = ChipMaster.Instance.InstantiateChip(SO);
        List<Chip> chipList = GameManager.Instance.player.chipsList;
        chipList.Add(newChip);

        newChip.transform.position = chipImage.transform.position;
        newChip.transform.SetParent(GameManager.Instance.playerChipTransforms[chipList.Count - 1], true);
        newChip.transform.localScale = Vector3.one;

        Sequence selectSeq = DOTween.Sequence();

        selectSeq
            .Append(newChip.MoveToPosition(GameManager.Instance.playerChipTransforms[chipList.Count - 1].transform));

        newChip.SO.OnSelectChip();

        return selectSeq;
    }

}
