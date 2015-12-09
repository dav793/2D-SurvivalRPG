using UnityEngine;
using System;
using System.Collections;

public class WorldSectorLevel {

    public int x, y, z;
    WorldCell[,] cells;

	// Render properties
	public bool isRendered = false;

    public WorldSectorLevel(int sector_x, int level_y, int sector_z) {
        x = sector_x;
        z = sector_z;
        y = level_y;

        initCells();
    }

    void initCells() {

        // declare cells
        cells = new WorldCell[
            getSectorLengthInCells(),
            getSectorLengthInCells()
        ];

        // init cells
		for (int cell_x = 0; cell_x < getSectorLengthInCells(); ++cell_x) {
			for (int cell_z = 0; cell_z < getSectorLengthInCells(); ++cell_z) {
                cells[cell_x, cell_z] = new WorldCell(
					x * getSectorLengthInCells() + cell_x, 
                    y, 
					z * getSectorLengthInCells() + cell_z
                );
            }
        }

    }

	public void restoreLevelData(SerializableLevel restoreFrom) {
	
		// restore cells
		for (int cell_x = 0; cell_x < getSectorLengthInCells(); ++cell_x) {
			for (int cell_z = 0; cell_z < getSectorLengthInCells(); ++cell_z) {
				SerializableCell loadedCell = restoreFrom.cells[
					cell_z * getSectorLengthInCells() + cell_x
				];
				cells[cell_x, cell_z].restoreCellData(loadedCell);
			}
		}

	}

	public WorldCell getCell(int cell_x, int cell_z) {

		if (!(
			cell_x >= x * getSectorLengthInCells() &&
			cell_x < (x + 1) * getSectorLengthInCells() &&
			cell_z >= z * getSectorLengthInCells() &&
			cell_z < (z + 1) * getSectorLengthInCells()
		))
			throw new InvalidOperationException ("Cell ("+cell_x+","+cell_z+") does not belong to sector ("+x+","+z+").");

		return cells[
			cell_x % getSectorLengthInCells(),
		    cell_z % getSectorLengthInCells()
		];

	}

	public WorldCell[,] getAllCells() {
		return cells;
	}

	public int getSectorLengthInCells() {
		return GameSettings.LoadedConfig.SectorLength_Cells;
	}

	public string indexToString() {
		return "SectorLevel: (" + x + ", " + y + ", " + z + ")";
	}

}
