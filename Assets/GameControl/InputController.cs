using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {
	
	public KeyCode saveWorld;
	public KeyCode loadWorld;

	void Update() {

		if (Input.GetKeyDown(saveWorld)) {
			WorldSerializer.SaveLoadedChunks ();
		}

		if (Input.GetKeyDown(loadWorld)) {
			WorldSerializer.LoadChunk (0, 0);
		}

	}

}
