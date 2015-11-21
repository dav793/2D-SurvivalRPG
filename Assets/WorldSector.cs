using System;
using System.Collections.Generic;

public class WorldSector {

    public int x, z;
    public SectorLevels levels;

    public WorldSector(int x_index, int z_index) {
        x = x_index;
        z = z_index;

        initLevels();
    }

    void initLevels() {
        levels = new SectorLevels(x, z);
        
        // Init default level (y = 0)
        levels.initLevel(0);
    }

	public string indexToString() {
		return "Sector: (" + x + ", " + z + ")";
	}

}

public class SectorLevels {

    int x;
    int z;
    List<WorldSectorLevel> levels;

    public SectorLevels(int sector_x, int sector_z) {
        x = sector_x;
        z = sector_z;
        levels = new List<WorldSectorLevel>();   
    }

    public void initLevel(int level_y) {
        levels.Add(new WorldSectorLevel(x, level_y, z));
    }

    public bool levelExists(int level_y) {
        for (int i = 0; i < levels.Count; ++i) {
            if (levels[i].y == level_y)
                return true;
        }
        return false;
    }

    public WorldSectorLevel getLevel(int level_y) {
        for (int i = 0; i < levels.Count; ++i) {
            if (levels[i].y == level_y)
                return levels[i];
        }
        return null;
    }

}
