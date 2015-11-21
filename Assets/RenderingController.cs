using UnityEngine;
using System;
using System.Collections;

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
        setActiveRenderer(Renderers.Default2D);
    }

    static void setActiveRenderer(Renderers style) {

        if (RendController == null) {
            throw new InvalidOperationException("RendererController reference is missing.");
        }

        switch (style) {

            case Renderers.Default2D:

                Transform rendObjTransform = RendController.transform.Find("Renderer2D");
                if (rendObjTransform == null) {
                    throw new InvalidOperationException("Selected renderer does not exist in the scene.");
                }

                StyleRenderer rendComponent = rendObjTransform.GetComponent<Renderer2D>();
                if (rendComponent == null) {
                    throw new InvalidOperationException("Selected renderer is missing component.");
                }

                ActiveRenderer = rendComponent;

                break;

        }

    }

}
