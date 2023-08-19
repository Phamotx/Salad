using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateScript : MonoBehaviour
{
    [SerializeField]
    private GameObject plate;
    [SerializeField]
    private GameObject[] droppingVeg;
    [SerializeField]
    private int numberOfVegOnPlate;
    [SerializeField]
    private plateData data;

    public int NumberOfVegOnPlate { get => numberOfVegOnPlate; set => numberOfVegOnPlate = value; }
    public plateData Data { get => data; set => data = value; }
    public GameObject[] DroppingVeg { get => droppingVeg; set => droppingVeg = value; }
    public GameObject Plate { get => plate; set => plate = value; }

    private void OnEnable()
    {
        EventManager.Instance.RegisterEvent(EventManager.eGameEvents.DropingChoppedItem, DroppingVegOnPlate);
        EventManager.Instance.RegisterEvent(EventManager.eGameEvents.PickPlate_2, PickPlate);
        EventManager.Instance.RegisterEvent(EventManager.eGameEvents.DropPlate_2, ResetPlate);
    }
    private void OnDisable()
    {
        EventManager.Instance.DeRegisterEvent(EventManager.eGameEvents.DropingChoppedItem, DroppingVegOnPlate);
        EventManager.Instance.DeRegisterEvent(EventManager.eGameEvents.PickPlate_2, PickPlate);
        EventManager.Instance.DeRegisterEvent(EventManager.eGameEvents.DropPlate_2, ResetPlate);
    }

    void DroppingVegOnPlate(params object[] args)
    {
        GameObject[] obj = args[0] as GameObject[];
        List<string> itemList = new List<string>();
        foreach (var v in obj)
        {
            itemList.Add(v.GetComponent<DataContainer>().VegType);
        }

        PlayerScore p = args[1] as PlayerScore;
        int index = 0;
        foreach (var item in itemList)
        {
            if (NumberOfVegOnPlate < 4)
            {
                DroppingVeg[NumberOfVegOnPlate].GetComponent<MeshRenderer>().material.color = obj[index].GetComponent<MeshRenderer>().material.color;
                DroppingVeg[NumberOfVegOnPlate].SetActive(true);
                Data.data[NumberOfVegOnPlate] = item;
                Data.player = p;
                NumberOfVegOnPlate++;
            }
            index++;
        }
    }
    void PickPlate(params object[] args)
    {
        Plate.SetActive(false);
    }
    void ResetPlate(params object[] args)
    {
        Plate.SetActive(true);
    }

}
[System.Serializable]
public class plateData
{
    public string[] data;
    public PlayerScore player;
}
