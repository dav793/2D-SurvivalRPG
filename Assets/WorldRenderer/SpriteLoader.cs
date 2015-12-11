using UnityEngine;
using System;
using System.Collections;

public class SpriteLoader : MonoBehaviour {

	static int grass_base_tilecount = 1;
	static int dirt_base_tilecount = 3;

	public static Sprite GetTerrainSprite(string id) {

		string filename;
		int subindex;
		GetTerrainSpriteFilename (id, out filename, out subindex);
		//Debug.Log (id+", "+filename+", "+subindex);

		Sprite[] sprites = Resources.LoadAll<Sprite> ("artwork/terrain/"+filename);
		if (sprites == null)
			throw new InvalidOperationException ("Resource not found.");

		return sprites[subindex];

	}

	static void GetTerrainSpriteFilename(string id, out string filename, out int index) {
			
		filename = "RedError";
		index = Int32.Parse (GetTerrainSpriteIdToken (id));

		switch (GetTerrainSpriteBiomeToken (id)) {

			case "grass":
				switch (GetTerrainSpriteTypeToken (id)) {

					case "base":
						filename = "grass_tiles";
						switch (GetTerrainSpriteGroupToken (id)) {
							case "short":
								index += grass_base_tilecount*0;
							break;
							case "med":
								index += grass_base_tilecount*1;
							break;
							case "tall":
								index += grass_base_tilecount*2;
							break;
						}
					break;

					case "border":
						filename = "grass_borders";
						switch (GetTerrainSpriteGroupToken (id)) {
							case "short":
								index += 0;
							break;
							case "med":
								index += 12;
							break;
							case "tall":
								index += 24;
							break;
						}
					break;
					
					case "decal":
						filename = "grass_decals";
					break;

				}
			break;

			case "dirt":
				switch (GetTerrainSpriteTypeToken (id)) {
					
					case "base":
						filename = "dirt_tiles";
						index = dirt_base_tilecount * index + UnityEngine.Random.Range(0, dirt_base_tilecount);
					break;

					case "border":
					break;

					case "decal":
					break;
				
				}
			break;

		}

	}

	public static string GetTerrainSpriteBiomeToken(string sprite_id) {
		return GetSpriteIdTokens (sprite_id) [0];
	}
	
	public static string GetTerrainSpriteGroupToken(string sprite_id) {
		return GetSpriteIdTokens (sprite_id) [1];
	}
	
	public static string GetTerrainSpriteTypeToken(string sprite_id) {
		return GetSpriteIdTokens (sprite_id) [2];
	}
	
	public static string GetTerrainSpriteIdToken(string sprite_id) {
		return GetSpriteIdTokens (sprite_id) [3];
	}

	static string[] GetSpriteIdTokens(string sprite_id) {
		return sprite_id.Split ('_');
	}

	public static bool SpriteIsBase(string sprite_id) {
		if (SpriteLoader.GetTerrainSpriteTypeToken (sprite_id) == "base")
			return true;
		return false;
	}

	public static bool SpriteIsBorder(string sprite_id) {
		if (SpriteLoader.GetTerrainSpriteTypeToken (sprite_id) == "border")
			return true;
		return false;
	}

	public static bool SpriteIsOverlapping(string sprite_id) {
		if (
			SpriteLoader.SpriteIsBorder (sprite_id) && 
			Int32.Parse (SpriteLoader.GetTerrainSpriteIdToken (sprite_id)) >= 8 &&
			Int32.Parse (SpriteLoader.GetTerrainSpriteIdToken (sprite_id)) <= 11
		)
			return true;
		return false;
	}

}
