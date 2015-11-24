using UnityEngine;
using System;
using System.Collections;

public class WorldSectorLevel {

    public int x, y, z;
    WorldCell[,] cells;

    public WorldSectorLevel(int sector_x, int level_y, int sector_z) {
        x = sector_x;
        z = sector_z;
        y = level_y;

        initCells();
    }

    void initCells() {

        // declare cells
        cells = new WorldCell[
            GameSettings.LoadedConfig.SectorLength_Cells,
            GameSettings.LoadedConfig.SectorLength_Cells
        ];

        // init cells
        for (int cell_x = 0; cell_x < GameSettings.LoadedConfig.SectorLength_Cells; ++cell_x) {
            for (int cell_z = 0; cell_z < GameSettings.LoadedConfig.SectorLength_Cells; ++cell_z) {
                cells[cell_x, cell_z] = new WorldCell(
                    x * GameSettings.LoadedConfig.SectorLength_Cells + cell_x, 
                    y, 
                    z * GameSettings.LoadedConfig.SectorLength_Cells + cell_z
                );
            }
        }

    }

	public WorldCell getCell(int cell_x, int cell_z) {

		if (!(
			cell_x >= x * GameSettings.LoadedConfig.SectorLength_Cells &&
			cell_x < (x + 1) * GameSettings.LoadedConfig.SectorLength_Cells &&
			cell_z >= z * GameSettings.LoadedConfig.SectorLength_Cells &&
			cell_z < (z + 1) * GameSettings.LoadedConfig.SectorLength_Cells
		))
			throw new InvalidOperationException ("Cell ("+cell_x+","+cell_z+") does not belong to sector ("+x+","+z+").");

		return cells[
			cell_x % GameSettings.LoadedConfig.SectorLength_Cells,
		    cell_z % GameSettings.LoadedConfig.SectorLength_Cells
		];

	}

	public string indexToString() {
		return "Sector: (" + x + ", " + z + ") level: "+y;
	}

}
