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
		//addSprite ("dirt_light_base_0", 0);
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

		if (sprite_id == "" || sprite_id == null) {
			return;
		}

		// add (or replace) the sprite
		sprite_ids.Remove (sprite_id);
		sprite_ids.Add (sprite_id, y_index);

	}

	public int findSpriteId(string sprite_id) {
		foreach (KeyValuePair<string, int> spr in sprite_ids) {
			if(spr.Key == sprite_id)
				return spr.Value;
		}
		return -9999;
	}

	public bool containsBiomeGroupBase(string biome, string group) {
		//check if any bases are of biome and group
		foreach (KeyValuePair<string, int> spr in sprite_ids) {
			if(SpriteLoader.SpriteIsBase(spr.Key)) {
				if(
					biome == SpriteLoader.GetTerrainSpriteBiomeToken(spr.Key) &&
					group == SpriteLoader.GetTerrainSpriteGroupToken(spr.Key)
					)
					return true;
			}
		}
		
		return false;
	}

	public bool containsBiomeGroupBase(string biome, string group, out int index_y) {
		//check if any bases are of biome and group
		foreach (KeyValuePair<string, int> spr in sprite_ids) {
			if(SpriteLoader.SpriteIsBase(spr.Key)) {
				if(
					biome == SpriteLoader.GetTerrainSpriteBiomeToken(spr.Key) &&
					group == SpriteLoader.GetTerrainSpriteGroupToken(spr.Key)
				) {
					index_y = spr.Value;
					return true;
				}
			}
		}

		index_y = -9999;
		return false;
	}

	public int getBiomeGroupBaseY(string biome, string group) {
		//check if any bases are of biome and group
		foreach (KeyValuePair<string, int> spr in sprite_ids) {
			if(SpriteLoader.SpriteIsBase(spr.Key)) {
				if(
					biome == SpriteLoader.GetTerrainSpriteBiomeToken(spr.Key) &&
					group == SpriteLoader.GetTerrainSpriteGroupToken(spr.Key)
					)
					return spr.Value;
			}
		}
		return -9999;
	}

	public bool spriteIdIsActiveBase(string sprite_id) {
		int active_base_y = -9999;
		
		// first, find the Y of the active (highest-y) base
		foreach (KeyValuePair<string, int> spr in sprite_ids) {
			if(SpriteLoader.SpriteIsBase(spr.Key)) {
				if(active_base_y < spr.Value)
					active_base_y = spr.Value;
			}
		}
		
		// then, check if sprite_id (if exists) is the active base
		int y_index = findSpriteId(sprite_id);
		if(y_index != -9999 && y_index == active_base_y)
			return true;
		
		return false;
	}

	public void removeBorderSprites() {
		List<string> ids = new List<string> ();
		foreach (KeyValuePair<string, int> spr in sprite_ids) {
			if (SpriteLoader.SpriteIsBorder(spr.Key))
				ids.Add(spr.Key);
		}
		for (int i = 0; i < ids.Count; ++i) {
			sprite_ids.Remove(ids[i]);
		}
	}

	public void restoreSpriteIds(List<SerializableSpriteId> restoreFrom) {
		sprite_ids = new Dictionary<string, int> ();
		for (int i = 0; i < restoreFrom.Count; ++i) {
			sprite_ids.Add(restoreFrom[i].id, restoreFrom[i].y);
		}
	}

	/*public bool biomeGroupIsActiveBase(string biome, string group) {
		int active_base_y = -9999;

		// first, find the Y of the active (highest-y) base
		foreach (KeyValuePair<string, int> spr in sprite_ids) {
			if(spriteIsBase(spr.Key)) {
				if(active_base_y < spr.Value)
					active_base_y = spr.Value;
			}
		}

		// then, check if any bases are of biome and group and are at least as high as the active base
		foreach (KeyValuePair<string, int> spr in sprite_ids) {
			if(spriteIsBase(spr.Key)) {
				if(
					biome == SpriteLoader.GetTerrainSpriteBiomeToken(spr.Key) &&
					group == SpriteLoader.GetTerrainSpriteGroupToken(spr.Key)
				) {
					if(spr.Value >= active_base_y)
						return true;
				}
			}
		}

		return false;
	}*/

}
