using UnityEngine;
using System.Collections;

public class StyleRenderer : MonoBehaviour {

	public virtual void init() {}

	public virtual void renderSectorLevel(WorldSectorLevel sectorLevel) {}

	public virtual void unrenderSectorLevel(WorldSectorLevel sectorLevel) {}

	public virtual void renderCell(WorldCell cell) {}

	public virtual void unrenderCell(WorldCell cell) {}

}
