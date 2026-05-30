using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChipPediaView : MonoBehaviour
{
    public GameObject sectionPrefab;
    public Chip chipPrefab;
    public Transform contentRoot;

    private Button backBtn;

    private void Awake()
    {
        backBtn = GetComponentInChildren<Button>();

        backBtn.onClick.AddListener(OnBackClick);
    }

    public void GeneratePedia()
    {
        foreach(Transform child in  contentRoot) Destroy(child.gameObject);

        var groupedChips = ChipMaster.Instance.ReturnFullChipList()
            .OrderBy(r =>r.chipRarity)
            .ThenBy(r => r.chipName)
            .GroupBy(r=>r.chipRarity);

        foreach(var group in groupedChips)
        {
            GameObject section = Instantiate(sectionPrefab, contentRoot);

            var rarityText = section.GetComponentInChildren<TextMeshProUGUI>();
            rarityText.text = group.Key.ToString();

            Transform gridTransform = section.transform.Find("GridView");

            foreach(var chipSO in group)
            {
                /*for(int i = 0; i<10; i++)
                {
                    chipPrefab.SO = chipSO;
                    Chip chip = Instantiate(chipPrefab, gridTransform);
                    chip.Initialization();
                    chip.gameObject.name = chipSO.chipID;
                }*/
                Chip chip = ChipMaster.Instance.InstantiateChip(chipSO);
                chip.transform.SetParent(gridTransform, false);
            }
        }
    }

    private void OnBackClick()
    {
        gameObject.SetActive(false);
    }

    /*private void OnGUI()
    {
        if (GUI.Button(new Rect(500, 500, 100, 100), "pedia"))
        {
            GeneratePedia();
        }
    }*/
}
