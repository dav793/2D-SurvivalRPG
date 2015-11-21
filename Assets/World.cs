using UnityEngine;
using System;
using System.Collections;

public class World {

    public static World GameWorld;

    WorldSector[,] sectors;

    public static void Init() {
        if (GameWorld == null) {
            GameWorld = new World();
        }
        else {
            throw new InvalidOperationException("A World object already exists.");
        }
    }

    public World() {
        Debug.Log("Creating world...");
        initSectors();
        Debug.Log("World created.");
    }

    void initSectors() {
        sectors = new WorldSector[
            GameSettings.LoadedConfig.WorldLength_Sectors,
            GameSettings.LoadedConfig.WorldLength_Sectors
        ];

        for (int x = 0; x < GameSettings.LoadedConfig.WorldLength_Sectors; ++x) {
            for (int z = 0; z < GameSettings.LoadedConfig.WorldLength_Sectors; ++z) {
                sectors[x, z] = new WorldSector(x, z);
            }
        }
    }

    public WorldSector getSector(int sector_x, int sector_z) {
        if (!sectorIndexExists(sector_x, sector_z)) {
            return null;
        }
        return sectors[sector_x, sector_z];
    }

    public WorldSector getSectorFromCellIndex(int cell_x, int cell_z) {
        return getSector(
            Mathf.FloorToInt(cell_x / GameSettings.LoadedConfig.SectorLength_Cells),
			Mathf.FloorToInt(cell_z / GameSettings.LoadedConfig.SectorLength_Cells)
        );
    }

    public WorldCell getCell(int cell_x, int cell_z) {
        return getCell(cell_x, 0, cell_z);
    }

    public WorldCell getCell(int cell_x, int cell_y, int cell_z) {
        if (!cellIndexExists(cell_x, cell_y, cell_z)) {
            return null;
        }
        return getSectorFromCellIndex(cell_x, cell_z).levels.getLevel(cell_y).getCell(cell_x, cell_z);
    }

    bool sectorIndexExists(int sector_x, int sector_z) {
        if (sector_x >= 0 && sector_x < GameSettings.LoadedConfig.WorldLength_Sectors && sector_z >= 0 && sector_z < GameSettings.LoadedConfig.WorldLength_Sectors) {
            return true;
        }
        return false;
    }

    bool sectorLevelExists(WorldSector sector, int level_y) {
        return sector.levels.levelExists(level_y);
    }

    bool cellIndexExists(int cell_x, int cell_z) {
        return cellIndexExists(cell_x, 0, cell_z);
    }

    bool cellIndexExists(int cell_x, int cell_y, int cell_z) {

        // Check if X and Z exist
        if (cell_x < 0 || cell_x >= GameSettings.LoadedConfig.getWorldLength_Cells() || cell_z < 0 || cell_z >= GameSettings.LoadedConfig.getWorldLength_Cells())
            return false;

        // Check if Y (level) exists
        if (!sectorLevelExists(getSectorFromCellIndex(cell_x, cell_z), cell_y))
            return false;

        return true;

    }

}
