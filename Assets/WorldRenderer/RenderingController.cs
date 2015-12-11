using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum Renderers { Default2D, Default3D };

public class RenderingController : MonoBehaviour {

    public static RenderingController RendController;
    public static StyleRenderer ActiveRenderer;

    // Establish static (global) reference
    void Awake() {
        if (RendController == null) {
            RendController = this;
            //DontDestroyOnLoad(RendController);
        }
        else if (RendController != this) {
            Destroy(gameObject);
        }
    }

    public static void Init() {
		Debug.Log ("Setting up renderer...");
        SetActiveRenderer(Renderers.Default2D);
		ActiveRenderer.init ();
		Debug.Log ("Renderer ready.");
	}

    static void SetActiveRenderer(Renderers style) {

        switch (style) {

            case Renderers.Default2D:

                Transform rendObjTransform = RendController.transform.Find("Renderer2D");
                if (rendObjTransform == null) {
                    throw new InvalidOperationException("Selected renderer does not exist in the scene.");
                }

                StyleRenderer rendComponent = rendObjTransform.GetComponent<Renderer2D>();
                if (rendComponent == null) {
                    throw new InvalidOperationException("Selected renderer is missing renderer component.");
                }

                ActiveRenderer = rendComponent;

                break;

        }

    }

	public static void RenderSectorLevel(int x, int y, int z) {
		WorldSectorLevel sectorLevel = World.GetSectorLevel (x, y, z);
		ActiveRenderer.renderSectorLevel (sectorLevel);
	}

	public static void UnrenderSectorLevel(int x, int y, int z) {
		WorldSectorLevel sectorLevel = World.GetSectorLevel (x, y, z);
		ActiveRenderer.unrenderSectorLevel (sectorLevel);
	}

}
