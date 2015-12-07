using UnityEngine;
using System;
using System.Collections;

public class WOComponentPools : MonoBehaviour {

	// Sprite component
	[HideInInspector]
	public GameObjectPool spriteComponentPool;
	[HideInInspector]
	public GameObjectPool spritePool;

	public void init() {

		initSpriteComponentPool ();

	}

	void initSpriteComponentPool() {

		// init sprite component pool
		if (transform.Find ("SpriteComponentPool") == null)
			throw new InvalidOperationException("Sprite component pool is missing.");

		spriteComponentPool = transform.Find ("SpriteComponentPool").GetComponent<GameObjectPool> ();
		spriteComponentPool.init ();

		// init sprite pool
		if (transform.Find ("SpriteComponentPool").transform.Find ("SpritePool") == null)
			throw new InvalidOperationException("Sprite pool is missing.");

		spritePool = transform.Find ("SpriteComponentPool").transform.Find ("SpritePool").GetComponent<GameObjectPool> ();
		spritePool.init ();

	}

}
