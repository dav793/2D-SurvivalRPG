using UnityEngine;
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
                //Debug.Log("Created cell: (" + (x * GameSettings.LoadedConfig.SectorLength_Cells + cell_x) + ", " + (z * GameSettings.LoadedConfig.SectorLength_Cells + cell_z) + ")");
            }
        }

    }

    public WorldCell getCell(int cell_x, int cell_z) {
        return cells[
            cell_x % GameSettings.LoadedConfig.SectorLength_Cells,
            cell_z % GameSettings.LoadedConfig.SectorLength_Cells
        ];
    }

}
