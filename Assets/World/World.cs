using UnityEngine;
using System;
using System.Collections;

public class World {

    public static World GameWorld;

	LoadedWorldChunks loadedChunks;

    public static void Init() {
        if (GameWorld == null) {
            
			GameWorld = new World();

			// Load initial world chunks
			Debug.Log("Creating world...");
			// Generate world here maybe?
			Debug.Log("World created.");

        }
        else {
            throw new InvalidOperationException("A World object already exists.");
        }
    }

    public World() {
		// Init loaded world chunks
		loadedChunks = new LoadedWorldChunks (LoadedChunks_DataStructure.List);
    }

	public static void LoadChunk(WorldChunk chunk) {
		GameWorld.loadedChunks.loadChunk (chunk);
	}

	public static void UnloadChunk(int chunk_x, int chunk_z) {
		GameWorld.loadedChunks.unloadChunk (chunk_x, chunk_z);
	}

	public static void UnloadChunk(WorldChunk chunk) {
		GameWorld.loadedChunks.unloadChunk (chunk.getX(), chunk.getZ());
	}

	bool chunkIsLoaded(int abs_chunk_index) {
		return loadedChunks.chunkIsLoaded (abs_chunk_index);
	}

	public static WorldChunk GetChunk(int x, int z) {
		if (GameWorld.chunkIsLoaded (WorldChunk.GetAbsoluteIndex (x, z))) {
			// if chunk is already loaded
			return GameWorld.loadedChunks.getChunk(x, z);		// get chunk	
		} 
		else {
			// if chunk is not loaded
			if (!GameWorld.loadedChunks.loadChunk(x, z)) {
				// Chunk not found in the world file. Generate a new chunk.
				World.LoadChunk(new WorldChunk (x, z));
			}
			return GameWorld.loadedChunks.getChunk(x, z);
		}
	}

	public static WorldSector GetSector(int x, int z) {
		WorldChunk chunk = World.GetChunk (
			x / GameSettings.LoadedConfig.ChunkLength_Sectors, 
			z / GameSettings.LoadedConfig.ChunkLength_Sectors
		);
		return chunk.getSector (x, z);
	}

	public static WorldSectorLevel GetSectorLevel(int x, int y, int z) {
		return World.GetSector (x, z).levels.getLevel (y);
	}

	public static WorldCell GetCell(int x, int y, int z) {

		if (x < 0 || z < 0)
			throw new InvalidOperationException ("Cell ("+x+","+y+","+z+") is out of world bounds.");

		WorldSectorLevel level = World.GetSectorLevel (
			x / GameSettings.LoadedConfig.SectorLength_Cells,
			y,
			z / GameSettings.LoadedConfig.SectorLength_Cells
		);
		
		return level.getCell (x, z);

	}

	public static WorldCell GetCell(int x, int z) {
		return World.GetCell (x, 0, z);
	}

	public static WorldSectorLevel GetSectorLevelFromCellIndex(int cell_x, int cell_y, int cell_z) {
		return World.GetSectorLevel (
			cell_x / GameSettings.LoadedConfig.SectorLength_Cells,
			cell_y,
			cell_z / GameSettings.LoadedConfig.SectorLength_Cells
		);
	}

	public static WorldSectorLevel GetSectorLevelFromCell(WorldCell cell) {
		return GetSectorLevelFromCellIndex (cell.x, cell.y, cell.z);
	}

	public static WorldSector GetSectorFromSectorLevel(WorldSectorLevel level) {
		return World.GetSector (level.x, level.z);
	}

	public static WorldChunk GetChunkFromSector(WorldSector sector) {
		return World.GetChunk (
			sector.x / GameSettings.LoadedConfig.ChunkLength_Sectors,
			sector.z / GameSettings.LoadedConfig.ChunkLength_Sectors
		);
	}

}
