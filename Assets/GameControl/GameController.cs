using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public static GameController GController;

    // Establish static (global) reference
    void Awake() {
        if (GController == null) {
            GController = this;
            //DontDestroyOnLoad(GController);
        }
        else if (GController != this) {
            Destroy(gameObject);
        }
    }

    void Start() {

        // Create world
        World.Init();

        // Set renderer
        RenderingController.Init();

		Debug.Log ("...All systems ready.");




		// test

		/*WorldSectorLevel sectorLevel = World.GetSectorLevel (0, 0, 0);
		WorldCell[,] cells = sectorLevel.getAllCells ();

		cells [4, 4].getRenderData ().addSprite ("grass_med_base_0", 2);
		cells [4, 5].getRenderData ().addSprite ("grass_med_base_0", 2);
		cells [5, 4].getRenderData ().addSprite ("grass_med_base_0", 2);
		cells [5, 5].getRenderData ().addSprite ("grass_med_base_0", 2);
		
		cells [3, 3].getRenderData ().addSprite ("grass_med_border_3", 2);
		cells [3, 4].getRenderData ().addSprite ("grass_med_border_4", 2);
		cells [3, 5].getRenderData ().addSprite ("grass_med_border_4", 2);
		cells [3, 6].getRenderData ().addSprite ("grass_med_border_0", 2);
		cells [4, 6].getRenderData ().addSprite ("grass_med_border_5", 2);
		cells [5, 6].getRenderData ().addSprite ("grass_med_border_5", 2);
		cells [6, 6].getRenderData ().addSprite ("grass_med_border_1", 2);
		cells [6, 5].getRenderData ().addSprite ("grass_med_border_6", 2);
		cells [6, 4].getRenderData ().addSprite ("grass_med_border_6", 2);
		cells [6, 3].getRenderData ().addSprite ("grass_med_border_2", 2);
		cells [5, 3].getRenderData ().addSprite ("grass_med_border_7", 2);
		cells [4, 3].getRenderData ().addSprite ("grass_med_border_7", 2);


		cells [5, 5].getRenderData ().addSprite ("grass_tall_base_0", 3);

		cells [4, 4].getRenderData ().addSprite ("grass_tall_border_3", 3);
		cells [4, 5].getRenderData ().addSprite ("grass_tall_border_4", 3);
		cells [4, 6].getRenderData ().addSprite ("grass_tall_border_0", 3);
		cells [5, 6].getRenderData ().addSprite ("grass_tall_border_5", 3);
		cells [6, 6].getRenderData ().addSprite ("grass_tall_border_1", 3);
		cells [6, 5].getRenderData ().addSprite ("grass_tall_border_6", 3);
		cells [6, 4].getRenderData ().addSprite ("grass_tall_border_2", 3);
		cells [5, 4].getRenderData ().addSprite ("grass_tall_border_7", 3);


		cells [2, 1].getRenderData ().addSprite ("grass__decal_0", 4);
		cells [9, 7].getRenderData ().addSprite ("grass__decal_0", 4);
		cells [2, 8].getRenderData ().addSprite ("grass__decal_0", 4);
		cells [1, 2].getRenderData ().addSprite ("grass__decal_1", 4);
		cells [3, 6].getRenderData ().addSprite ("grass__decal_1", 4);
		cells [1, 8].getRenderData ().addSprite ("grass__decal_2", 4);
		cells [3, 2].getRenderData ().addSprite ("grass__decal_3", 4);
		cells [6, 8].getRenderData ().addSprite ("grass__decal_3", 4);
		cells [9, 4].getRenderData ().addSprite ("grass__decal_3", 4);
		cells [5, 2].getRenderData ().addSprite ("grass__decal_4", 4);
		cells [4, 4].getRenderData ().addSprite ("grass__decal_4", 4);
		cells [8, 3].getRenderData ().addSprite ("grass__decal_5", 4);
		cells [6, 1].getRenderData ().addSprite ("grass__decal_5", 4);*/


		/*WorldSectorLevel sectorLevel = World.GetSectorLevel (10, 0, 10);
		WorldCell[,] cells = sectorLevel.getAllCells ();
		cells [0, 0].getRenderData ().addSprite ("grass__decal_5", 4);*/


		RenderingController.RenderSectorLevel (0, 0, 0);

		/*RenderingController.UnrenderSectorLevel (0, 0, 0);
		World.UnloadChunk (World.GetChunk (0, 0));
		RenderingController.RenderSectorLevel (0, 0, 0);*/




		//RenderingController.RenderSectorLevel (10, 0, 10);


		/*RenderingController.UnrenderSectorLevel (0, 0, 0);

		World.UnloadChunk (World.GetChunk (0, 0));
		
		RenderingController.RenderSectorLevel (0, 0, 0);*/


		//RenderingController.RenderSectorLevel (0, 1, 0);
		//RenderingController.RenderSectorLevel (1, 0, 1);
		//RenderingController.RenderSectorLevel (1, 0, 0);
		//RenderingController.UnrenderSectorLevel (0, 0, 0);
		//RenderingController.RenderSectorLevel (0, 0, 0);

		// end test

    }

}
