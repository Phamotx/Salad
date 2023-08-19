using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;

public class OrderTableScript : MonoBehaviour
{
    [SerializeField]
    private float timeBeforeFirstOrder;
    [SerializeField]
    private int min_Items;
    [SerializeField]
    private int max_Items;
    [SerializeField]
    private GameObject plate;
    [SerializeField]
    private GameObject[] vegItems;
    [SerializeField]
    private GameObject customer;
    [SerializeField]
    [Range(0, 2)]
    private float animtaionSpeed;
    [SerializeField]
    private Image orderPanel;
    [SerializeField]
    private Image timeBar;
    [SerializeField]
    private Text order;
    [SerializeField]
    private float timeForFoodCunsumption;
    [SerializeField]
    private float timeIntervalBeforeNextOrder;
    [SerializeField]
    private float customerWaitTime;
    [SerializeField]
    private string[] data;
    [SerializeField]
    private float powerUpThreshold;
    [SerializeField]
    private PowerUp powerUpPrefab;
    [SerializeField]
    private Transform powerUpParent;
    private string[] vegIndex = { "A", "B", "C", "D", "E", "F" };
    private bool isOrderPlaced;
    private bool isPlayerInContact;

    public bool IsPlayerInContact { get => isPlayerInContact; set => isPlayerInContact = value; }

    private void Start()
    {
        Invoke("GenrateOrder", timeBeforeFirstOrder);
    }
    private void OnEnable()
    {
        EventManager.Instance.RegisterEvent(EventManager.eGameEvents.PlaceOrder_2, SetPlate);
    }
    private void OnDisable()
    {
        EventManager.Instance.DeRegisterEvent(EventManager.eGameEvents.PlaceOrder_2, SetPlate);
    }
    void GenrateOrder()
    {
        timeBar.fillAmount = 1;
        customer.SetActive(true);
        OrderData();
        customer.transform.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
        customer.transform.DOPunchScale(new Vector3(0.75f, 8f, 0.75f), animtaionSpeed, 4, 0.5f).OnComplete(() => customer.transform.localScale = new Vector3(0.75f, 8f, 0.75f));
        StartCoroutine(OrderActivation());
    }

    IEnumerator OrderActivation()
    {
        yield return new WaitForSeconds(2f);
        timeBar.gameObject.SetActive(true);
        timeBar.DOFillAmount(0, customerWaitTime);
        StartCoroutine(AfterTimeOut());
    }

    IEnumerator AfterTimeOut()
    {
        yield return new WaitForSeconds(customerWaitTime);
        timeBar.gameObject.SetActive(false);
        StartCoroutine(WaitTime());
    }

    IEnumerator WaitTime(params object[] args)
    {
        order.text = "";
        timeBar.gameObject.SetActive(false);
        orderPanel.gameObject.SetActive(false);
        yield return new WaitForSeconds(timeForFoodCunsumption);
        if (isOrderPlaced)
        {
            CheckOrder(args);
        }
        else
        {
            OrderNotRecived();
        }
        customer.SetActive(false);
        yield return new WaitForSeconds(timeIntervalBeforeNextOrder);
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = "";
        }
        isOrderPlaced = false;
        GenrateOrder();
    }
    void OrderData()
    {
        int numberOfItems = Random.Range(min_Items, max_Items + 1);
        for (int i = 0; i < numberOfItems; i++)
        {
            int itemIndex = Random.Range(0, 6);
            if (i == 0)
            {
                order.text = vegIndex[itemIndex];
            }
            else
            {
                order.text = order.text + "+" + vegIndex[itemIndex];
            }
            data[i] = vegIndex[itemIndex];
        }
        orderPanel.gameObject.SetActive(true);
    }

    void OrderNotRecived()
    {
        PlayerScore[] players = FindObjectsOfType<PlayerScore>();
        for (int i = 0; i < players.Length; i++)
        {
            players[i].Score -= 10;
            if (players[i].Score <= 0)
            {
                players[i].Score = 0;
            }
        }
    }

    void CheckOrder(params object[] args)
    {
        PlayerScore player = args[0] as PlayerScore;
        PlateScript plates = player.Ps;

        var areEqual = plates.Data.data.OrderBy(x => x)
                              .SequenceEqual(data.OrderBy(x => x));
        for (int i = 0; i < plates.Data.data.Length; i++)
        {
            Debug.Log(plates.Data.data[i]);
            Debug.Log(data[i]);
        }

        Debug.Log("Are equal " + areEqual);
        if (areEqual)
        {
            player.Score += 40;

            if (timeBar.fillAmount >= (1 - powerUpThreshold))
            {
                GenratePowerUp(player.tag);
            }
        }
        else
        {
            player.Score -= 20;
            if (player.Score <= 0)
            {
                player.Score = 0;
            }
        }
        for (int i = 0; i < 4; i++)
        {
            vegItems[i].SetActive(false);
            plates.Data.data[i] = "";
        }
        plate.SetActive(false);
        EventManager.Instance.TriggerEvent(EventManager.eGameEvents.RePositionPlate, plate);
        plates.Plate.SetActive(true);
        //plate data should reapear.
    }

    void SetPlate(params object[] args)
    {
        if (IsPlayerInContact)
        {
            isOrderPlaced = true;
            PlayerScore obj = args[0] as PlayerScore;
            Debug.Log(obj.gameObject.name);

            PlateScript p = obj.Ps;
            plate.SetActive(true);
            for (int i = 0; i < p.NumberOfVegOnPlate; i++)
            {
                vegItems[i].GetComponent<MeshRenderer>().material.color = p.DroppingVeg[i].GetComponent<MeshRenderer>().material.color;
                vegItems[i].SetActive(true);
            }
            p.NumberOfVegOnPlate = 0;
            StopAllCoroutines();
            isPlayerInContact = false;
            StartCoroutine(WaitTime(args));
        }
    }

    void GenratePowerUp(string tag)
    {
        var powerUp = Instantiate(powerUpPrefab, Vector3.zero, Quaternion.identity, powerUpParent);
        powerUp.Init(tag);
    }
}
