using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class WorldSectorLevel {

    public int x, y, z;
    WorldCell[,] cells;
	WorldSector parent;

	// Render properties
	public bool isRendered = false;

    public WorldSectorLevel(int sector_x, int level_y, int sector_z, WorldSector parent) {
        x = sector_x;
        z = sector_z;
        y = level_y;
		this.parent = parent;

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
					z * GameSettings.LoadedConfig.SectorLength_Cells + cell_z,
					this
                );
            }
        }

    }

	public void restoreLevelData(SerializableLevel restoreFrom) {
	
		// restore cells
		for (int cell_x = 0; cell_x < GameSettings.LoadedConfig.SectorLength_Cells; ++cell_x) {
			for (int cell_z = 0; cell_z < GameSettings.LoadedConfig.SectorLength_Cells; ++cell_z) {
				SerializableCell loadedCell = restoreFrom.cells[
					cell_z * GameSettings.LoadedConfig.SectorLength_Cells + cell_x
				];
				cells[cell_x, cell_z].restoreCellData(loadedCell);
			}
		}

	}

	public WorldCell getCell(int cell_x, int cell_z) {

		/*if (!(
			cell_x >= x * GameSettings.LoadedConfig.SectorLength_Cells &&
			cell_x < (x + 1) * GameSettings.LoadedConfig.SectorLength_Cells &&
			cell_z >= z * GameSettings.LoadedConfig.SectorLength_Cells &&
			cell_z < (z + 1) * GameSettings.LoadedConfig.SectorLength_Cells
		))
			throw new InvalidOperationException ("Cell ("+cell_x+","+cell_z+") does not belong to sector ("+x+","+z+").");*/

		if (!(
			cell_x < GameSettings.LoadedConfig.WorldLength_Chunks * GameSettings.LoadedConfig.ChunkLength_Sectors * GameSettings.LoadedConfig.SectorLength_Cells &&
			cell_x >= 0 &&
			cell_z < GameSettings.LoadedConfig.WorldLength_Chunks * GameSettings.LoadedConfig.ChunkLength_Sectors * GameSettings.LoadedConfig.SectorLength_Cells &&
			cell_z >= 0
		))
			throw new InvalidOperationException ("Cell ("+cell_x+","+cell_z+") does not exist.");

		return cells[
			cell_x % GameSettings.LoadedConfig.SectorLength_Cells,
		    cell_z % GameSettings.LoadedConfig.SectorLength_Cells
		];

	}

	public WorldCell[,] getAllCells() {
		return cells;
	}

	public WorldSector getParent() {
		return parent;
	}

	public List<WorldCell> getBorderCells(NeighborDirections border_side) {
		List<WorldCell> border = new List<WorldCell> ();
		switch (border_side) {
		case NeighborDirections.N:
			for(int i = 0; i < GameSettings.LoadedConfig.SectorLength_Cells; ++i) {
				border.Add(getCell(i, GameSettings.LoadedConfig.SectorLength_Cells-1));
			}
			return border;
		case NeighborDirections.NE:
			border.Add(getCell(GameSettings.LoadedConfig.SectorLength_Cells-1, GameSettings.LoadedConfig.SectorLength_Cells-1));
			return border;
		case NeighborDirections.E:
			for(int i = 0; i < GameSettings.LoadedConfig.SectorLength_Cells; ++i) {
				border.Add(getCell(GameSettings.LoadedConfig.SectorLength_Cells-1, i));
			}
			return border;
		case NeighborDirections.SE:
			border.Add(getCell(GameSettings.LoadedConfig.SectorLength_Cells-1, 0));
			return border;
		case NeighborDirections.S:
			for(int i = 0; i < GameSettings.LoadedConfig.SectorLength_Cells; ++i) {
				border.Add(getCell(i, 0));
			}
			return border;
		case NeighborDirections.SW:
			border.Add(getCell(0, 0));
			return border;
		case NeighborDirections.W:
			for(int i = 0; i < GameSettings.LoadedConfig.SectorLength_Cells; ++i) {
				border.Add(getCell(0, i));
			}
			return border;
		case NeighborDirections.NW:
			border.Add(getCell(0, GameSettings.LoadedConfig.SectorLength_Cells-1));
			return border;
		}
		return null;
	}

	public string indexToString() {
		return "SectorLevel: (" + x + ", " + y + ", " + z + ")";
	}

}
