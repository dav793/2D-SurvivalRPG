using UnityEngine;
using System.Collections;

public class WorldCell {

    public int x, y, z;
	WorldSectorLevel parent;

	// Render properties
	WorldCellRenderData renderData;
	RendererWorldObject renderedObject;

    public WorldCell(int cell_x, int cell_y, int cell_z, WorldSectorLevel parent) {

        x = cell_x;
        y = cell_y;
        z = cell_z;
		this.parent = parent;

		renderData = new WorldCellRenderData ();

    }

	public void restoreCellData(SerializableCell restoreFrom) {
		renderData.restoreSpriteIds (restoreFrom.sprite_ids);
	}

	public WorldCell getNeighborCell(NeighborDirections direction) {

		int local_x, local_z;
		getLocalIndex (out local_x, out local_z);

		switch (direction) {
		case NeighborDirections.N:
			if(local_z < GameSettings.LoadedConfig.SectorLength_Cells-1) {
				return parent.getCell (x, z+1);
			}
			else {
				WorldSector sc = parent.getParent().getNeighborSector (NeighborDirections.N);
				if (sc != null)
					return sc.levels.getLevel(y).getCell(local_x, 0);
			}
			break;
		case NeighborDirections.NE:
			if(local_x < GameSettings.LoadedConfig.SectorLength_Cells-1 && local_z < GameSettings.LoadedConfig.SectorLength_Cells-1) {
				return parent.getCell (x+1, z+1);
			}
			else {
				if(local_x == GameSettings.LoadedConfig.SectorLength_Cells-1 && local_z == GameSettings.LoadedConfig.SectorLength_Cells-1) {
					WorldSector sc = parent.getParent().getNeighborSector (NeighborDirections.NE);
					if (sc != null)
						return sc.levels.getLevel(y).getCell(0, 0);
				} 
				else if (local_x == GameSettings.LoadedConfig.SectorLength_Cells-1) {
					WorldSector sc = parent.getParent().getNeighborSector (NeighborDirections.E);
					if (sc != null)
						return sc.levels.getLevel(y).getCell(0, z+1);
				}
				else if (local_z == GameSettings.LoadedConfig.SectorLength_Cells-1) {
					WorldSector sc = parent.getParent().getNeighborSector (NeighborDirections.N);
					if (sc != null)
						return sc.levels.getLevel(y).getCell(x+1, 0);
				}
			}
			break;
		case NeighborDirections.E:
			if(local_x < GameSettings.LoadedConfig.SectorLength_Cells-1) {
				return parent.getCell (x+1, z);
			}
			else {
				WorldSector sc = parent.getParent().getNeighborSector (NeighborDirections.E);
				if (sc != null)
					return sc.levels.getLevel(y).getCell(0, local_z);
			}
			break;
		case NeighborDirections.SE:
			if(local_x < GameSettings.LoadedConfig.SectorLength_Cells-1 && local_z > 0) {
				return parent.getCell (x+1, z-1);
			}
			else {
				if(local_x == GameSettings.LoadedConfig.SectorLength_Cells-1 && local_z == 0) {
					WorldSector sc = parent.getParent().getNeighborSector (NeighborDirections.SE);
					if (sc != null)
						return sc.levels.getLevel(y).getCell(0, GameSettings.LoadedConfig.SectorLength_Cells-1);
				} 
				else if (local_x == GameSettings.LoadedConfig.SectorLength_Cells-1) {
					WorldSector sc = parent.getParent().getNeighborSector (NeighborDirections.E);
					if (sc != null)
						return sc.levels.getLevel(y).getCell(0, z-1);
				}
				else if (local_z == 0) {
					WorldSector sc = parent.getParent().getNeighborSector (NeighborDirections.S);
					if (sc != null)
						return sc.levels.getLevel(y).getCell(x+1, GameSettings.LoadedConfig.SectorLength_Cells-1);
				}
			}
			break;
		case NeighborDirections.S:
			if(local_z > 0) {
				return parent.getCell (x, z-1);
			}
			else {
				WorldSector sc = parent.getParent().getNeighborSector (NeighborDirections.S);
				if (sc != null)
					return sc.levels.getLevel(y).getCell(local_x, GameSettings.LoadedConfig.SectorLength_Cells-1);
			}
			break;
		case NeighborDirections.SW:
			if(local_x > 0 && local_z > 0) {
				return parent.getCell (x-1, z-1);
			}
			else {
				if(local_x == 0 && local_z == 0) {
					WorldSector sc = parent.getParent().getNeighborSector (NeighborDirections.SW);
					if (sc != null)
						return sc.levels.getLevel(y).getCell(GameSettings.LoadedConfig.SectorLength_Cells-1, GameSettings.LoadedConfig.SectorLength_Cells-1);
				} 
				else if (local_x == 0) {
					WorldSector sc = parent.getParent().getNeighborSector (NeighborDirections.W);
					if (sc != null)
						return sc.levels.getLevel(y).getCell(GameSettings.LoadedConfig.SectorLength_Cells-1, z-1);
				}
				else if (local_z == 0) {
					WorldSector sc = parent.getParent().getNeighborSector (NeighborDirections.S);
					if (sc != null)
						return sc.levels.getLevel(y).getCell(x-1, GameSettings.LoadedConfig.SectorLength_Cells-1);
				}
			}
			break;
		case NeighborDirections.W:
			if(local_x > 0) {
				return parent.getCell (x-1, z);
			}
			else {
				WorldSector sc = parent.getParent().getNeighborSector (NeighborDirections.W);
				if (sc != null)
					return sc.levels.getLevel(y).getCell(GameSettings.LoadedConfig.SectorLength_Cells-1, local_z);
			}
			break;
		case NeighborDirections.NW:
			if(local_x > 0 && local_z < GameSettings.LoadedConfig.SectorLength_Cells-1) {
				return parent.getCell (x-1, z+1);
			}
			else {
				if(local_x == 0 && local_z == GameSettings.LoadedConfig.SectorLength_Cells-1) {
					WorldSector sc = parent.getParent().getNeighborSector (NeighborDirections.NW);
					if (sc != null)
						return sc.levels.getLevel(y).getCell(GameSettings.LoadedConfig.SectorLength_Cells-1, 0);
				} 
				else if (local_x == 0) {
					WorldSector sc = parent.getParent().getNeighborSector (NeighborDirections.W);
					if (sc != null)
						return sc.levels.getLevel(y).getCell(GameSettings.LoadedConfig.SectorLength_Cells-1, z+1);
				}
				else if (local_z == GameSettings.LoadedConfig.SectorLength_Cells-1) {
					WorldSector sc = parent.getParent().getNeighborSector (NeighborDirections.N);
					if (sc != null)
						return sc.levels.getLevel(y).getCell(x-1, 0);
				}
			}
			break;
		}

		return null;
	}

	void getLocalIndex(out int local_x, out int local_z) {
		local_x = x % GameSettings.LoadedConfig.SectorLength_Cells;
		local_z = z % GameSettings.LoadedConfig.SectorLength_Cells;
	}

	public WorldSectorLevel getParent() {
		return parent;
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
