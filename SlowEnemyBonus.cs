using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEnemyBonus : MonoBehaviour
{
	public GameObject Enemy;

	void OnMouseDown()
	{
		Enemy.GetComponent<Enemy> ().BonusSpeedSlow (2.0f);
	}
}