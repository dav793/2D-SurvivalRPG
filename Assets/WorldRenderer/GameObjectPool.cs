using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameObjectPool : MonoBehaviour {

	public string objectName;
	public GameObject objectPrefab;
	public int size;

	Stack<GameObject> pool;

	public void init() {

		if (objectPrefab == null || size == null) {
			throw new InvalidOperationException("Object pool parameters not set.");
		}

		pool = new Stack<GameObject> ();

		for (int i = 0; i < size; ++i) {
			instantiateObject();
		}

	}

	public void push(GameObject obj) {
		obj.SetActive (false);
		pool.Push (obj);
	}

	public GameObject pop() {
		if (pool.Count > 0) {
			GameObject obj = pool.Pop ();
			obj.SetActive (true);
			return obj;
		}
		throw new InvalidOperationException ("Object pool is empty.");
		return null;
	}

	public int getObjCount() {
		return pool.Count;
	}

	void instantiateObject() {

		GameObject obj = Instantiate (objectPrefab) as GameObject;

		if (objectName != null)
			obj.name = objectName;
		else 
			obj.name = "NewObject";
		
		obj.transform.parent = gameObject.transform;

		push (obj);
	
	}

}
