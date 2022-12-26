using UnityEngine;

public class PanManager : MonoBehaviour
{
    public static PanManager instance;


    [field: SerializeField]
    public int NumberOfRolls { get; private set; } = 0;

    [SerializeField] Transform captureSlot;
    [SerializeField] PanSlot[] panSlots;
    Flavour.flavourType flavourType;
    Transform[] flavours = new Transform[3];
    bool isFlavoured;

    [Header("Debug")]
    [SerializeField] bool[] isEmpty = new bool[3];


    #region Unity CallBack Functions
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        InitPanSlots();
        captureSlot = panSlots[0].transform;
        flavourType = Flavour.flavourType.none;
        InitFlavourPrefab();
    }

    void Update()
    {
        GetFlavourFollowingRoll();
        DebugSlot();
    }
    #endregion

    #region Acquire Rolls or Flavour
    public void AcquireRoll(Roll.rollType rollType)
    {
        PushRolls();

        // 가장 아래 비어 있는 슬롯에 캡쳐한 롤 집어 넣기
        RollSo rollSo = RecipeRoll.instance.GetRollSo(rollType);

        GameObject _roll = Instantiate(rollSo.rollPrefab, captureSlot.position, captureSlot.rotation);
        panSlots[0].AddRoll(_roll.transform);

        UpdateSlots();
    }
    public void AcquireFlavour(Flavour.flavourType flavourType)
    {
        foreach (var item in panSlots) // isFlavoured 가능. 적어도 하나의 슬롯이 비어 있지 않다면
        {
            if (!item.IsEmpty) 
                isFlavoured = true;
        }

        if (!isFlavoured) // 슬롯이 다 비어 있다면 Flavoured 될 수 없다.
            return;

        DestroyFlavourPrefab();
        CreateFlavourPrefab(flavourType);
    }
    #endregion

    #region Slot Actions
    void InitPanSlots()
    {
        panSlots = GetComponentsInChildren<PanSlot>();
        CountRolls();
    }
    void PushRolls()
    {
        //꼭대기 슬롯 정리
        if (panSlots[panSlots.Length - 1].IsEmpty == false)
        {
            panSlots[panSlots.Length - 1].DumpRoll();
        }

        //나머지 슬롯 정리
        for (int i = panSlots.Length - 2; i > -1; i--)
        {
            if (panSlots[i].IsEmpty == false)
            {
                panSlots[i].MoveRoll(panSlots[i + 1]);
            }
        }
        UpdateSlots();
    }
    void PullRolls()
    {
        for (int i = 0; i < panSlots.Length - 1; i++)
        {
            if (panSlots[i + 1].IsEmpty == false)
            {
                panSlots[i + 1].MoveRoll(panSlots[i]);
            }
        }
        UpdateSlots();
    }

    public void ReleaseRoll()
    {
        flavours[0].GetChild(0).transform.SetParent(panSlots[0].GetRoll().transform);
        panSlots[0].ReleaseRoll();
        PullRolls();

        DestroyFlavourPrefab();
        CreateFlavourPrefab(flavourType);
    }

    void CreateFlavourPrefab(Flavour.flavourType flavourType)
    {
        this.flavourType = flavourType;

        FlavourSo flavourSo = GetFlavourSo(flavourType);
        for (int i = 0; i < panSlots.Length; i++)
        {
            if (panSlots[i].IsEmpty == false)
            {
                Transform targetTransform = panSlots[i].transform;
                GameObject flavourPrefab = Instantiate(flavourSo.flavourPrefab, targetTransform.position, targetTransform.rotation);
                flavourPrefab.transform.SetParent(flavours[i].transform);
            }
        }
    }
    void GetFlavourFollowingRoll()
    {
        for (int i = 0; i < panSlots.Length; i++)
        {
            if (panSlots[i].IsEmpty == false)
                flavours[i].position = panSlots[i].transform.position;
        }
    }
    void DestroyFlavourPrefab()
    {
        for (int i = 0; i < panSlots.Length; i++)
        {
            if (flavours[i].GetChild(0) != null)
            {
                //flavours[i].GetChild(0).transform.SetParent(null);
                //Destroy(flavours[i].GetChild(0));
                Debug.Log(flavours[i].GetChild(0).name);
            }
        }
    }
    void InitFlavourPrefab()
    {
        for (int i = 0; i < flavours.Length; i++)
        {
            flavours[i] = panSlots[i].transform;
        }
        
        flavourType = Flavour.flavourType.none;
        FlavourSo flavourSo = GetFlavourSo(flavourType);

        for (int i = 0; i < panSlots.Length; i++)
        {
            Transform targetTransform = panSlots[i].transform;
            GameObject flavourPrefab = Instantiate(flavourSo.flavourPrefab, targetTransform.position, targetTransform.rotation);
            flavourPrefab.transform.SetParent(flavours[i]);
        }
    }
    FlavourSo GetFlavourSo(Flavour.flavourType flavourType)
    {
        return RecipeFlavour.instance.GetFlavourSo(flavourType);
    }
    #endregion

    #region Update Slots
    void UpdateSlots()
    {
        SetSortingLayers();
        CountRolls();
    }
    void SetSortingLayers()
    {
        for (int i = 0; i < panSlots.Length; i++)
        {
            if (panSlots[i].IsEmpty == false)
            {
                panSlots[i].GetRoll().GetComponent<SpriteRenderer>().sortingOrder = panSlots.Length - i;
            }
        }
    }
    void CountRolls()
    {
        for (int i = 0; i < panSlots.Length; i++)
        {
            if (panSlots[i].IsEmpty)
            {
                NumberOfRolls = i;
                return;
            }
        }
        NumberOfRolls = panSlots.Length;
    }
    void DebugSlot()
    {
        for (int i = 0; i < isEmpty.Length; i++)
        {
            isEmpty[i] = panSlots[i].IsEmpty;
        }
    }
    #endregion
}
