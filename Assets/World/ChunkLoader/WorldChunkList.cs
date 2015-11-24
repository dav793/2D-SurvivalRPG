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
		if (!chunkExists (chunk))
			chunks.Add (chunk);
	}

	public void removeChunk(WorldChunk chunk) {
		chunks.Remove (chunk);
	}

	public bool chunkExists(WorldChunk chunk) {
		if (chunks.Contains (chunk))
			return true;
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

}
