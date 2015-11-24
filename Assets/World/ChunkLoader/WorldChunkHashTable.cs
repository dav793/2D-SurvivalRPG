using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class WorldChunkHashTable {
	
	int totalBuckets;
	WorldChunkHTBucket[] buckets;
	
	public WorldChunkHashTable() {
		totalBuckets = Mathf.FloorToInt (GameSettings.LoadedConfig.getTotalChunks() / GameSettings.LoadedConfig.ChunkHT_BucketSize);
		buckets = new WorldChunkHTBucket[totalBuckets];
		for (int i = 0; i < totalBuckets; ++i) {
			buckets[i] = new WorldChunkHTBucket();
		}
	}

	public void addChunk(WorldChunk chunk) {
		if (!getBucket(chunk).elementExists(chunk))
			getBucket(chunk).addElement(chunk);
	}

	public void removeChunk(WorldChunk chunk) {
		getBucket(chunk).removeElement(chunk);
	}

	public bool chunkExists(int abs_chunk_index) {
		return getBucket (abs_chunk_index).elementExists (abs_chunk_index);
	}

	public WorldChunk findChunk(int abs_chunk_index) {
		return getBucket (abs_chunk_index).findElement (abs_chunk_index);
	}

	WorldChunkHTBucket getBucket(WorldChunk chunk) {
		return buckets [hash (WorldChunk.GetAbsoluteIndex (chunk.getX(), chunk.getZ()))];
	}

	WorldChunkHTBucket getBucket(int abs_chunk_index) {
		return buckets [hash (abs_chunk_index)];
	}

	int hash(int abs_chunk_index) {
		return abs_chunk_index % totalBuckets;
	}
	
}

public class WorldChunkHTBucket {
	
	List<WorldChunk> elements;
	
	public WorldChunkHTBucket() {
		elements = new List<WorldChunk> ();
	}
	
	public void addElement(WorldChunk elem) {
		if (elements.Count >= GameSettings.LoadedConfig.ChunkHT_BucketSize)
			Debug.LogWarning ("Loaded chunk HT bucket has exceeded it's max size.");

		elements.Add (elem);
	}
	
	public void removeElement(WorldChunk elem) {
		elements.Remove (elem);
	}

	public bool elementExists(WorldChunk elem) {
		return elements.Contains (elem);
	}

	public bool elementExists(int abs_chunk_index) {
		for (int i = 0; i < elements.Count; ++i) {
			if (WorldChunk.GetAbsoluteIndex(elements[i].getX(), elements[i].getZ()) == abs_chunk_index)
				return true;
		}
		return false;
	}

	public WorldChunk findElement(int abs_chunk_index) {
		for (int i = 0; i < elements.Count; ++i) {
			if (WorldChunk.GetAbsoluteIndex(elements[i].getX(), elements[i].getZ()) == abs_chunk_index)
				return elements[i];
		}
		return null;
	}
	
}