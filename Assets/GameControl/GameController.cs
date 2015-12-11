using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public static GameController GController;

    // Establish static (global) reference
    void Awake() {
        if (GController == null) {
            GController = this;
            //DontDestroyOnLoad(GController);
        }
        else if (GController != this) {
            Destroy(gameObject);
        }
    }

    void Start() {

        // Create world
        World.Init();

        // Set renderer
        RenderingController.Init();

		Debug.Log ("...All systems ready.");




		// test

		RenderingController.RenderSectorLevel (0, 0, 0);
		RenderingController.RenderSectorLevel (1, 0, 0);
		RenderingController.RenderSectorLevel (2, 0, 0);
		RenderingController.RenderSectorLevel (0, 0, 1);
		RenderingController.RenderSectorLevel (1, 0, 1);
		RenderingController.RenderSectorLevel (2, 0, 1);
		RenderingController.RenderSectorLevel (0, 0, 2);
		RenderingController.RenderSectorLevel (1, 0, 2);
		RenderingController.RenderSectorLevel (2, 0, 2);

		// end test

    }

}
