using UnityEngine;
using System;
using System.Collections;

public class World {

    public static World GameWorld;

	LoadedWorldChunks loadedChunks;

    public static void Init() {
        if (GameWorld == null) {
            GameWorld = new World();
        }
        else {
            throw new InvalidOperationException("A World object already exists.");
        }
    }

    public World() {

		// Init loaded world chunks
		loadedChunks = new LoadedWorldChunks (LoadedChunks_DataStructure.List);

		// Load initial world chunks
        Debug.Log("Creating world...");


		// Create a chunk. Load it. Retrieve it and check consistency. Unload it.

		//WorldChunk ch1 = new WorldChunk (0, 0);
		//WorldChunk ch2 = new WorldChunk (1, 1);
		//WorldChunk ch3 = new WorldChunk (8, 6);
		//Debug.Log (ch.indexToString());
		//Debug.Log (ch.absIndexToString());
		//Debug.Log ();

		//WorldChunk ch1 = getChunk (0, 0);
		//WorldChunk ch2 = getChunk (1, 1);
		//WorldChunk ch3 = getChunk (8, 6);
		//WorldChunk ch4 = getChunk (0, 0);

		//Debug.Log (ch1.indexToString());
		//Debug.Log (ch1.absIndexToString());

		//WorldSector sector1 = getSector (95, 95);
		//Debug.Log (sector1.indexToString());

		//WorldCell cell1 = getCell (999, 0, 999);
		//Debug.Log (cell1.indexToString());



        Debug.Log("World created.");

    }

	void loadChunk(WorldChunk chunk) {
		loadedChunks.loadChunk (chunk);
	}

	void unloadChunk(WorldChunk chunk) {
		loadedChunks.unloadChunk (chunk);
	}

	bool chunkIsLoaded(int abs_chunk_index) {
		return loadedChunks.chunkIsLoaded (abs_chunk_index);
	}

	public WorldChunk getChunk(int x, int z) {
		if (chunkIsLoaded (WorldChunk.GetAbsoluteIndex (x, z))) {
			// if chunk is already loaded
			return loadedChunks.getChunk(x, z);		// get chunk	
		} 
		else {
			// if chunk is not loaded
			loadChunk(new WorldChunk (x, z));		// create new chunk and load it (TODO: Change this)
													// (instead it should find it in the world file and then load it)
													// (if it doesn't exist in the world file THEN it should create it and save it to the world file)
			return loadedChunks.getChunk(x, z);		// get chunk
		}
	}

	public WorldSector getSector(int x, int z) {

		WorldChunk chunk = getChunk (
			x / GameSettings.LoadedConfig.ChunkLength_Sectors, 
			z / GameSettings.LoadedConfig.ChunkLength_Sectors
		);

		//Debug.Log (chunk.indexToString());	// TEST
		return chunk.getSector (x, z);

	}

	public WorldSectorLevel getSectorLevel(int x, int y, int z) {
		return getSector (x, z).levels.getLevel (y);
	}

	public WorldCell getCell(int x, int y, int z) {

		if (x < 0 || z < 0)
			throw new InvalidOperationException ("Cell ("+x+","+y+","+z+") is out of world bounds.");

		WorldSectorLevel level = getSectorLevel (
			x / GameSettings.LoadedConfig.SectorLength_Cells,
			y,
			z / GameSettings.LoadedConfig.SectorLength_Cells
		);

		//Debug.Log (level.indexToString());	// TEST
		return level.getCell (x, z);

	}

	public WorldCell getCell(int x, int z) {
		return getCell (x, 0, z);
	}

	public WorldSectorLevel getSectorLevelFromCellIndex(int cell_x, int cell_y, int cell_z) {
		return getSectorLevel (
			cell_x / GameSettings.LoadedConfig.SectorLength_Cells,
			cell_y,
			cell_z / GameSettings.LoadedConfig.SectorLength_Cells
		);
	}

	public WorldSector getSectorFromSectorLevel(WorldSectorLevel level) {
		return getSector (level.x, level.z);
	}

	public WorldChunk getChunkFromSector(WorldSector sector) {
		return getChunk (
			sector.x / GameSettings.LoadedConfig.ChunkLength_Sectors,
			sector.z / GameSettings.LoadedConfig.ChunkLength_Sectors
		);
	}

}
