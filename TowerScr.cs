using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScr : MonoBehaviour 
{
	public GameObject coreTowerPrefab;

	Tower selfTower;
	public TowerType selfType;

	GameControllerScr GCS;

	private void Start()
	{
		GCS = FindObjectOfType<GameControllerScr> ();
		selfTower = GCS.AllTowers [(int)selfType];
		GetComponent<SpriteRenderer> ().sprite = selfTower.spriteTower;

		InvokeRepeating ("SearchTarget", 0, 0.1f);
	}

	void Update()
	{
		if (selfTower.currCoolDown > 0)
			selfTower.currCoolDown -= Time.deltaTime;
	}

	bool CanShoot()
	{
		if (selfTower.currCoolDown <= 0)
			return true;
		return false;
	}

	void SearchTarget()
	{
		if (CanShoot()) {
			Transform foundUnit = null;

			Collider2D[] colliders = Physics2D.OverlapCircleAll (transform.position, gameObject.GetComponent<CircleCollider2D> ().radius);
			for (int i = 0; i < colliders.Length; i++) {
				if (colliders [i].GetComponent<Enemy> ()) {
					foundUnit = colliders [i].transform;
				}
			}
			if (foundUnit != null)
				Shoot (foundUnit);
		}
	}

	void Shoot(Transform unit)
	{
		selfTower.currCoolDown = selfTower.coolDown;
		GameObject tempCore = Instantiate (coreTowerPrefab);
		tempCore.GetComponent<CoreOfTheTowerScr> ().selfTower = selfTower;
		tempCore.transform.SetParent (gameObject.transform, false);
		tempCore.transform.position = transform.position;
		tempCore.GetComponent<CoreOfTheTowerScr> ().SetTarget (unit);
	}

}
