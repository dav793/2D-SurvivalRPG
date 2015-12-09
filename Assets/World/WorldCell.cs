using UnityEngine;
using System.Collections;

public class WorldCell {

    public int x, y, z;

	// Render properties
	WorldCellRenderData renderData;
	RendererWorldObject renderedObject;

    public WorldCell(int cell_x, int cell_y, int cell_z) {

        x = cell_x;
        y = cell_y;
        z = cell_z;

		renderData = new WorldCellRenderData ();

    }

	public void restoreCellData(SerializableCell restoreFrom) {
		renderData.restoreSpriteIds (restoreFrom.sprite_ids);
	}

	public string indexToString() {
		return "Cell: (" + x + ", " + y + ", " + z + ")";
	}


	// Render functions

	public void attachRenderObject(RendererWorldObject obj) {
		if (renderedObject == null)
			renderedObject = obj;
	}

	public void detachRenderObject() {
		renderedObject = null;
	}

	public RendererWorldObject getRenderObject() {
		if (renderedObject == null)
			Debug.LogError ("Cell is not rendered!");
		return renderedObject;
	}

	public WorldCellRenderData getRenderData() {
		if (renderData == null)
			Debug.LogError ("Cell does not have a Render Data element!");
		return renderData;
	}

	/*public void getIndexInSector(out int x_in_sector, out int z_in_sector) {
		x_in_sector = x % GameSettings.LoadedConfig.SectorLength_Cells;
		z_in_sector = z % GameSettings.LoadedConfig.SectorLength_Cells;
	}

	public void getSectorIndex(out int sector_x, out int sector_z) {
		sector_x = Mathf.FloorToInt (x / GameSettings.LoadedConfig.SectorLength_Cells);
		sector_z = Mathf.FloorToInt (z / GameSettings.LoadedConfig.SectorLength_Cells);
	}*/

	/*public string indexInSectorToString() {
		int x_in_sector, z_in_sector, sector_x, sector_z;
		getIndexInSector (out x_in_sector, out z_in_sector);
		getSectorIndex (out sector_x, out sector_z);

		return "Cell: (" + x_in_sector + ", " + y + ", " + z_in_sector + ") in sector: (" + sector_x + ", " + sector_z + ")";
	}*/

}
