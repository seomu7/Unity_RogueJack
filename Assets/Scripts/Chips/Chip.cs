using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Chip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ChipSO SO;
    public Image image;
    public string ID;

    public void Initialization()
    {
        if(SO.chipImage != null)
        {
            image.sprite = SO.chipImage;
        }
        ID = SO.chipID;
    }

    public Sequence MoveToPosition(Transform tran)
    {
        Sequence seq = DOTween.Sequence();

        return seq.Append(transform.DOMove(tran.position, 1).SetEase(Ease.OutExpo));
    }

    public Sequence Highlight()
    {
        Sequence highlightSeq = DOTween.Sequence();

        highlightSeq.Append(this.transform.DOPunchScale(Vector3.one * 0.2f, 0.4f, 1));
        highlightSeq.Join(this.transform.DOPunchRotation(new Vector3(0, 0, 20f), 0.4f, 1));

        return highlightSeq;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipMaster.Instance.ShowChipTooltip(SO.chipName, SO.chipDescription, transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipMaster.Instance.HideChipTooltip();
    }
}
