using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointScript : MonoBehaviour {

	void OnTriggerEnter(Collider col)
	{
		if ( col.tag == "Player" )
		{
			GameManager.Instance.SetGameComplete(true);
			Destroy (transform.parent.gameObject);
		}
	}
}
