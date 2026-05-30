using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChipMaster : MonoBehaviour
{
    private static ChipMaster _instance;
    public static ChipMaster Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ChipMaster>();
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
        else if(_instance == this)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        Initialize();
    }

    private Dictionary<string, Chip> chipPool = new Dictionary<string, Chip>();

    [SerializeField, Header("ChipLists")]
    private List<ChipSO> fullChipList = new List<ChipSO>();
    [SerializeField]
    private List<ChipSO> commonChipList = new List<ChipSO>();
    [SerializeField]
    private List<ChipSO> uncommonChipList = new List<ChipSO>();
    [SerializeField]
    private List<ChipSO> rareChipList = new List<ChipSO>();

    public Chip chipPrefab;

    [Header("For Debug")]
    public Chip emptyChip;
    public ChipSO emptyChipSO;
    
    public List<ChipSO> ReturnFullChipList()
    {
        return fullChipList;
    }

    public void Initialize()
    {
        uncommonChipList.Clear();
        commonChipList.Clear();
        rareChipList.Clear();

        foreach(ChipSO so in fullChipList)
        {
            switch (so.chipRarity)
            {
                case ChipRarity.Common:
                    commonChipList.Add(so);
                    break;

                case ChipRarity.Uncommon:
                    uncommonChipList.Add(so);
                    break;

                case ChipRarity.Rare:
                    rareChipList.Add(so);
                    break;
            }
        }

        StaticCalculator.Shuffle<ChipSO>(commonChipList);
        StaticCalculator.Shuffle<ChipSO>(uncommonChipList);
        StaticCalculator.Shuffle<ChipSO>(rareChipList);
    }

    public ChipSO GetChipSOFromList()
    {
        //Random probability part
        List<int> prob = new List<int> { 70, 20, 10 };

        int a = Random.Range(0, 100);

        Debug.Log(a);

        int which = 0;

        for(int index = 0; index < prob.Count; index++)
        {
            if(a <  prob[index])
            {
                which = index;
                break;
            }

            a -= prob[index];
        }

        //So get chip

        ChipSO SO;

        if(which == 0)
        {
            SO = GetSOFromCommon();
        }
        else if(which == 1)
        {
            SO = GetSOFromUncommon();
        }
        else if(which == 2)
        {
            SO = GetSOFromRare();
        }
        else
        {
            SO = emptyChipSO;
        }

        Debug.Log(SO.chipName);

        return SO;
    }

    public Chip InstantiateChip(ChipSO SO)
    {
        chipPrefab.SO = SO;

        string ID = SO.chipID;

        if (!chipPool.TryGetValue(ID, out Chip newChip))
        {
            newChip = Instantiate(chipPrefab);
            chipPool.TryAdd(ID, newChip);
            newChip.gameObject.SetActive(true);
            Debug.Log("Chip Generated: " + ID);
        }
        else
        {
            newChip.gameObject.SetActive(true);
            Debug.Log("Chip from Pool: " + ID);
        }

        newChip.Initialization();
        newChip.gameObject.name = ID;

        return newChip;
    }

    private ChipSO GetSOFromCommon()
    {
        if(commonChipList.Count != 0)
        {
            ChipSO SO = commonChipList[0];
            commonChipList.RemoveAt(0);
            return SO;
        }

        return emptyChipSO;
    }

    private ChipSO GetSOFromUncommon()
    {
        if(uncommonChipList.Count != 0)
        {
            ChipSO SO = uncommonChipList[0];
            uncommonChipList.RemoveAt(0);
            return SO;
        }

        return GetSOFromCommon();
    }

    private ChipSO GetSOFromRare()
    {
        if (rareChipList.Count != 0)
        {
            ChipSO SO = rareChipList[0];
            rareChipList.RemoveAt(0);
            return SO;
        }

        return GetSOFromUncommon();
    }
}
