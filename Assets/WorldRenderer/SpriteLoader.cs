using UnityEngine;
using System;
using System.Collections;

public class SpriteLoader : MonoBehaviour {

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
								index += 0;
							break;
							case "med":
								index += 1;
							break;
							case "tall":
								index += 2;
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

}
