using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.IO.Compression;

public class WorldSerializer : MonoBehaviour {
	
	public static WorldSerializer WSerializer;
	
	WorldData wdata;
	string chunksDirName = "chunks";
	string chunkFilePrefix = "chunk_";
	
	// Establish static (global) reference
	void Awake() {
		if (WSerializer == null) {
			WSerializer = this;
			//DontDestroyOnLoad(GController);
		}
		else if (WSerializer != this) {
			Destroy(gameObject);
		}
	}
	
	public static void SaveLoadedChunks() {
		
		string savesDirPath = Path.Combine (Application.persistentDataPath, GameSettings.LoadedConfig.SavesPath);
		bool savesDirExists = System.IO.Directory.Exists (savesDirPath);
		if (!savesDirExists) {
			System.IO.Directory.CreateDirectory(savesDirPath);
		}
		
		string activeWorldDirPath = Path.Combine (savesDirPath, GameSettings.LoadedConfig.ActiveWorldDir);
		bool activeWorldDirExists = System.IO.Directory.Exists (activeWorldDirPath);
		if (!activeWorldDirExists) {
			System.IO.Directory.CreateDirectory(activeWorldDirPath);
		}
		
		string chunksDirPath = Path.Combine (activeWorldDirPath, WSerializer.chunksDirName);
		bool chunksDirExists = System.IO.Directory.Exists (chunksDirPath);
		if (!chunksDirExists) {
			System.IO.Directory.CreateDirectory(chunksDirPath);
		}
		
		WSerializer.serializeWorldData ();
		
		List<SerializableChunk> chunks = WSerializer.wdata.chunks;
		for (int i = 0; i < chunks.Count; ++i) {
			
			string chunkFilePath = Path.Combine (chunksDirPath, WSerializer.chunkFilePrefix + chunks[i].x + "-" + chunks[i].z + ".sav");
			
			Debug.Log("Chunk: ("+chunks[i].x+", "+chunks[i].z+") was saved.");
			
			XmlSerializer serializer = new XmlSerializer (typeof(SerializableChunk));
			FileStream stream = new FileStream (chunkFilePath, FileMode.Create);
			serializer.Serialize (stream, chunks[i]);
			stream.Close ();

		}
		
	}
	
	public static WorldChunk LoadChunk(int x, int z) {
		
		string savesDirPath = Path.Combine (Application.persistentDataPath, GameSettings.LoadedConfig.SavesPath);
		bool savesDirExists = System.IO.Directory.Exists (savesDirPath);
		if (!savesDirExists) {
			return null;
		}
		
		string activeWorldDirPath = Path.Combine (savesDirPath, GameSettings.LoadedConfig.ActiveWorldDir);
		bool activeWorldDirExists = System.IO.Directory.Exists (activeWorldDirPath);
		if (!activeWorldDirExists) {
			return null;
		}
		
		string chunksDirPath = Path.Combine (activeWorldDirPath, WSerializer.chunksDirName);
		bool chunksDirExists = System.IO.Directory.Exists (chunksDirPath);
		if (!chunksDirExists) {
			return null;
		}
		
		string chunkFilePath = Path.Combine (chunksDirPath, WSerializer.chunkFilePrefix + x + "-" + z + ".sav");
		bool chunkFileExists = System.IO.File.Exists (chunkFilePath);
		if (!chunkFileExists) {
			return null;
		}
		
		XmlSerializer serializer = new XmlSerializer (typeof(SerializableChunk));
		FileStream stream = new FileStream (chunkFilePath, FileMode.Open);
		SerializableChunk loadedChunk = serializer.Deserialize (stream) as SerializableChunk;
		stream.Close ();
		
		WorldChunk restoredChunk = new WorldChunk (x, z);
		restoredChunk.restoreChunkData (loadedChunk);
		Debug.Log("Chunk: ("+x+", "+z+") was restored from file.");
		
		return restoredChunk;
		
	}
	
	void serializeWorldData() {
		
		List<WorldChunk> loadedChunks = World.GetAllLoadedChunks ();
		wdata = new WorldData ();
		
		for (int i = 0; i < loadedChunks.Count; ++i) {
			wdata.chunks.Add(new SerializableChunk());
			serializeChunkData(wdata.chunks[i], loadedChunks[i]);
		}
		
	}
	
