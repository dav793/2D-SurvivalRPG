using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour {

    public static GameSettings LoadedConfig;

	public int WorldLength_Chunks;				// Length of World border in number of chunks
    public int ChunkLength_Sectors;             // Length of Chunk border in number of Sectors
    public int SectorLength_Cells;              // Length of Sector border in number of Cells
    public int CellLength_Pixels;               // Length of Cell border in number of pixels

	public int ChunkHT_BucketSize;				// Bucket size of loaded world chunk hash table (should not be a multiple of world chunk length)

    // Establish static (global) reference
    void Awake() {
        if (LoadedConfig == null) {
            LoadedConfig = this;
            //DontDestroyOnLoad(LoadedConfig);
        }
        else if (LoadedConfig != this) {
            Destroy(gameObject);
        }
    }

	public int getWorldLength_Sectors() {
		return WorldLength_Chunks * ChunkLength_Sectors;
	}

	public int getWorldLength_Cells() {
		return getWorldLength_Sectors() * SectorLength_Cells;
	}

	public int getTotalChunks() {
		return WorldLength_Chunks * WorldLength_Chunks;
	}

}
