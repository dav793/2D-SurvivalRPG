using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class WOSpriteComponent : WOComponent {

	List<GameObject> attachedSprites;
	
	public override void init() {
		type = WOComponentType.SpriteComponent;

		if (attachedSprites != null)
			throw new InvalidOperationException ("There are already sprites attached to this SpriteComponent.");

		attachedSprites = new List<GameObject> ();
	}

	public override void terminate() {
		detachSprites ();
	}

	public void attachSprites(int spriteCount) {
		for (int i = 0; i < spriteCount; ++i) {
			attachSprite ();	
		}
	}

	public void detachSprites() {
		for (; attachedSprites.Count > 0 ;) {
			detachSprite(attachedSprites[0]);
		}
		attachedSprites = null;
	}

	public List<GameObject> getAttachedSprites() {
		return attachedSprites;
	}

	public void attachSprite() {
		GameObject spr = ((Renderer2D)RenderingController.ActiveRenderer).worldObjectComponentPools.spritePool.pop();
		spr.transform.parent = transform;
		attachedSprites.Add (spr);
	}

	void detachSprite(GameObject spr) {
		attachedSprites.Remove (spr);
		((Renderer2D)RenderingController.ActiveRenderer).worldObjectComponentPools.spritePool.pushAndResetParent (spr);
	}

}
