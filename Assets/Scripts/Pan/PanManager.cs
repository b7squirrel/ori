using UnityEngine;

public class PanManager : MonoBehaviour
{
    public static PanManager instance;

    [SerializeField] Transform captureSlot;

    [field: SerializeField]
    public int NumberOfRolls { get; private set; } = 0;

    [SerializeField] PanSlot[] panSlots;
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
    }

    void Update()
    {
        for (int i = 0; i < isEmpty.Length; i++)
        {
            isEmpty[i] = panSlots[i].IsEmpty;
        }
    }
    #endregion

    #region Slot Actions
    void InitPanSlots()
    {
        panSlots = GetComponentsInChildren<PanSlot>();
        CountRolls();
    }
    public void AcquireRoll(Roll.rollType rollType)
    {
        PushRolls();

        // 가장 아래 비어 있는 슬롯에 캡쳐한 롤 집어 넣기
        RollSo rollSo = RecipeRoll.instance.GetRollSo(rollType);

        GameObject _roll = Instantiate(rollSo.rollPrefab, captureSlot.position, captureSlot.rotation);
        panSlots[0].AddRoll(_roll.transform);

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
                Debug.Log("Moved Roll in Slot[" + i + "] to Slot[" + (i + 1) + "]");
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
        panSlots[0].ReleaseRoll();
        PullRolls();
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
                panSlots[i].GetRoll().GetComponent<SpriteRenderer>().sortingOrder = i;
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
    #endregion
}
