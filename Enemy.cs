using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{	
	public float startSpeed = 3.0f;
	public float speed = 3.0f;
	public float health = 10.0f;
	public List<GameObject> wayPoints = new List<GameObject>();
	int wayIndex = 0;

	void Start()
	{
		GetWayPoints ();
	}

	void Update()
	{
		Move ();
	}

	void GetWayPoints()
	{
		wayPoints = GetComponentInParent <LevelManager> ().wayPoints;
	}
		
	void Move()
	{
		Transform currWayPoints = wayPoints [wayIndex].transform;
		Vector3 currWayPos = new Vector3 (currWayPoints.position.x + currWayPoints.GetComponent<SpriteRenderer> ().bounds.size.x / 2,
			                              currWayPoints.position.y - currWayPoints.GetComponent<SpriteRenderer> ().bounds.size.x / 2);
		Vector3 moveDir = currWayPos - transform.position;
		transform.Translate (moveDir.normalized * Time.deltaTime * speed);
		if (Vector3.Distance (transform.position, currWayPos) < 0.1f) {
			if (wayIndex < wayPoints.Count - 1)
				wayIndex++;
			else
				Destroy (gameObject);
		}
	}

	public void TakeDamage(float damage)
	{
		health -= damage;
		if (health <= 0)
			Destroy (gameObject, 3.0f);
	}

	public void StartSlow(float durandion, float slowValue)
	{
		StopCoroutine ("GetSlow");
		speed = startSpeed;
		StartCoroutine (GetSlow (durandion, slowValue));
	}

	IEnumerator GetSlow(float durandion, float slowValue)
	{
		speed -= slowValue;
		yield return new WaitForSeconds (durandion);
		speed = startSpeed;
	}

	public void AOEDamage(float range, float damage)
	{
		List<Enemy> enemies = new List<Enemy> ();
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy")) {
			if (Vector2.Distance (transform.position, go.transform.position) <= range) {
				enemies.Add (go.GetComponent<Enemy> ());
			}
		}
		foreach (Enemy es in enemies)
			es.TakeDamage (damage);
	}

	public void BonusSpeedSlow(float slowSpeed)
	{
		speed -= slowSpeed;
		Debug.Log (speed);
	}
}
