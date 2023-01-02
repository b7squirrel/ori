using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 비어 있는지 여부를 GetRoll로 체크하면 null일 경우 에러가 뜬다
/// IsEmpty로 체크하면 된다
/// </summary>
public class PanManager : MonoBehaviour
{
    public static PanManager instance;

    [field: SerializeField]
    public int NumberOfRolls { get; private set; } = 0;

    [SerializeField] Transform captureSlot;
    [SerializeField] PanSlot[] panSlots;
    [SerializeField] PanData pandata;
    Flavour.flavourType flavourType;
    List<GameObject> flavours = new List<GameObject>();
    bool isFlavoured;
    Coroutine flavourLifeCo;

    [SerializeField] Transform hitRollPoint;

    [Header("Effects")]
    [SerializeField] Material whiteMat;
    [SerializeField] float whiteFlashDuration;
    SpriteRenderer[] rollSR = new SpriteRenderer[3];

    [Header("Debug")]
    [SerializeField] bool[] isEmpty = new bool[3];

    [SerializeField] Player player;


    #region Unity CallBack Functions
    void Awake()
    {
        instance = this;
        player = GetComponentInParent<Player>();
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
        if (isFlavoured)
        {
            DestroyFlavourPrefab();
            CreateFlavourPrefab(flavourType);
        }
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
        if (flavourLifeCo != null)
            StopCoroutine(flavourLifeCo);
        StartFlavourLife();
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
        DestroyFlavourPrefab();

        Transform roll = panSlots[0].transform;
        FlavourSo flavourSo = GetFlavourSo(this.flavourType);
        GameObject flavourPrefab = Instantiate(flavourSo.flavourPrefab, roll.position, roll.rotation);
        flavourPrefab.transform.SetParent(panSlots[0].GetRoll().transform);

        RollHealth rollHealth = panSlots[0].GetRoll().GetComponent<RollHealth>();

        rollHealth.CountLife();
        rollHealth.FlavourType = this.flavourType;

        float movementInputDir = player.InputHandler.MovementInput.x;
        float playerVelocity = player.playerData.movementVelocity;

        panSlots[0].GetRoll().transform.position = hitRollPoint.position;
        panSlots[0].GetRoll().GetComponent<IRollEffect>().WhiteFlash(0f); // white flash
        panSlots[0].ReleaseRoll(movementInputDir, playerVelocity);
        PullRolls();

        CreateFlavourPrefab(flavourType);

        HitRollEffect();
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
                flavours.Add(flavourPrefab);
            }
        }
    }
    void GetFlavourFollowingRoll()
    {
        for (int i = 0; i < flavours.Count; i++)
        {
            flavours[i].transform.position = panSlots[i].transform.position;
        }
    }
    void DestroyFlavourPrefab()
    {
        GameObject[] flavourTemp = new GameObject[flavours.Count];
        for (int i = 0; i < flavourTemp.Length; i++)
        {
            flavourTemp[i] = flavours[i];
        }
        flavours.Clear();
        foreach (var item in flavourTemp)
        {
            Destroy(item);
        }
    }
    void InitFlavourPrefab()
    {
        flavourType = Flavour.flavourType.none;
        FlavourSo flavourSo = GetFlavourSo(flavourType);
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
        ResetRollScale();
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

    void ResetRollScale()
    {
        foreach (var item in panSlots)
        {
            if (item.IsEmpty == false)
            {
                item.GetRoll().transform.localScale = new Vector3(1, 1, 1);
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

    #region Count Flavour Life
    void StartFlavourLife()
    {
        flavourLifeCo = StartCoroutine(StartFlavourLifeCo());
    }
    IEnumerator StartFlavourLifeCo()
    {
        yield return new WaitForSeconds(pandata.flavourLife);
        isFlavoured = false;
        flavourType = Flavour.flavourType.none;
        DestroyFlavourPrefab();
    }
    #endregion

    #region Effects
    void HitRollEffect()
    {
        Instantiate(pandata.hitRollSpark, hitRollPoint.position, hitRollPoint.rotation);
    }
    #endregion
}
