using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour {

	public float speed = 10f;

	private Transform target;
	[SerializeField] private int wavePointIndex = 0;

	public bool isShooting;
	private Transform upperPart, shootPoint;

	public float range = 100f;
	private LineRenderer laser;
	private MeshRenderer alertLight;
	// Use this for initialization
	void Start () 
	{
		
		target = WayPointsScript.points [wavePointIndex];
		upperPart = transform.GetChild (0).GetChild (0);
		shootPoint = transform.GetChild (1);
		alertLight = upperPart.GetChild (0).GetComponent<MeshRenderer> ();
		laser = GetComponent<LineRenderer> ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		
				Watch ();

				if ( !foundPlayer )
				{
					MoveEnemy ();
					RotateUpperPart ();
				}
	}

	PointScript thisPoint;

	private void MoveEnemy()
	{
		Vector3 dir = target.position - transform.position;
		transform.Translate (dir.normalized * speed * Time.deltaTime, Space.World);

		if ( Vector3.Distance (transform.position, target.position) <= .2f)
		{
			GetNextWayPoint ();
		}
	}

	private void GetNextWayPoint()
	{

		int indx = Random.Range (0, 4);

		thisPoint = target.GetComponent<PointScript> ();

		while (!thisPoint.GoPoints [indx].hasAttached)
			indx = Random.Range (0, 4);
		
		target = thisPoint.GoPoints[indx].point;
	}

	private void RotateUpperPart()
	{
		if ( !isShooting )
		{
			upperPart.Rotate (0, 50 * Time.deltaTime, 0);
		}
	} 

	public Transform player;
	private bool foundPlayer;
	public float turnSpeed;
	private void Watch()
	{
		RaycastHit hit;
		if(Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, range))
		{
			if ( hit.transform.tag == "Player" )
			{
				Debug.Log ("player found 1");
				foundPlayer = true;
				player = hit.transform;
			}
		}
		 if(Physics.Raycast(shootPoint.position, -shootPoint.forward, out hit, range))
		{
			if ( hit.transform.tag == "Player" )
			{
				Debug.Log ("player found 2");
				foundPlayer = true;
				player = hit.transform;
			}
		}
		if(Physics.Raycast(shootPoint.position, shootPoint.right, out hit, range))
		{
			if ( hit.transform.tag == "Player" )
			{
				Debug.Log ("player found 3");
				foundPlayer = true;
				player = hit.transform;
			}
		}
		if(Physics.Raycast(shootPoint.position, -shootPoint.right, out hit, range))
		{
			if ( hit.transform.tag == "Player" )
			{
				Debug.Log ("player found 4");
				foundPlayer = true;
				player = hit.transform;
			}
		}

		if ( foundPlayer )
		{
			Debug.Log ("look at player");
			Vector3 dir = player.position - transform.position;
			//Vector3.Lerp (upperPart.transform.position, dir, 200*Time.deltaTime);
			Quaternion lookRotation = Quaternion.LookRotation (dir);
			Vector3 rotation = Quaternion.Lerp (upperPart.transform.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
			upperPart.rotation = Quaternion.Euler (0f, rotation.y, 0f);


			alertLight.gameObject.SetActive (true);
			StartCoroutine (ShootLaser ());
		}
		else
		{
			alertLight.gameObject.SetActive (false);
		}
	}

	IEnumerator ShootLaser()
	{
		yield return new WaitForSeconds (.3f);
		player.GetComponent<PlayerControl> ().Die ();
		LineRenderer laser = GetComponent<LineRenderer> ();
		laser.enabled = true;
		laser.SetPosition (0, shootPoint.transform.position);
		laser.SetPosition (1, player.transform.position);
		yield return new WaitForSeconds (.2f);
		laser.enabled = false;
		foundPlayer = false;
	}

	void AlertLightAnimation()
	{
	//	alertLight.materials[0]. = Color.green;
	}
}
