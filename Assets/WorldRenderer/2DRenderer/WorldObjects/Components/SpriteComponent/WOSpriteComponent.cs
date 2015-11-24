using UnityEngine;
using System.Collections;

public class WOSpriteComponent : WOComponent {

	public GameObject spritePrefab;

	public override void init() {
		type = WOComponentType.SpriteComponent;
	}

	public override void terminate() {

	}

}
