using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreOfTheTowerScr : MonoBehaviour 
{
	Transform target;
	CoreTower selfCoreTower;
	public Tower selfTower;
	GameControllerScr GCS;

	private void Start()
	{
		GCS = FindObjectOfType<GameControllerScr> ();
		selfCoreTower = GCS.AllCore [selfTower.type];
		GetComponent<SpriteRenderer> ().sprite = selfCoreTower.spriteCore;
	}

	private void Update()
	{
		Move ();
	}

	public void SetTarget(Transform units)
	{
		target = units;
	}

	private void Move()
	{

		if (target != null) {

			if (Vector2.Distance (transform.position, target.position) < 0.1f) {
				Hit ();
				Destroy (gameObject);
			}
			else 
			{
				Vector2 moveDirection = target.transform.position - transform.position;

				transform.Translate (moveDirection.normalized * Time.deltaTime * selfCoreTower.speed);
			}
		} else
			Destroy (gameObject);
	}

	private void Hit()
	{
		switch (selfTower.type) {
		case(int) TowerType.IceTower:
			target.GetComponent<Enemy> ().StartSlow (1.0f, 1.0f);
			target.GetComponent<Enemy> ().TakeDamage (selfCoreTower.damage);
			break;
		case(int)TowerType.AreaTower:
			target.GetComponent<Enemy> ().AOEDamage (2.0f, selfCoreTower.damage);
			break;
		}
	}
}
