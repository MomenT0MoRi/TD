using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBonus : MonoBehaviour
{
	public GameObject[] Bonus = new GameObject[3];

	private void Awake()
	{
		GameObject tempBonus = Bonus [2];
		Instantiate (tempBonus);
	}
}