	void serializeChunkData(SerializableChunk copyTo, WorldChunk copyFrom) {
		
		// save indexes
		copyTo.x = copyFrom.getX();
		copyTo.z = copyFrom.getZ();
		
		// save chunk sectors
		WorldSector[,] sectors = copyFrom.getAllSectors();
		for (int i = 0; i < GameSettings.LoadedConfig.ChunkLength_Sectors*GameSettings.LoadedConfig.ChunkLength_Sectors; ++i) {
			copyTo.sectors.Add(new SerializableSector());
			
			int sx = i%GameSettings.LoadedConfig.ChunkLength_Sectors;
			int sz = Mathf.FloorToInt(i/GameSettings.LoadedConfig.ChunkLength_Sectors);
			serializeSectorData(copyTo.sectors[i], sectors[sx, sz]);
		}
		
	}
	
	void serializeSectorData(SerializableSector copyTo, WorldSector copyFrom) {
		
		// save indexes
		copyTo.x = copyFrom.x;
		copyTo.z = copyFrom.z;
		
		// save sector levels
		List<WorldSectorLevel> levels = copyFrom.levels.getAllLevels();
		for (int i = 0; i < levels.Count; ++i) {
			copyTo.levels.Add(new SerializableLevel());
			
			serializeLevelData(copyTo.levels[i], levels[i]);
		}
		
	}
	
	void serializeLevelData(SerializableLevel copyTo, WorldSectorLevel copyFrom) {
		
		// save index
		copyTo.y = copyFrom.y;
		
		// save level cells
		WorldCell[,] cells = copyFrom.getAllCells();
		for (int i = 0; i < GameSettings.LoadedConfig.SectorLength_Cells*GameSettings.LoadedConfig.SectorLength_Cells; ++i) {
			copyTo.cells.Add(new SerializableCell());
			
			int cx = i%GameSettings.LoadedConfig.SectorLength_Cells;
			int cz = Mathf.FloorToInt(i/GameSettings.LoadedConfig.SectorLength_Cells);
			serializeCellData(copyTo.cells[i], cells[cx, cz]);
		}
		
	}
	
	void serializeCellData(SerializableCell copyTo, WorldCell copyFrom) {
		
		// save cell indexes
		copyTo.x = copyFrom.x;
		copyTo.y = copyFrom.y;
		copyTo.z = copyFrom.z;
		
		// save sprite ids 
		Dictionary<string, int> sprite_ids = copyFrom.getRenderData ().sprite_ids;
		foreach (KeyValuePair<string, int> sprite in sprite_ids) {
			SerializableSpriteId spr = new SerializableSpriteId();
			spr.id = sprite.Key;
			spr.y = sprite.Value;
			copyTo.sprite_ids.Add(spr);
		}
		
	}
	
}

public class WorldData {
	
	public List<SerializableChunk> chunks = new List<SerializableChunk> ();
	
	public SerializableChunk findChunk(int x, int z) {
		for (int i = 0; i < chunks.Count; ++i) {
			if(chunks[i].x == x && chunks[i].z == z)
				return chunks[i];
		}
		return null;
	}
	
}

[XmlRoot("Chunk")]
public class SerializableChunk {
	[XmlAttribute("x")]
	public int x;
	[XmlAttribute("z")]
	public int z;
	
	[XmlArray("Sectors")]
	[XmlArrayItem("Sector")]
	public List<SerializableSector> sectors = new List<SerializableSector> ();
} 

public class SerializableSector {
	[XmlAttribute("x")]
	public int x;
	[XmlAttribute("z")]
	public int z;
	
	[XmlArray("Levels")]
	[XmlArrayItem("Level")]
	public List<SerializableLevel> levels = new List<SerializableLevel> ();
}

public class SerializableLevel {
	/*[XmlAttribute("x")]
	public int sector_x;
	[XmlAttribute("z")]
	public int sector_z;*/
	[XmlAttribute("y")]
	public int y;
	
	[XmlArray("Cells")]
	[XmlArrayItem("Cell")]
	public List<SerializableCell> cells = new List<SerializableCell> ();
}

public class SerializableCell {
	[XmlAttribute("x")]
	public int x;
	[XmlAttribute("y")]
	public int y;
	[XmlAttribute("z")]
	public int z;
	
	[XmlArray("SpriteIds")]
	[XmlArrayItem("SpriteId")]
	public List<SerializableSpriteId> sprite_ids = new List<SerializableSpriteId> ();
}

public class SerializableSpriteId {
	[XmlAttribute("id")]
	public string id;
	[XmlAttribute("y")]
	public int y;
}

