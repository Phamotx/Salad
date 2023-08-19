using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerScore))]
public class PickingDropingChopping : MonoBehaviour
{
    [SerializeField]
    private GameObject[] pickedVegItem;
    [SerializeField]
    private GameObject[] pickedChoppedVegItem;
    [SerializeField]
    private GameObject plate;
    [SerializeField]
    private GameObject[] plateItem;
    [SerializeField]
    private Color[] itemColor;
    [SerializeField]
    private int numberOfItemVegPicked;
    [SerializeField]
    private Image barIndicator;
    [SerializeField]
    private float choppingTime;
    [SerializeField]
    private int RefnumberOfChoopedItem;

    private bool isPlatePicked;
    [SerializeField]
    private int pickedItemByPlayer;
    private bool haschoppedItem;
    private PlayerScore player;

    public int RefnumberOfChoopedItem1 { get => RefnumberOfChoopedItem; set => RefnumberOfChoopedItem = value; }
    public GameObject[] PickedVegItem { get => pickedVegItem; set => pickedVegItem = value; }
    public GameObject[] PickedChoppedVegItem { get => pickedChoppedVegItem; set => pickedChoppedVegItem = value; }
    public float ChoppingTime { get => choppingTime; set => choppingTime = value; }
    public int NumberOfItemVegPicked { get => numberOfItemVegPicked; set => numberOfItemVegPicked = value; }
    public bool IsPlatePicked { get => isPlatePicked; set => isPlatePicked = value; }
    public bool HaschoppedItem { get => haschoppedItem; set => haschoppedItem = value; }
    public int PickedItemByPlayer { get => pickedItemByPlayer; set => pickedItemByPlayer = value; }
    public GameObject[] PlateItem { get => plateItem; set => plateItem = value; }
    public PlayerScore Player { get => player; set => player = value; }
    public GameObject Plate { get => plate; set => plate = value; }

    private void Awake()
    {
        Player = this.GetComponent<PlayerScore>();
    }
    private void OnEnable()
    {
        EventManager.Instance.RegisterEvent(EventManager.eGameEvents.PickVeg, PickingVegItem);
        EventManager.Instance.RegisterEvent(EventManager.eGameEvents.PickPlate_1, PickingPlate);
        EventManager.Instance.RegisterEvent(EventManager.eGameEvents.DropPlate_1, DropingPlate);
        EventManager.Instance.RegisterEvent(EventManager.eGameEvents.DropVeg, DropingVegItem);
        EventManager.Instance.RegisterEvent(EventManager.eGameEvents.DropVegOnChoppingBoard_1, DropingVegItemChoppingBoard);
        EventManager.Instance.RegisterEvent(EventManager.eGameEvents.Chop_1, ChoppingVegItem);
        EventManager.Instance.RegisterEvent(EventManager.eGameEvents.PickingChoppedItem_1, PickingChoppedItemFromChoppingBoard);
        EventManager.Instance.RegisterEvent(EventManager.eGameEvents.PlaceOrder_1, DropingOrder);
    }
    private void OnDisable()
    {
        EventManager.Instance.DeRegisterEvent(EventManager.eGameEvents.PickVeg, PickingVegItem);
        EventManager.Instance.DeRegisterEvent(EventManager.eGameEvents.PickPlate_1, PickingPlate);
        EventManager.Instance.DeRegisterEvent(EventManager.eGameEvents.DropPlate_1, DropingPlate);
        EventManager.Instance.DeRegisterEvent(EventManager.eGameEvents.DropVeg, DropingVegItem);
        EventManager.Instance.DeRegisterEvent(EventManager.eGameEvents.DropVegOnChoppingBoard_1, DropingVegItemChoppingBoard);
        EventManager.Instance.DeRegisterEvent(EventManager.eGameEvents.Chop_1, ChoppingVegItem);
        EventManager.Instance.DeRegisterEvent(EventManager.eGameEvents.PickingChoppedItem_1, PickingChoppedItemFromChoppingBoard);
        EventManager.Instance.DeRegisterEvent(EventManager.eGameEvents.PlaceOrder_1, DropingOrder);
    }

    void PickingVegItem(params object[] args)
    {
        int itemNumber = (int)args[0];
        string type = args[1] as string;
        if (NumberOfItemVegPicked < 2 && !IsPlatePicked && !HaschoppedItem)
        {

            PickedVegItem[NumberOfItemVegPicked].GetComponent<MeshRenderer>().material.color = itemColor[itemNumber];
            PickedVegItem[NumberOfItemVegPicked].GetComponent<DataContainer>().VegType = type;
            PickedVegItem[NumberOfItemVegPicked].SetActive(true);
            NumberOfItemVegPicked++;
        }
        this.enabled = false;
    }

    void PickingPlate(params object[] args)
    {
        GameObject obj = args[0] as GameObject;
        if (!IsPlatePicked && NumberOfItemVegPicked < 1 && !HaschoppedItem)
        {
            // obj.transform.GetChild(0).gameObject.SetActive(false);
            Plate.SetActive(true);
            IsPlatePicked = true;
            Player.Ps = obj.GetComponent<PlateScript>();
            for (int i = 0; i < Player.Ps.NumberOfVegOnPlate; i++)
            {
                PlateItem[i].GetComponent<MeshRenderer>().material.color = Player.Ps.DroppingVeg[i].GetComponent<MeshRenderer>().material.color;
                PlateItem[i].SetActive(true);
            }
            EventManager.Instance.TriggerEvent(EventManager.eGameEvents.PickPlate_2, args);
            Player.Ps.enabled = false;
        }
        this.enabled = false;
    }

