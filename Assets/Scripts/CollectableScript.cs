using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour {

	void OnTriggerEnter(Collider col)
	{
		if ( col.tag == "Player" )
		{
			GameManager.Instance.collectableCollected++;
			Destroy (gameObject);
		}
	}
}
