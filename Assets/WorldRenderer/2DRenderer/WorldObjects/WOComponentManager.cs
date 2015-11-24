using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum WorldObjectTypes { Unassigned, TerrainCell };

public class WOComponentManager : MonoBehaviour {

	public GameObject spriteComponentPrefab;

	public WorldObjectTypes type = WorldObjectTypes.Unassigned;
	List<WOComponent> attachedComponents;

	public void init(WorldObjectTypes type) {

		if (!checkComponentIntegrity ())
			throw new InvalidOperationException ("Some component prefabs have not been assigned.");

		this.type = type;
		setupComponents ();

	}

	public void terminate() {
		type = WorldObjectTypes.Unassigned;
		detachComponents ();
	}

	void setupComponents() {

		if (attachedComponents != null)
			throw new InvalidOperationException ("World Object already has components attached to it.");

		attachComponents (type);

	}

	void attachComponents(WorldObjectTypes type) {

		attachedComponents = new List<WOComponent> ();

		switch (type) {
		
			case WorldObjectTypes.TerrainCell:		// Attach components for Terrain Cell object.
				
				// Attach sprite component.
				GameObject spriteComponentObj = Instantiate(spriteComponentPrefab) as GameObject;
				spriteComponentObj.transform.parent = transform;

				WOSpriteComponent spriteComponent = spriteComponentObj.GetComponent<WOSpriteComponent>();
				if(spriteComponent == null)
					throw new InvalidOperationException("Sprite Component prefab is malformed or not set correctly.");
				
				attachedComponents.Add(spriteComponent);
				attachedComponents[attachedComponents.Count-1].init();

				break;
		
		}

	}

	void detachComponents() {
		for (; attachedComponents.Count > 0 ;) {
			detachComponent(attachedComponents[0]);
		}
	}

	void detachComponent(WOComponent component) {
		component.terminate ();
		attachedComponents.Remove (component);
		Destroy (component.gameObject);
	}

	bool checkComponentIntegrity() {
		if (spriteComponentPrefab == null)
			return false;
		return true;
	}

}
