using UnityEngine;
using System.Collections;

public class WorldCell {

    public int x, y, z;

    public WorldCell(int cell_x, int cell_y, int cell_z) {

        x = cell_x;
        y = cell_y;
        z = cell_z;

    }

	/*public void getIndexInSector(out int x_in_sector, out int z_in_sector) {
		x_in_sector = x % GameSettings.LoadedConfig.SectorLength_Cells;
		z_in_sector = z % GameSettings.LoadedConfig.SectorLength_Cells;
	}

	public void getSectorIndex(out int sector_x, out int sector_z) {
		sector_x = Mathf.FloorToInt (x / GameSettings.LoadedConfig.SectorLength_Cells);
		sector_z = Mathf.FloorToInt (z / GameSettings.LoadedConfig.SectorLength_Cells);
	}*/

    public string indexToString() {
        return "Cell: (" + x + ", " + y + ", " + z + ")";
    }

	/*public string indexInSectorToString() {
		int x_in_sector, z_in_sector, sector_x, sector_z;
		getIndexInSector (out x_in_sector, out z_in_sector);
		getSectorIndex (out sector_x, out sector_z);

		return "Cell: (" + x_in_sector + ", " + y + ", " + z_in_sector + ") in sector: (" + sector_x + ", " + sector_z + ")";
	}*/

}
