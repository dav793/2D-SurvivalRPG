using UnityEngine;
using System;
using System.Collections;

public class WorldChunk {
	
	int x, z;
	WorldSector[,] sectors;

	public WorldChunk(int index_x, int index_z) {
		//Debug.Log ("---Creating chunk: (" + index_x + ", " + index_z + ")");
		checkIndexIntegrity (index_x, index_z);
		x = index_x;
		z = index_z;

		initSectors ();
	}

	void initSectors() {

		// declare sectors
		sectors = new WorldSector[
			GameSettings.LoadedConfig.ChunkLength_Sectors,
			GameSettings.LoadedConfig.ChunkLength_Sectors
		];
		
		// init sectors
		for (int sector_x = 0; sector_x < GameSettings.LoadedConfig.ChunkLength_Sectors; ++sector_x) {
			for (int sector_z = 0; sector_z < GameSettings.LoadedConfig.ChunkLength_Sectors; ++sector_z) {
				sectors[sector_x, sector_z] = new WorldSector(
					x * GameSettings.LoadedConfig.ChunkLength_Sectors + sector_x, 
					z * GameSettings.LoadedConfig.ChunkLength_Sectors + sector_z
				);
			}
		}

	}

	public void restoreChunkData(SerializableChunk restoreFrom) {
	
		// restore sectors
		for (int sector_x = 0; sector_x < GameSettings.LoadedConfig.ChunkLength_Sectors; ++sector_x) {
			for (int sector_z = 0; sector_z < GameSettings.LoadedConfig.ChunkLength_Sectors; ++sector_z) {
				SerializableSector loadedSector = restoreFrom.sectors[
					sector_z * GameSettings.LoadedConfig.ChunkLength_Sectors + sector_x       
				];
				sectors[sector_x, sector_z].restoreSectorData(loadedSector);
			}
		}

	}

	public int getX() {
		return x;
	}

	public int getZ() {
		return z;
	}

	public WorldSector getSector(int sector_x, int sector_z) {

		if (!(
			sector_x >= x * GameSettings.LoadedConfig.ChunkLength_Sectors && 
			sector_x < (x + 1) * GameSettings.LoadedConfig.ChunkLength_Sectors &&
			sector_z >= z * GameSettings.LoadedConfig.ChunkLength_Sectors && 
			sector_z < (z + 1) * GameSettings.LoadedConfig.ChunkLength_Sectors
		))
			throw new InvalidOperationException ("Sector ("+sector_x+","+sector_z+") does not belong to chunk ("+x+", "+z+").");

		return sectors[
			sector_x % GameSettings.LoadedConfig.ChunkLength_Sectors, 
		    sector_z % GameSettings.LoadedConfig.ChunkLength_Sectors
		];

	}

	public WorldSector[,] getAllSectors() {
		return sectors;
	}

	void checkIndexIntegrity(int index_x, int index_z) {
		if (!(
			index_x >= 0 && 
			index_x < GameSettings.LoadedConfig.WorldLength_Chunks && 
			index_z >= 0 && 
			index_z < GameSettings.LoadedConfig.WorldLength_Chunks
		))
			throw new InvalidOperationException("Chunk index is out of world bounds.");
	}

	public string indexToString() {
		return "Chunk: (" + x + ", " + z + ")";
	}
	
	public string absIndexToString() {
		return "Chunk: " + GetAbsoluteIndex(x, z);
	}

	public static int GetAbsoluteIndex(int index_x, int index_z) {
		return index_z * GameSettings.LoadedConfig.WorldLength_Chunks + index_x;
	}

}
