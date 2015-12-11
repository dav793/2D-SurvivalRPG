using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum WorldObjectTypes { Unassigned, TerrainCell };

public class RendererWorldObject : MonoBehaviour {

	public WorldObjectTypes type = WorldObjectTypes.Unassigned;
	public WOComponentManager componentManager;

	WorldCell cell_object;
	//WorldObject world_object;

	void Awake() {
		componentManager = GetComponent<WOComponentManager> ();
	}

	public void init(WorldCell cell) {
		type = WorldObjectTypes.TerrainCell;
		attachObject (cell);

		componentManager.init (this);
	}

	public void terminate() {
		type = WorldObjectTypes.Unassigned;
		detachObject ();

		componentManager.terminate ();
	}

	void attachObject(WorldCell cell) {
		cell_object = cell;
	}

	void detachObject() {
		cell_object = null;
	}

	public void updatePosition() {
		switch (type) {
		case WorldObjectTypes.TerrainCell:
			transform.position = new Vector3(
				GameSettings.LoadedConfig.CellLength_Pixels * cell_object.x,
				RenderSettings.LoadedConfig.TerrainBaseY + cell_object.y * RenderSettings.LoadedConfig.LevelDifferenceY,
				(GameSettings.LoadedConfig.CellLength_Pixels * cell_object.z) + cell_object.y * RenderSettings.LoadedConfig.LevelOffsetZ
			);
			break;
		}
	}

	public void initSprites() {
		switch (type) {
		case WorldObjectTypes.TerrainCell: 

			componentManager.initSprites (cell_object.getRenderData().getSpriteCount());
			List<GameObject> sprites = componentManager.getSpriteComponent().getAttachedSprites();
			Dictionary<string, int> sprite_ids = cell_object.getRenderData().sprite_ids;

			if(sprites.Count != sprite_ids.Count)
				throw new InvalidOperationException("Something went wrong here...");

			// TODO: render the sprites from the ids
			int i = 0;
			foreach (KeyValuePair<string, int> spr_id in sprite_ids) {
				// set the sprite
				sprites[i].GetComponent<SpriteRenderer> ().sprite = SpriteLoader.GetTerrainSprite(spr_id.Key);

				// add border overlap offset if necessary (for borders which overlap any other border)
				float over_offset = 0f;
				if(SpriteLoader.SpriteIsOverlapping(spr_id.Key))
					over_offset = 0.05f;

				// adjust y index
				sprites[i].transform.position = new Vector3(
					sprites[i].transform.position.x,
					sprites[i].transform.position.y + spr_id.Value*0.1f + over_offset,
					sprites[i].transform.position.z
				);
				++i;
			}

			break;		
		}
	}

}
