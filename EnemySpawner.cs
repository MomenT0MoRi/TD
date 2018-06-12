using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour 
{
	public LevelManager LM;

	public float timeToNextWave = 3.0f;
	public int unitsWave = 0;
	int spawnUnit = 10;
	void Start()
	{
		StartCoroutine (SpawnUnit (spawnUnit, 0.3f));
	}

	void Update()
	{
		NextWave ();
	}

	IEnumerator SpawnUnit(int unitCount, float delayTime){
		for (int i = 0; i <= unitCount; i++) {
			GameObject tempEnemy = GetComponent<ObjectPool> ().GetPoolObject ();
			if (tempEnemy != null) {
				tempEnemy.transform.SetParent (gameObject.transform, false);
				Transform startCellPos = LM.wayPoints [0].transform;
				Vector3 startPos = new Vector3 (startCellPos.position.x + startCellPos.GetComponent<SpriteRenderer> ().bounds.size.x / 2,
					                  startCellPos.position.y + startCellPos.GetComponent<SpriteRenderer> ().bounds.size.y / 2);
				tempEnemy.transform.position = startPos;
				tempEnemy.SetActive (true);
			}
			yield return new WaitForSeconds (delayTime);
		}
	}

	private void NextWave()
	{
		timeToNextWave -= Time.deltaTime;
		if (unitsWave <= 10  && timeToNextWave <= 0) {
			StartCoroutine (SpawnUnit (spawnUnit, 0.3f));
			unitsWave++;
			timeToNextWave = 15.0f;
		}
	}
}
