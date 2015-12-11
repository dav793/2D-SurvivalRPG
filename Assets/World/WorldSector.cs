using UnityEngine;
using System;
using System.Collections.Generic;

public class WorldSector {

    public int x, z;
    public SectorLevels levels;

	WorldChunk parent;

    public WorldSector(int x_index, int z_index, WorldChunk parent) {
		//Debug.Log ("------Creating sector: (" + x_index + ", " + z_index + ")");
		x = x_index;
        z = z_index;
		this.parent = parent;

        initLevels();
    }

    void initLevels() {
        levels = new SectorLevels(x, z, this);
        
        // Init default level (y = 0)
        levels.initLevel(0);
    }

	public void restoreSectorData(SerializableSector restoreFrom) {
		levels.restoreLevelsData (restoreFrom.levels);
	}

	public List<WorldSectorLevel> getAllLevels() {
		return levels.getAllLevels ();
	}

	public WorldSector getNeighborSector(NeighborDirections direction) {
		
		int local_x, local_z;
		getLocalIndex (out local_x, out local_z);
		
		switch (direction) {
		case NeighborDirections.N:
			if(local_z < GameSettings.LoadedConfig.ChunkLength_Sectors-1) {
				return parent.getSector (x, z+1);
			}
			else {
				WorldChunk ch = parent.getNeighborChunk (NeighborDirections.N);
				if(ch != null)
					return ch.getSector(local_x, 0);
			}
			break;
		case NeighborDirections.NE:
			if(local_x < GameSettings.LoadedConfig.ChunkLength_Sectors-1 && local_z < GameSettings.LoadedConfig.ChunkLength_Sectors-1) {
				return parent.getSector (x+1, z+1);
			}
			else {
				WorldChunk ch = parent.getNeighborChunk (NeighborDirections.NE);
				if(ch != null)
					return ch.getSector(0, 0);
			}
			break;
		case NeighborDirections.E:
			if(local_x < GameSettings.LoadedConfig.ChunkLength_Sectors-1) {
				return parent.getSector (x+1, z);
			}
			else {
				WorldChunk ch = parent.getNeighborChunk (NeighborDirections.E);
				if(ch != null)
					return ch.getSector(0, local_z);
			}
			break;
		case NeighborDirections.SE:
			if(local_x < GameSettings.LoadedConfig.ChunkLength_Sectors-1 && local_z > 0) {
				return parent.getSector (x+1, z-1);
			}
			else {
				WorldChunk ch = parent.getNeighborChunk (NeighborDirections.SE);
				if(ch != null)
					return ch.getSector(0, GameSettings.LoadedConfig.ChunkLength_Sectors-1);
			}
			break;
		case NeighborDirections.S:
			if(local_z > 0) {
				return parent.getSector (x, z-1);
			}
			else {
				WorldChunk ch = parent.getNeighborChunk (NeighborDirections.S);
				if(ch != null)
					return ch.getSector(local_x, GameSettings.LoadedConfig.ChunkLength_Sectors-1);
			}
			break;
		case NeighborDirections.SW:
			if(local_x > 0 && local_z > 0) {
				return parent.getSector (x-1, z-1);
			}
			else {
				WorldChunk ch = parent.getNeighborChunk (NeighborDirections.SW);
				if(ch != null)
					return ch.getSector(GameSettings.LoadedConfig.ChunkLength_Sectors-1, GameSettings.LoadedConfig.ChunkLength_Sectors-1);
			}
			break;
		case NeighborDirections.W:
			if(local_x > 0) {
				return parent.getSector (x-1, z);
			}
			else {
				WorldChunk ch = parent.getNeighborChunk (NeighborDirections.W);
				if(ch != null)
					return ch.getSector(GameSettings.LoadedConfig.ChunkLength_Sectors-1, local_z);
			}
			break;
		case NeighborDirections.NW:
			if(local_x > 0 && local_z < GameSettings.LoadedConfig.ChunkLength_Sectors-1) {
				return parent.getSector (x-1, z+1);
			}
			else {
				WorldChunk ch = parent.getNeighborChunk (NeighborDirections.NW);
				if(ch != null)
					return ch.getSector(GameSettings.LoadedConfig.ChunkLength_Sectors-1, 0);
			}
			break;
		}
		
		return null;
	}

	void getLocalIndex(out int local_x, out int local_z) {
		local_x = x % GameSettings.LoadedConfig.ChunkLength_Sectors;
		local_z = z % GameSettings.LoadedConfig.ChunkLength_Sectors;
	}

	public WorldChunk getParent() {
		return parent;
	}

	public string indexToString() {
		return "Sector: (" + x + ", " + z + ")";
	}

}

public class SectorLevels {

    int x;
    int z;
    List<WorldSectorLevel> levels;
	WorldSector parent;

    public SectorLevels(int sector_x, int sector_z, WorldSector parent) {
        x = sector_x;
        z = sector_z;
        levels = new List<WorldSectorLevel>();   
		this.parent = parent;
	}

    public void initLevel(int level_y) {
		if (!levelExists (level_y))
			levels.Add (new WorldSectorLevel (x, level_y, z, parent));
		else
			throw new InvalidOperationException ("Level " + level_y + " already exists in sector " + x + ", " + z + ".");
	}

	public void restoreLevelsData(List<SerializableLevel> restoreFrom) {
	
		for (int i = 0; i < restoreFrom.Count; ++i) {
			levels[i].restoreLevelData(restoreFrom[i]);
		}

	}

    public bool levelExists(int level_y) {
        for (int i = 0; i < levels.Count; ++i) {
            if (levels[i].y == level_y)
                return true;
        }
        return false;
    }

    public WorldSectorLevel getLevel(int level_y) {
		if (!levelExists (level_y))
			initLevel (level_y);

        for (int i = 0; i < levels.Count; ++i) {
            if (levels[i].y == level_y)
                return levels[i];
        }

        return null;
    }

	public List<WorldSectorLevel> getAllLevels() {
		return levels;
	}

}
