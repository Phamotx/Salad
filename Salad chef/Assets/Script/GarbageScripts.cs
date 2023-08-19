using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageScripts : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.Instance.RegisterEvent(EventManager.eGameEvents.DropGarbage, DropGarbage);
    }
    private void OnDisable()
    {
        EventManager.Instance.DeRegisterEvent(EventManager.eGameEvents.DropGarbage, DropGarbage);
    }
    void DropGarbage(params object[] args)
    {
        GameObject obj = args[0] as GameObject;
        PickingDropingChopping pdc = obj.GetComponent<PickingDropingChopping>();
        Debug.Log(pdc.enabled);

        if (pdc.NumberOfItemVegPicked > 0 && !pdc.IsPlatePicked)
        {

            for (int i = 0; i < pdc.NumberOfItemVegPicked; i++)
            {
                pdc.PickedVegItem[i].SetActive(false);
                pdc.PickedVegItem[i].GetComponent<DataContainer>().VegType = null;
            }
            pdc.NumberOfItemVegPicked = 0;

        }
        if (pdc.HaschoppedItem)
        {
            pdc.PickedChoppedVegItem[0].SetActive(false);
            pdc.PickedChoppedVegItem[1].SetActive(false);
            // pickedItemByPlayer--;
            //  RefnumberOfChoopedItem1--;
            if (pdc.RefnumberOfChoopedItem1 == pdc.PickedItemByPlayer)
            {
                pdc.RefnumberOfChoopedItem1 = 0;
                pdc.PickedItemByPlayer = 0;
            }
            pdc.HaschoppedItem = false;
            pdc.PickedChoppedVegItem[0].GetComponent<DataContainer>().VegType = null;
            pdc.PickedChoppedVegItem[1].GetComponent<DataContainer>().VegType = null;
        }
        if (pdc.IsPlatePicked)
        {
            PlateScript plate = pdc.Player.Ps;
            for (int i = 0; i < 4; i++)
            {
                plate.DroppingVeg[i].SetActive(false);
                plate.Data.data[i] = "";
                pdc.PlateItem[i].SetActive(false);

            }
            pdc.IsPlatePicked = false;
            plate.Plate.SetActive(true);
            pdc.Plate.SetActive(false);
            // Reduce Some Amount from player Score.
            pdc.Player.Score -= 10;
            if (pdc.Player.Score <= 0)
            {
                pdc.Player.Score = 0;
            }
            pdc.Player.Ps.enabled = false;
        }
        this.enabled = false;
        pdc.enabled = false;

    }
}
