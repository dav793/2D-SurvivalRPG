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

			WorldCell[,] cells = sectorLevel.getAllCells ();
			
			for (int x = 0; x < GameSettings.LoadedConfig.SectorLength_Cells; ++x) {
				for (int z = 0; z < GameSettings.LoadedConfig.SectorLength_Cells; ++z) {
					renderCell (cells[x, z]);
				}
			}

			sectorLevel.isRendered = true;
			Debug.Log (sectorLevel.indexToString()+" was rendered.");

			//debug lines
			debug_delimitSectorLevel (sectorLevel);

		}

	}

	public override void unrenderSectorLevel(WorldSectorLevel sectorLevel) {

		if (!sectorLevel.isRendered) {
			Debug.LogWarning ("Sector level is not rendered!");
		}
		else {

			WorldCell[,] cells = sectorLevel.getAllCells ();
			
			for (int x = 0; x < GameSettings.LoadedConfig.SectorLength_Cells; ++x) {
				for (int z = 0; z < GameSettings.LoadedConfig.SectorLength_Cells; ++z) {
					unrenderCell (cells[x, z]);
				}
			}

			sectorLevel.isRendered = false;
			Debug.Log (sectorLevel.indexToString()+" was unrendered.");

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

	// Debug functions
	void debug_delimitSectorLevel(WorldSectorLevel sectorLevel) {
		WorldCell[,] cells = sectorLevel.getAllCells ();

		int cell_width = GameSettings.LoadedConfig.CellLength_Pixels;
		Debug.DrawLine(
			new Vector3(cells[0,0].x*cell_width-cell_width/2, sectorLevel.y, cells[0,0].z*cell_width-cell_width/2), 
			new Vector3(cells[0,0].x*cell_width-cell_width/2, sectorLevel.y, cells[9,9].z*cell_width+cell_width/2), 
			Color.red,
			10000f,
			false
		);
		Debug.DrawLine(
			new Vector3(cells[0,0].x*cell_width-cell_width/2, sectorLevel.y, cells[9,9].z*cell_width+cell_width/2), 
			new Vector3(cells[9,9].x*cell_width+cell_width/2, sectorLevel.y, cells[9,9].z*cell_width+cell_width/2), 
			Color.red,
			10000f,
			false
		);
		Debug.DrawLine(
			new Vector3(cells[9,9].x*cell_width+cell_width/2, sectorLevel.y, cells[9,9].z*cell_width+cell_width/2), 
			new Vector3(cells[9,9].x*cell_width+cell_width/2, sectorLevel.y, cells[0,0].z*cell_width-cell_width/2), 
			Color.red,
			10000f,
			false
		);
		Debug.DrawLine(
			new Vector3(cells[9,9].x*cell_width+cell_width/2, sectorLevel.y, cells[0,0].z*cell_width-cell_width/2), 
			new Vector3(cells[0,0].x*cell_width-cell_width/2, sectorLevel.y, cells[0,0].z*cell_width-cell_width/2), 
			Color.red,
			10000f,
			false
		);
	}

}
