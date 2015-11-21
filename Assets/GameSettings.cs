using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour {

    public static GameSettings LoadedConfig;

    public int WorldLength_Sectors;             // Length of World side in number of Sectors
    public int SectorLength_Cells;              // Length of Sector side in number of Cells
    public int CellLength_Pixels;               // Length of Cell side in number of pixels

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

    public int getWorldLength_Cells() {
        return WorldLength_Sectors * SectorLength_Cells;
    }

}
