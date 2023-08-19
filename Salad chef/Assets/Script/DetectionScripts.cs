using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionScripts : MonoBehaviour
{
    [SerializeField]
    private float reach = 2;
    [SerializeField]
    private KeyCode pick;
    [SerializeField]
    private KeyCode drop;
    [SerializeField]
    private KeyCode chop;
    private PlayerInput playerInput;

    private void Start()
    {
        playerInput = gameObject.GetComponent<PlayerInput>();
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        Vector3 position = new Vector3(transform.position.x, 0.25f, transform.position.z);
        Ray punchRay = new Ray(position, forward * reach);

        Debug.DrawRay(position, forward * reach, Color.red);

        if (Physics.Raycast(punchRay, out hit, reach))
        {
            // DebugUtils.LogError(hit.collider.gameObject.tag);
            if (!playerInput.IsChopping)
            {
                if (Input.GetKeyDown(pick))
                {
                    Pickup(hit.collider.gameObject.tag, hit.collider.gameObject);
                }
                if (Input.GetKeyDown(drop))
                {
                    Dropup(hit.collider.gameObject.tag, hit.collider.gameObject);
                }
                if (Input.GetKeyDown(chop))
                {
                    Chopping(hit.collider.gameObject.tag, hit.collider.gameObject);
                }
            }
        }
    }

    void Pickup(string work, GameObject obj)
    {
        switch (work)
        {
            case "A":
                this.gameObject.GetComponent<PickingDropingChopping>().enabled = true;
                EventManager.Instance.TriggerEvent(EventManager.eGameEvents.PickVeg, 0, work);
                break;
            case "B":
                this.gameObject.GetComponent<PickingDropingChopping>().enabled = true;
                EventManager.Instance.TriggerEvent(EventManager.eGameEvents.PickVeg, 1, work);
                break;
            case "C":
                this.gameObject.GetComponent<PickingDropingChopping>().enabled = true;
                EventManager.Instance.TriggerEvent(EventManager.eGameEvents.PickVeg, 2, work);
                break;
            case "D":
                this.gameObject.GetComponent<PickingDropingChopping>().enabled = true;
                EventManager.Instance.TriggerEvent(EventManager.eGameEvents.PickVeg, 3, work);
                break;
            case "E":
                this.gameObject.GetComponent<PickingDropingChopping>().enabled = true;
                EventManager.Instance.TriggerEvent(EventManager.eGameEvents.PickVeg, 4, work);
                break;
            case "F":
                this.gameObject.GetComponent<PickingDropingChopping>().enabled = true;
                EventManager.Instance.TriggerEvent(EventManager.eGameEvents.PickVeg, 5, work);
                break;
            case "Plate":
                this.gameObject.GetComponent<PickingDropingChopping>().enabled = true;
                obj.GetComponent<PlateScript>().enabled = true;
                EventManager.Instance.TriggerEvent(EventManager.eGameEvents.PickPlate_1, obj);
                break;
            case "ChoppingBoard":
                this.gameObject.GetComponent<PickingDropingChopping>().enabled = true;
                obj.GetComponent<ChoppingBoard>().enabled = true;
                EventManager.Instance.TriggerEvent(EventManager.eGameEvents.PickingChoppedItem_1, obj);
                break;
            default:
                break;
        }
    }

    void Dropup(string work, GameObject obj)
    {
        switch (work)
        {
            case "A":
                this.gameObject.GetComponent<PickingDropingChopping>().enabled = true;
                EventManager.Instance.TriggerEvent(EventManager.eGameEvents.DropVeg);
                break;
            case "B":
                this.gameObject.GetComponent<PickingDropingChopping>().enabled = true;
                EventManager.Instance.TriggerEvent(EventManager.eGameEvents.DropVeg);
                break;
            case "C":
                this.gameObject.GetComponent<PickingDropingChopping>().enabled = true;
                EventManager.Instance.TriggerEvent(EventManager.eGameEvents.DropVeg);
                break;
            case "D":
                this.gameObject.GetComponent<PickingDropingChopping>().enabled = true;
                EventManager.Instance.TriggerEvent(EventManager.eGameEvents.DropVeg);
                break;
            case "V":
                this.gameObject.GetComponent<PickingDropingChopping>().enabled = true;
                EventManager.Instance.TriggerEvent(EventManager.eGameEvents.DropVeg);
                break;
            case "F":
                this.gameObject.GetComponent<PickingDropingChopping>().enabled = true;
                EventManager.Instance.TriggerEvent(EventManager.eGameEvents.DropVeg);
                break;
            case "ChoppingBoard":
                this.gameObject.GetComponent<PickingDropingChopping>().enabled = true;
                obj.GetComponent<ChoppingBoard>().enabled = true;
                EventManager.Instance.TriggerEvent(EventManager.eGameEvents.DropVegOnChoppingBoard_1, obj);
                break;
            case "Plate":
                this.gameObject.GetComponent<PickingDropingChopping>().enabled = true;
                obj.GetComponent<PlateScript>().enabled = true;
                EventManager.Instance.TriggerEvent(EventManager.eGameEvents.DropPlate_1, obj);
                break;
            case "PlateDrop":
                this.gameObject.GetComponent<PickingDropingChopping>().enabled = true;
                //  obj.GetComponent<OrderTableScript>().enabled = true;
                EventManager.Instance.TriggerEvent(EventManager.eGameEvents.PlaceOrder_1, obj);
                break;
            case "Garbage":
                this.gameObject.GetComponent<PickingDropingChopping>().enabled = true;
                obj.GetComponent<GarbageScripts>().enabled = true;
                EventManager.Instance.TriggerEvent(EventManager.eGameEvents.DropGarbage, this.gameObject);
                break;
            default:
                break;
        }
    }
    void Chopping(string work, GameObject obj)
    {
        switch (work)
        {
            case "ChoppingBoard":
                this.gameObject.GetComponent<PickingDropingChopping>().enabled = true;
                obj.GetComponent<ChoppingBoard>().enabled = true;
                EventManager.Instance.TriggerEvent(EventManager.eGameEvents.Chop_1, obj);
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "powerUP")
        {
            var tag = this.gameObject.tag;
            PowerUpType type = (PowerUpType)Random.Range(1, 3);
            var player = this.gameObject.GetComponent<PlayerScore>();
            EventManager.Instance.TriggerEvent(EventManager.eGameEvents.PickPowerUp, tag, type, player);
        }
    }
}
