using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TooltipMaster : MonoBehaviour
{
    private static TooltipMaster _instance;
    public static TooltipMaster Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<TooltipMaster>();
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

    public GameObject tooltipPanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public RectTransform panelRect;

    public void HideChipTooltip()
    {
        tooltipPanel.gameObject.SetActive(false);
    }

    public void ShowChipTooltip(string chipName, string chipDescription, Vector3 position)
    {
        nameText.text = chipName;
        descriptionText.text = chipDescription;

        tooltipPanel.SetActive(true);

        LayoutRebuilder.ForceRebuildLayoutImmediate(panelRect);

        UpdatePivot(position);

        tooltipPanel.transform.position = position;
    }

    public void UpdatePivot(Vector2 mousePosition)
    {
        float xRatio = mousePosition.x / Screen.width;
        float yRatio = mousePosition.y / Screen.height;

        float pivotX = xRatio < 0.5f ? 0f : 1f;
        float pivotY = yRatio < 0.5f ? 0f : 1f;

        panelRect.pivot = new Vector2(pivotX, pivotY);
    }
}
