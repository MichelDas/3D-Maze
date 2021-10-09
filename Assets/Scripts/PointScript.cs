using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointScript : MonoBehaviour {

	[Header("0 for top, 1 for bottom, 2 for left, 3 for right")]
	public Attached[] GoPoints; 

	[System.Serializable]
	public struct Attached
	{
		public bool hasAttached;
		[HideInInspector] public Transform point;
		public int attachedPointNo;
	}

	// Use this for initialization
	void Awake () {
		for (int i = 0; i < 4; i++)
		{
			if(GoPoints[i].hasAttached)
				GoPoints [i].point = GameObject.Find ("Wp1 (" + GoPoints [i].attachedPointNo + ")").transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
