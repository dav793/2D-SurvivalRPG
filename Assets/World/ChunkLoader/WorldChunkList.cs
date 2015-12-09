using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class WorldChunkList {

	List<WorldChunk> chunks;

	public WorldChunkList() {
		chunks = new List<WorldChunk> ();
	}

	public void addChunk(WorldChunk chunk) {
		if (!chunkExists (chunk.getX (), chunk.getZ ())) {
			chunks.Add (chunk);
			Debug.Log (chunk.indexToString () + " was loaded in memory.");
		}
	}

	public void removeChunk(int chunk_x, int chunk_z) {
		for (int i = 0; i < chunks.Count; ++i) {
			if (chunks[i].getX() == chunk_x && chunks[i].getZ() == chunk_z) {
				chunks.RemoveAt(i);
				Debug.Log ("Chunk: ("+chunk_x+", "+chunk_z+") was unloaded from memory.");
				break;
			}
		}
	}

	public bool chunkExists(int chunk_x, int chunk_z) {
		for (int i = 0; i < chunks.Count; ++i) {
			if (chunks[i].getX() == chunk_x && chunks[i].getZ() == chunk_z)
				return true;
		}
		return false;
	}

	public bool chunkExists(int abs_chunk_index) {
		for (int i = 0; i < chunks.Count; ++i) {
			if (WorldChunk.GetAbsoluteIndex(chunks[i].getX(), chunks[i].getZ()) == abs_chunk_index)
				return true;
		}
		return false;
	}

	public WorldChunk findChunk(int abs_chunk_index) {
		for (int i = 0; i < chunks.Count; ++i) {
			if (WorldChunk.GetAbsoluteIndex(chunks[i].getX(), chunks[i].getZ()) == abs_chunk_index)
				return chunks[i];
		}
		return null;
	}

	public List<WorldChunk> getAllChunks() {
		return chunks;
	}

}
