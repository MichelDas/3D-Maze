using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSensorScript : MonoBehaviour {

	[Header("All Sensors")]

	private PlayerControl plColtrol;

	[SerializeField] private  bool left, right, top, bottom;

	void Awake()
	{
		plColtrol = GetComponentInParent<PlayerControl> ();
	}

	void OnTriggerEnter(Collider col)
	{
		if ( col.tag == "Environment" )
		{
			if ( left )
			{
				plColtrol.leftSensor = true;
			}
			else if ( right )
			{
				plColtrol.rightSensor = true;
			}
			else if ( top )
			{
				plColtrol.topSensor = true;
			}
			else if ( bottom )
			{
				plColtrol.bottomSensor = true;
			}
		}
	}

	void OnTriggerExit(Collider col)
	{
		if ( col.tag == "Environment" )
		{
			if ( left )
			{
				plColtrol.leftSensor = false;
			}
			else if ( right )
			{
				plColtrol.rightSensor = false;
			}
			else if ( top )
			{
				plColtrol.topSensor = false;
			}
			else if ( bottom )
			{
				plColtrol.bottomSensor = false;
			}
		}
	}

}
