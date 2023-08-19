using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EventManager : SingleTon<EventManager>
{

	public enum eGameEvents
	{
		PickVeg,
		PickPlate_1,
		PickPlate_2,
		DropVeg,
		DropVegOnChoppingBoard_1,
		DropVegOnChoppingBoard_2,
		DropPlate_1,
		DropPlate_2,
		Chop_1,
		Chop_2,
		PickingChoppedItem_1,
		PickingChoppedItem_2,
		DropingChoppedItem,
		PlaceOrder_1,
		PlaceOrder_2,
		RePositionPlate,
		DropGarbage,
		PickPowerUp
	};


	void Awake()
	{
		//m_dicEventRegistry = new Dictionary<eGameEvents, GameEventDelegate>();
	}

	public void OnInstanceCreated()
	{
	}

	public delegate void GameEventDelegate(params object[] args);


	Dictionary<eGameEvents, GameEventDelegate> m_dicEventRegistry = new Dictionary<eGameEvents, GameEventDelegate>();

	public void RegisterEvent(EventManager.eGameEvents a_eEvent, GameEventDelegate a_delListener)
	{
		if (!m_dicEventRegistry.ContainsKey(a_eEvent))
		{
			m_dicEventRegistry.Add(a_eEvent, a_delListener);
			return;
		}
		//DebugUtils.Log ("=================================Register event: " +  a_eEvent.ToString ());
		m_dicEventRegistry[a_eEvent] -= a_delListener;
		m_dicEventRegistry[a_eEvent] += a_delListener;

	}

	public void DeRegisterEvent(EventManager.eGameEvents a_eEvent, GameEventDelegate a_delListener)
	{
		if (!m_dicEventRegistry.ContainsKey(a_eEvent))
			return;
		//DebugUtils.Log ("=================================DeRegister event: " +  a_eEvent.ToString ());
		m_dicEventRegistry[a_eEvent] -= a_delListener;
	}

	public void DeRegisterAllEvent()
	{
		m_dicEventRegistry.Clear();
	}

	string strEventKey;
	GameEventDelegate d;

	public void TriggerEvent(eGameEvents a_eEvent, params object[] args)
	{
		strEventKey = a_eEvent.ToString();

		if (m_dicEventRegistry.TryGetValue(a_eEvent, out d))
		{
			//Callback callback = d as Callback;
			if (d != null)
			{
				//				DebugUtils.Log ("Event triggered: " + strEventKey);
				d(args);
			}
			else
				DebugUtils.Log("=================================Could not trigger event: " + strEventKey);
		}
		d = null;
	}

	void OnDestroy()
	{
		base.OnDestroy();
		m_dicEventRegistry.Clear();
	}
}
