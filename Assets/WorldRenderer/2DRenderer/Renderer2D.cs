using UnityEngine;
using System;
using System.Collections;

public class Renderer2D : StyleRenderer {

	GameObjectPool worldObjectPool;

	public override void init() {

		initWorldObjectPool ();

		/*GameObject newWObject = worldObjectPool.pop ();
		WOComponentManager compMan = newWObject.GetComponent<WOComponentManager> ();
		compMan.init (WorldObjectTypes.TerrainCell);
		compMan.terminate ();
		worldObjectPool.push (newWObject);*/

	}

	void initWorldObjectPool() {

		if (transform.Find ("WorldObjectPool") == null)
			throw new InvalidOperationException("World object pool is missing.");
		worldObjectPool = transform.Find ("WorldObjectPool").GetComponent<GameObjectPool> ();
		
		worldObjectPool.init ();
	
	}

}
