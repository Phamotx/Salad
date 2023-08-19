using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public enum PowerUpType { Speed, Time, Score }
public class PowerUp : MonoBehaviour
{
    private string playerTag;
    private PowerUpType type;
    private void OnEnable()
    {
        EventManager.Instance.RegisterEvent(EventManager.eGameEvents.PickPowerUp, OnPowerPickUp);
    }
    private void OnDisable()
    {
        EventManager.Instance.DeRegisterEvent(EventManager.eGameEvents.PickPowerUp, OnPowerPickUp);
    }
    public void Init(string tag)
    {
        playerTag = tag;
    }
    void OnPowerPickUp(params object[] args)
    {
        if (playerTag == (string)args[0])
        {
            type = (PowerUpType)args[1];
            PlayerScore obj = args[2] as PlayerScore;
            switch (type)
            {
                case PowerUpType.Speed:
                    break;
                case PowerUpType.Time:
                    obj.PlayerTime += 50;
                    break;
                case PowerUpType.Score:
                    obj.Score += 20;
                    break;
            }

            Destroy(this.gameObject);
        }
    }
}
