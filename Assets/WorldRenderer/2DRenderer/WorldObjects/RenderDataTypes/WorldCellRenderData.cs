using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldCellRenderData : RenderData {

	public Dictionary<string, int> sprite_ids;

	public WorldCellRenderData() {
		sprite_ids = new Dictionary<string, int> ();
		setDefaultBaseSprite ();
	}

	public void setDefaultBaseSprite() {
		addSprite ("grass_short_base_0", 1);
		//addSprite ("grass_tall_border_7", 3);
	}

	public int getSpriteCount() {
		return sprite_ids.Count;
	}

	public void addSprite(string sprite_id, int y_index) {

		/*if (spriteIsBase (sprite_id)) {
			// remove any other base sprites already here
			List<string> markedForRemoval = new List<string> ();
			foreach (KeyValuePair<string, int> spr in sprite_ids) {
				if(spriteIsBase(spr.Key))
					markedForRemoval.Add(spr.Key);
			}
			for (int i = 0; i < markedForRemoval.Count; ++i) {
				sprite_ids.Remove (markedForRemoval[i]);
			}
		}*/

		// add the sprite if it's not already contained
		if (!sprite_ids.ContainsKey(sprite_id))
			sprite_ids.Add (sprite_id, y_index);

	}

	public void restoreSpriteIds(List<SerializableSpriteId> restoreFrom) {
		sprite_ids = new Dictionary<string, int> ();
		for (int i = 0; i < restoreFrom.Count; ++i) {
			sprite_ids.Add(restoreFrom[i].id, restoreFrom[i].y);
		}
	}

	bool spriteIsBase(string sprite_id) {
		if (SpriteLoader.GetTerrainSpriteTypeToken (sprite_id) == "base")
			return true;
		return false;
	}

}
