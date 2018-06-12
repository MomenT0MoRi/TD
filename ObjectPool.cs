using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour 
{
	public GameObject objectToPool;
	public int amountPool = 10;
	public Transform parentObjectPool;

	public List<GameObject> objectPool;


	private void Start()
	{
		CreateObjectPool (amountPool);
	}

	private void Update()
	{
		CreateNextUnitsWave ();
	}

	List<GameObject> CreateObjectPool(int amaountPool){
		
		objectPool = new List<GameObject>(amountPool);

		for (int i = 0; i < amountPool; i++) {
			GameObject tempObject = (GameObject) Instantiate (objectToPool);
			tempObject.SetActive (false);
			objectPool.Add (tempObject);
			tempObject.transform.SetParent (parentObjectPool, false);
		}
		return null;
	}

	public GameObject GetPoolObject(){
		
		for (int i = 0; i < objectPool.Count; i++) {
			if (!objectPool [i].activeInHierarchy) {
				return objectPool [i];
			}
		}
		return null;
	}

	private void CreateNextUnitsWave()
	{
		for (int i = 0; i < objectPool.Count; i++)
			if (objectPool[i] == null) {
				objectPool.Remove(objectPool[i]);
				if (objectPool.Count <= 0)
					CreateObjectPool (amountPool);
			}
	}

}
