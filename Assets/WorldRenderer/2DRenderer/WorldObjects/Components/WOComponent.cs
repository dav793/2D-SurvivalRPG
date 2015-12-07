using UnityEngine;
using System.Collections;

public enum WOComponentType { SpriteComponent };

public class WOComponent : MonoBehaviour {

	[HideInInspector]
	public WOComponentType type;

	public virtual void init() {}

	public virtual void terminate() {}

}
