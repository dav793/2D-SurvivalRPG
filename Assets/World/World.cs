using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum NeighborDirections {N, NE, E, SE, S, SW, W, NW};

public class World {

    public static World GameWorld;

	LoadedWorldChunks loadedChunks = new LoadedWorldChunks (LoadedChunks_DataStructure.List);

    public static void Init() {
        if (GameWorld == null) {
            
			Debug.Log("Preparing world...");

			GameWorld = new World();

			// Generate world here maybe? (Load initial world chunks)

			Debug.Log("World ready.");

        }
        else {
            throw new InvalidOperationException("A World object already exists.");
        }
    }

    public World() {

    }

	public static List<WorldChunk> GetAllLoadedChunks() {
		return GameWorld.loadedChunks.getAllLoadedChunks ();
	}

	public static bool LoadChunk(int chunk_x, int chunk_z) {
		return GameWorld.loadedChunks.loadChunk (chunk_x, chunk_z);
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

	public static WorldChunk GetChunk(int x, int z, bool generateIfNotExists = true) {
		if (GameWorld.chunkIsLoaded (WorldChunk.GetAbsoluteIndex (x, z))) {
			// chunk is already loaded, get loaded chunk
			return GameWorld.loadedChunks.getChunk(x, z);		// get chunk	
		} 
		else {
			// chunk is not loaded, try to restore it from world file
			if (!World.LoadChunk(x, z)) {
				// Chunk not found in the world file.
				if(generateIfNotExists)
					// Generate a new chunk and load it.
					World.LoadChunk(WorldChunkGenerator.GenerateChunk (x, z));
				else
					// Do not generate chunk.
					return null;
			}
			return GameWorld.loadedChunks.getChunk(x, z);
		}
	}

	public static WorldSector GetSector(int x, int z, bool generateIfNotExists = true) {
		WorldChunk chunk = World.GetChunk (
			x / GameSettings.LoadedConfig.ChunkLength_Sectors, 
			z / GameSettings.LoadedConfig.ChunkLength_Sectors,
			generateIfNotExists
		);

		if (chunk != null)
			return chunk.getSector (x, z);
		else 
			return null;
	}

	public static WorldSectorLevel GetSectorLevel(int x, int y, int z, bool generateIfNotExists = true) {
		WorldSector sector = World.GetSector (x, z, generateIfNotExists);
		if (sector != null)
			return sector.levels.getLevel (y);
		else
			return null;
	}

	public static WorldCell GetCell(int x, int y, int z, bool generateIfNotExists = true) {
		
		if (x < 0 || z < 0)
			throw new InvalidOperationException ("Cell ("+x+","+y+","+z+") is out of world bounds.");

		WorldSectorLevel level = World.GetSectorLevel (
			x / GameSettings.LoadedConfig.SectorLength_Cells,
			y,
			z / GameSettings.LoadedConfig.SectorLength_Cells,
			generateIfNotExists
		);

		if (level != null)
			return level.getCell (x, z);
		else 
			return null;

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
