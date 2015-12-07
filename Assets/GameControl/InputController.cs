using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {
	
	public KeyCode saveWorld;
	public KeyCode loadWorld;

	void Update() {

		if (Input.GetKeyDown(saveWorld)) {
			Debug.Log("Saving world: "+GameSettings.LoadedConfig.ActiveWorldFile);
			WorldSerializer.SaveLoadedChunks ();
		}

		if (Input.GetKeyDown(loadWorld)) {
			Debug.Log("Loading world: "+GameSettings.LoadedConfig.ActiveWorldFile);
			WorldSerializer.LoadChunk (0, 0);
		}

	}

}
