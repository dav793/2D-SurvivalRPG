using UnityEngine;
using System;
using System.Collections;

public class Renderer2D : StyleRenderer {

	public WOComponentPools worldObjectComponentPools;
	GameObjectPool worldObjectPool;

	public override void init() {
		initWorldObjectPool ();
		initWOComponentPools ();
	}

	void initWorldObjectPool() {

		if (transform.Find ("WorldObjectPool") == null)
			throw new InvalidOperationException("World object pool is missing.");
		worldObjectPool = transform.Find ("WorldObjectPool").GetComponent<GameObjectPool> ();
		
		worldObjectPool.init ();
	
	}

	void initWOComponentPools() {

		if (transform.Find ("WOComponentPools") == null)
			throw new InvalidOperationException("World object component pool is missing.");
		worldObjectComponentPools = transform.Find ("WOComponentPools").GetComponent<WOComponentPools> ();
		
		worldObjectComponentPools.init ();

	}

	public override void renderSectorLevel(WorldSectorLevel sectorLevel) {

		if (sectorLevel.isRendered) {
			Debug.LogWarning ("Sector level is already rendered!");
		}
		else {
		
			Debug.Log ("Rendering "+sectorLevel.indexToString());
			
			WorldCell[,] cells = sectorLevel.getAllCells ();
			
			for (int x = 0; x < sectorLevel.getSectorLengthInCells(); ++x) {
				for (int z = 0; z < sectorLevel.getSectorLengthInCells(); ++z) {
					renderCell (cells[x, z]);
				}
			}

			sectorLevel.isRendered = true;

		}

	}

	public override void unrenderSectorLevel(WorldSectorLevel sectorLevel) {

		if (!sectorLevel.isRendered) {
			Debug.LogWarning ("Sector level is not rendered!");
		}
		else {
			
			Debug.Log ("Unrendering "+sectorLevel.indexToString());
			
			WorldCell[,] cells = sectorLevel.getAllCells ();
			
			for (int x = 0; x < sectorLevel.getSectorLengthInCells(); ++x) {
				for (int z = 0; z < sectorLevel.getSectorLengthInCells(); ++z) {
					unrenderCell (cells[x, z]);
				}
			}

			sectorLevel.isRendered = false;

		}

	}

	public override void renderCell(WorldCell cell) {
		//Debug.Log ("Rendering "+cell.indexToString());
		RendererWorldObject rendererObj = worldObjectPool.pop ().GetComponent<RendererWorldObject> ();
		cell.attachRenderObject (rendererObj);
		rendererObj.init (cell);
		rendererObj.initSprites ();
		rendererObj.updatePosition ();
	}

	public override void unrenderCell(WorldCell cell) {
		cell.getRenderObject().terminate ();
		worldObjectPool.push (cell.getRenderObject().gameObject);
		cell.detachRenderObject ();
	}

}
