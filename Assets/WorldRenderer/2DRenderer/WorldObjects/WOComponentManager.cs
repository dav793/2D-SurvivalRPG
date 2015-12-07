using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class WOComponentManager : MonoBehaviour {

	RendererWorldObject objectHandle;
	List<WOComponent> attachedComponents;

	public void init(RendererWorldObject handle) {
		objectHandle = handle;
		setupComponents ();
	}

	public void terminate() {
		detachComponents ();
	}

	void setupComponents() {

		if (attachedComponents != null)
			throw new InvalidOperationException ("World Object already has components attached to it.");

		attachComponents ();

	}

	void attachComponents() {

		attachedComponents = new List<WOComponent> ();

		switch (objectHandle.type) {
		
			case WorldObjectTypes.TerrainCell:		// Attach components for Terrain Cell object.
				
				// Attach sprite component.
				WOSpriteComponent spriteComponent = ((Renderer2D)RenderingController.ActiveRenderer).worldObjectComponentPools.spriteComponentPool.pop().GetComponent<WOSpriteComponent> ();
				spriteComponent.transform.parent = transform;
				attachedComponents.Add(spriteComponent);
				
				spriteComponent.init ();
				//attachedComponents[attachedComponents.Count-1].init();

				break;
		
		}

	}

	void detachComponents() {
		for (; attachedComponents.Count > 0 ;) {
			detachComponent(attachedComponents[0]);
		}
		attachedComponents = null;
	}

	void detachComponent(WOComponent component) {
		attachedComponents.Remove (component);
		component.terminate ();
		((Renderer2D)RenderingController.ActiveRenderer).worldObjectComponentPools.spriteComponentPool.pushAndResetParent (component.gameObject);
	}

	public WOSpriteComponent getSpriteComponent() {
		for (int i = 0; i < attachedComponents.Count; ++i) {
			if ((WOSpriteComponent)attachedComponents[i] != null)
				return (WOSpriteComponent)attachedComponents[i];
		}
		return null;
	}

	public void initSprites(int num_sprites) {
		WOSpriteComponent spriteComponent = getSpriteComponent ();
		if(spriteComponent == null)
			throw new InvalidOperationException ("Object does not have a sprite component attached.");

		spriteComponent.attachSprites (num_sprites);
	}

}
