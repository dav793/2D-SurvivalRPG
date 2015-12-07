using UnityEngine;
using System.Collections;

public class RenderSettings : MonoBehaviour {

	public static RenderSettings LoadedConfig;

	public int LevelDifferenceY;			// Y unit difference between 2 adjacent levels.
	public int LevelOffsetZ;				// Z unit offset between 2 adjacent levels.
	public int TerrainBaseY;				// Y unit around which terrain objects are based.
	public int WorldObjectBaseY;			// Y unit around which world objects are based.

	// Establish static (global) reference
	void Awake() {
		if (LoadedConfig == null) {
			LoadedConfig = this;
			//DontDestroyOnLoad(LoadedConfig);
		}
		else if (LoadedConfig != this) {
			Destroy(gameObject);
		}
	}

}