    void DropingVegItem(params object[] args)
    {
        if (NumberOfItemVegPicked > 0 && !IsPlatePicked)
        {

            for (int i = 0; i < NumberOfItemVegPicked; i++)
            {
                PickedVegItem[i].SetActive(false);
                PickedVegItem[i].GetComponent<DataContainer>().VegType = null;
            }
            NumberOfItemVegPicked = 0;

        }
        this.enabled = false;
    }

    void DropingVegItemChoppingBoard(params object[] args)
    {
        GameObject obj = args[0] as GameObject;
        ChoppingBoard chop = obj.GetComponent<ChoppingBoard>();
        if (NumberOfItemVegPicked > 0 && !IsPlatePicked && !chop.IsChoppingBoardFull)
        {

            for (int i = 0; i < NumberOfItemVegPicked; i++)
            {
                string type = PickedVegItem[i].GetComponent<DataContainer>().VegType;
                PickedVegItem[i].GetComponent<DataContainer>().VegType = null;
                EventManager.Instance.TriggerEvent(EventManager.eGameEvents.DropVegOnChoppingBoard_2, PickedVegItem[i].GetComponent<MeshRenderer>().material.color, type);
                PickedVegItem[i].SetActive(false);
            }
            chop.enabled = false;
            NumberOfItemVegPicked = 0;

        }
        this.enabled = false;
    }
    void ChoppingVegItem(params object[] args)
    {
        GameObject obj = args[0] as GameObject;
        ChoppingBoard chop = obj.GetComponent<ChoppingBoard>();
        if (NumberOfItemVegPicked < 1 && !IsPlatePicked && (chop.RefnumberOfItemOnChoopingBoard1 > 0) && !HaschoppedItem)
        {
            this.GetComponent<PlayerInput>().IsChopping = true;
            barIndicator.gameObject.SetActive(true);
            barIndicator.DOFillAmount(0, ChoppingTime).OnComplete(() => AfterChopping(chop));
        }
        else
        {
            chop.enabled = false;
        }
        this.enabled = false;
    }
    void AfterChopping(ChoppingBoard chop)
    {
        EventManager.Instance.TriggerEvent(EventManager.eGameEvents.Chop_2);
        chop.enabled = false;
        NumberOfItemVegPicked = 0;
        barIndicator.gameObject.SetActive(false);
        barIndicator.fillAmount = 1;
        this.GetComponent<PlayerInput>().IsChopping = false;
        this.enabled = false;
    }


    void PickingChoppedItemFromChoppingBoard(params object[] args)
    {
        GameObject obj = args[0] as GameObject;
        ChoppingBoard chop = obj.GetComponent<ChoppingBoard>();
        if (NumberOfItemVegPicked < 1 && !IsPlatePicked && (chop.NumberOfChoopedItem > 0))
        {
            PickedChoppedVegItem[PickedItemByPlayer].GetComponent<MeshRenderer>().material.color = chop.ChoopedVegItem1[PickedItemByPlayer].GetComponent<MeshRenderer>().material.color;
            PickedChoppedVegItem[PickedItemByPlayer].SetActive(true);
            PickedChoppedVegItem[PickedItemByPlayer].GetComponent<DataContainer>().VegType = chop.ChoopedVegItem1[PickedItemByPlayer].GetComponent<DataContainer>().VegType;
            EventManager.Instance.TriggerEvent(EventManager.eGameEvents.PickingChoppedItem_2, this);
            chop.enabled = false;
            HaschoppedItem = true;
            PickedItemByPlayer++;
        }
        this.enabled = false;
    }

    void DropingPlate(params object[] args)
    {
        GameObject obj = args[0] as GameObject;
        PlateScript Plate = obj.GetComponent<PlateScript>();
        if (IsPlatePicked && NumberOfItemVegPicked < 1)
        {
            this.Plate.SetActive(false);
            IsPlatePicked = false;
            EventManager.Instance.TriggerEvent(EventManager.eGameEvents.DropPlate_2);
        }
        Debug.Log(HaschoppedItem);
        if (HaschoppedItem && Plate.NumberOfVegOnPlate < 4)
        {
            PickedChoppedVegItem[0].SetActive(false);
            PickedChoppedVegItem[1].SetActive(false);

            List<GameObject> items = new List<GameObject>();
            if (RefnumberOfChoopedItem1 == 1)
            {
                items.Add(PickedChoppedVegItem[0]);
            }
            if (RefnumberOfChoopedItem1 == 2)
            {
                items.Add(PickedChoppedVegItem[0]);
                items.Add(PickedChoppedVegItem[1]);
            }
            if (RefnumberOfChoopedItem1 == PickedItemByPlayer)
            {
                RefnumberOfChoopedItem1 = 0;
                PickedItemByPlayer = 0;
            }

            EventManager.Instance.TriggerEvent(EventManager.eGameEvents.DropingChoppedItem, items.ToArray(), Player);
            HaschoppedItem = false;
            PickedChoppedVegItem[0].GetComponent<DataContainer>().VegType = null;
            PickedChoppedVegItem[1].GetComponent<DataContainer>().VegType = null;
        }
        Plate.enabled = false;
        this.enabled = false;
    }

    void DropingOrder(params object[] args)
    {
        GameObject obj = args[0] as GameObject;
        if (IsPlatePicked && NumberOfItemVegPicked < 1)
        {
            obj.GetComponent<OrderTableScript>().IsPlayerInContact = true;
            Plate.SetActive(false);
            IsPlatePicked = false;
            for (int i = 0; i < Player.Ps.NumberOfVegOnPlate; i++)
            {
                Player.Ps.DroppingVeg[i].SetActive(false);
                PlateItem[i].SetActive(false);
            }
            EventManager.Instance.TriggerEvent(EventManager.eGameEvents.PlaceOrder_2, Player);
        }
        this.enabled = false;

    }
}
