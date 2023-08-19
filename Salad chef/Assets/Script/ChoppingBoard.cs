using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppingBoard : MonoBehaviour
{
    [SerializeField]
    private int numberOfItemOnChoopingBoard;
    [SerializeField]
    private int RefnumberOfItemOnChoopingBoard;
    [SerializeField]
    private int numberOfChoopedItem;
    [SerializeField]
    private GameObject[] vegOnChoopingBoard;
    [SerializeField]
    private GameObject[] ChoopedVegItem;
    [SerializeField]
    private bool isChoppingBoardFull;

    public bool IsChoppingBoardFull { get => isChoppingBoardFull; set => isChoppingBoardFull = value; }
    public int NumberOfItemOnChoopingBoard { get => numberOfItemOnChoopingBoard; set => numberOfItemOnChoopingBoard = value; }
    public int NumberOfChoopedItem { get => numberOfChoopedItem; set => numberOfChoopedItem = value; }
    public int RefnumberOfItemOnChoopingBoard1 { get => RefnumberOfItemOnChoopingBoard; set => RefnumberOfItemOnChoopingBoard = value; }
    public GameObject[] ChoopedVegItem1 { get => ChoopedVegItem; set => ChoopedVegItem = value; }

    private void OnEnable()
    {
        EventManager.Instance.RegisterEvent(EventManager.eGameEvents.DropVegOnChoppingBoard_2, DropingVegItemOnChoppingBoard);
        EventManager.Instance.RegisterEvent(EventManager.eGameEvents.Chop_2, ChoopingVegItem);
        EventManager.Instance.RegisterEvent(EventManager.eGameEvents.PickingChoppedItem_2, PickingChoppedItem);
    }
    private void OnDisable()
    {
        EventManager.Instance.DeRegisterEvent(EventManager.eGameEvents.DropVegOnChoppingBoard_2, DropingVegItemOnChoppingBoard);
        EventManager.Instance.DeRegisterEvent(EventManager.eGameEvents.Chop_2, ChoopingVegItem);
        EventManager.Instance.DeRegisterEvent(EventManager.eGameEvents.PickingChoppedItem_2, PickingChoppedItem);
    }
    void DropingVegItemOnChoppingBoard(params object[] args)
    {
        Color color = (Color)args[0];
        string type = args[1] as string;
        if (NumberOfItemOnChoopingBoard < 2)
        {
            vegOnChoopingBoard[NumberOfItemOnChoopingBoard].GetComponent<MeshRenderer>().material.color = color;
            vegOnChoopingBoard[NumberOfItemOnChoopingBoard].GetComponent<DataContainer>().VegType = type;
            vegOnChoopingBoard[NumberOfItemOnChoopingBoard].SetActive(true);
            NumberOfItemOnChoopingBoard++;
            RefnumberOfItemOnChoopingBoard1++;
        }
        if (NumberOfItemOnChoopingBoard >= 2)
        {
            isChoppingBoardFull = true;
        }
        else
        {
            isChoppingBoardFull = false;
        }

    }
    void ChoopingVegItem(params object[] args)
    {
        if (NumberOfItemOnChoopingBoard > 0)
        {
            vegOnChoopingBoard[NumberOfChoopedItem].SetActive(false);
            ChoopedVegItem1[NumberOfChoopedItem].GetComponent<MeshRenderer>().material.color = vegOnChoopingBoard[NumberOfChoopedItem].GetComponent<MeshRenderer>().material.color;
            ChoopedVegItem1[NumberOfChoopedItem].SetActive(true);
            ChoopedVegItem1[NumberOfChoopedItem].GetComponent<DataContainer>().VegType = vegOnChoopingBoard[NumberOfChoopedItem].GetComponent<DataContainer>().VegType;
            vegOnChoopingBoard[NumberOfChoopedItem].GetComponent<DataContainer>().VegType = null;
            RefnumberOfItemOnChoopingBoard1--;
            NumberOfChoopedItem++;
        }
    }

    void PickingChoppedItem(params object[] args)
    {
        PickingDropingChopping player = args[0] as PickingDropingChopping;
        if (numberOfChoopedItem > 0)
        {
            if (numberOfChoopedItem == 2 && NumberOfItemOnChoopingBoard == 2)
            {
                ChoopedVegItem1[player.RefnumberOfChoopedItem1].SetActive(false);
                ChoopedVegItem1[player.RefnumberOfChoopedItem1].GetComponent<DataContainer>().VegType = null;
                NumberOfItemOnChoopingBoard--;
                player.RefnumberOfChoopedItem1++;
                NumberOfChoopedItem--;
            }
            else if (numberOfChoopedItem == 2 && NumberOfItemOnChoopingBoard == 1)
            {
                ChoopedVegItem1[player.RefnumberOfChoopedItem1].SetActive(false);
                ChoopedVegItem1[player.RefnumberOfChoopedItem1].GetComponent<DataContainer>().VegType = null;
                NumberOfItemOnChoopingBoard--;
                player.RefnumberOfChoopedItem1++;
                NumberOfChoopedItem--;
            }
            else if (numberOfChoopedItem == 1 && NumberOfItemOnChoopingBoard == 1)
            {
                ChoopedVegItem1[player.RefnumberOfChoopedItem1].SetActive(false);
                ChoopedVegItem1[player.RefnumberOfChoopedItem1].GetComponent<DataContainer>().VegType = null;
                NumberOfItemOnChoopingBoard--;
                player.RefnumberOfChoopedItem1++;
                NumberOfChoopedItem--;
            }
            else if (numberOfChoopedItem == 1 && NumberOfItemOnChoopingBoard == 2)
            {
                ChoopedVegItem1[player.RefnumberOfChoopedItem1].SetActive(false);
                vegOnChoopingBoard[1].SetActive(false);
                vegOnChoopingBoard[0].SetActive(true);
                vegOnChoopingBoard[0].GetComponent<MeshRenderer>().material.color = vegOnChoopingBoard[1].GetComponent<MeshRenderer>().material.color;
                vegOnChoopingBoard[0].GetComponent<DataContainer>().VegType = vegOnChoopingBoard[1].GetComponent<DataContainer>().VegType;
                vegOnChoopingBoard[1].GetComponent<DataContainer>().VegType = null;
                ChoopedVegItem1[player.RefnumberOfChoopedItem1].GetComponent<DataContainer>().VegType = null;
                NumberOfItemOnChoopingBoard--;
                player.RefnumberOfChoopedItem1++;
                NumberOfChoopedItem--;
            }
            isChoppingBoardFull = false;

        }
    }
    // void Droping
}
