﻿using UnityEngine;
using System;
using System.Collections;

public enum LoadedChunks_DataStructure { HashTable, List };

public class LoadedWorldChunks {

	LoadedChunks_DataStructure dataStructure;
	WorldChunkHashTable loadedWorldChunks_HT;
	WorldChunkList loadedWorldChunks_L;

	public LoadedWorldChunks(LoadedChunks_DataStructure data_structure) {
		dataStructure = data_structure;

		switch (dataStructure) {
			case LoadedChunks_DataStructure.HashTable:
				initLoadedChunksHT ();
				break;

			case LoadedChunks_DataStructure.List:
				initLoadedChunksL ();
				break;
		}

	}

	public void loadChunk(WorldChunk chunk) {
		switch (dataStructure) {
			case LoadedChunks_DataStructure.HashTable:
				loadedWorldChunks_HT.addChunk(chunk);
				break;
				
			case LoadedChunks_DataStructure.List:
				loadedWorldChunks_L.addChunk(chunk);
				break;
		}
	}

	public void unloadChunk(WorldChunk chunk) {
		switch (dataStructure) {
			case LoadedChunks_DataStructure.HashTable:
				loadedWorldChunks_HT.removeChunk(chunk);
				break;
				
			case LoadedChunks_DataStructure.List:
				loadedWorldChunks_L.removeChunk(chunk);
				break;
		}
	}

	public bool chunkIsLoaded(int abs_chunk_index) {
		switch (dataStructure) {
			case LoadedChunks_DataStructure.HashTable:
				return loadedWorldChunks_HT.chunkExists(abs_chunk_index);
				
			case LoadedChunks_DataStructure.List:
				return loadedWorldChunks_L.chunkExists(abs_chunk_index);
		}
		return false;
	}

	public WorldChunk getChunk(int x, int z) {
		switch (dataStructure) {
			case LoadedChunks_DataStructure.HashTable:
				return loadedWorldChunks_HT.findChunk(WorldChunk.GetAbsoluteIndex(x, z));
				
			case LoadedChunks_DataStructure.List:
				return loadedWorldChunks_L.findChunk(WorldChunk.GetAbsoluteIndex(x, z));
		}
		return null;
	}

	void initLoadedChunksHT() {
		
		if (GameSettings.LoadedConfig.ChunkHT_BucketSize <= 0)
			throw new InvalidOperationException ("Loaded chunk HT bucket size must be greater than 0.");
		
		if (GameSettings.LoadedConfig.WorldLength_Chunks % GameSettings.LoadedConfig.ChunkHT_BucketSize == 0)
			Debug.LogWarning("For better loaded chunk distribution, world chunk border length should not be divisible by chunk HT bucket size.");
		
		loadedWorldChunks_HT = new WorldChunkHashTable ();
		
	}
	
	void initLoadedChunksL() {
		loadedWorldChunks_L = new WorldChunkList ();
	}

}