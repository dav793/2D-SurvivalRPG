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

    }

}
