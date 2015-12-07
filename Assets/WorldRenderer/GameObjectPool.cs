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

		if (objectPrefab == null || size == 0) {
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
		obj.transform.position = Vector3.zero;
	}

	public void pushAndResetParent(GameObject obj) {
		push (obj);
		setName (obj);
		obj.transform.parent = transform;
		obj.transform.position = Vector3.zero;
	}

	public GameObject pop() {
		if (pool.Count > 0) {
			GameObject obj = pool.Pop ();
			obj.SetActive (true);
			return obj;
		}
		throw new InvalidOperationException ("Object pool is empty.");
	}

	public int getObjCount() {
		return pool.Count;
	}

	void instantiateObject() {
		GameObject obj = Instantiate (objectPrefab) as GameObject;
		setName (obj);
		obj.transform.parent = gameObject.transform;
		push (obj);
	}

	void setName(GameObject obj) {
		if (objectName != null)
			obj.name = objectName;
		else 
			obj.name = "NewObject";
	}

}
