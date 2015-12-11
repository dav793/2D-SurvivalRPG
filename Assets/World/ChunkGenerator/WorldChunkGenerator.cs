using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldChunkGenerator {

	public static WorldChunk GenerateChunk(int x, int z) {
		WorldChunk newChunk = new WorldChunk (x, z);
		Debug.Log ("Generating "+newChunk.indexToString());

		for (int x_ind = 0; x_ind < GameSettings.LoadedConfig.ChunkLength_Sectors; ++x_ind) {
			for (int z_ind = 0; z_ind < GameSettings.LoadedConfig.ChunkLength_Sectors; ++z_ind) {
				GenerateSector(newChunk.getSector(x_ind, z_ind));
			}
		}

		return newChunk;
	}

	static void GenerateSector(WorldSector sector) {
		for (int i = 0; i < sector.getAllLevels().Count; ++i) {
			GenerateSectorLevel(sector.getAllLevels()[i]);
		}
	}

	static void GenerateSectorLevel(WorldSectorLevel level) {
		if (level.y == 0) {
			// is default (ground) level
			GenerateTerrain (level);
		} 
		else if (level.y < 0) {
			// is underground level
			GenerateUndergroundTerrain (level);
		}
	}

	static void GenerateTerrain(WorldSectorLevel level) {
		GenerateTerrainBase (level);
		GenerateTerrainBorders (level);
	}

	static void GenerateUndergroundTerrain(WorldSectorLevel level) {
		GenerateUndergroundTerrainBase (level);
		GenerateUndergroundTerrainBorders (level);
	}

	static void GenerateTerrainBase(WorldSectorLevel level) {
		WorldCell[,] cells = level.getAllCells();
		for(int x_ind = 0; x_ind < GameSettings.LoadedConfig.SectorLength_Cells; ++x_ind) {
			for(int z_ind = 0; z_ind < GameSettings.LoadedConfig.SectorLength_Cells; ++z_ind) {
				GenerateTerrainCellBase (cells[x_ind, z_ind]);
			}
		}
		updateBordersOnSurroundingSectors (level);
	}

	static void GenerateTerrainCellBase(WorldCell cell) {
		int terrain_type = UnityEngine.Random.Range(0, 2);
		string sprite_index = "";
		int sprite_y = 0;
		
		switch(terrain_type) {
		case 0:
			sprite_index = "dirt_light_base_0";
			sprite_y = 0;
			break;
		case 1:
			sprite_index = "grass_short_base_0";
			sprite_y = 1;
			break;
		}

		cell.getRenderData ().removeBorderSprites ();
		cell.getRenderData().addSprite(sprite_index, sprite_y);
	}
	
	static void GenerateTerrainBorders(WorldSectorLevel level) {
		WorldCell[,] cells = level.getAllCells();
		for(int x_ind = 0; x_ind < GameSettings.LoadedConfig.SectorLength_Cells; ++x_ind) {
			for(int z_ind = 0; z_ind < GameSettings.LoadedConfig.SectorLength_Cells; ++z_ind) {
				GenerateTerrainCellBorders (cells[x_ind, z_ind]);
			}
		}
	}

	static void GenerateTerrainCellBorders(WorldCell cell) {

		// grass_short
		GenerateBiomeGroupBorders (cell, "grass", "short");

	}

	static void GenerateBiomeGroupBorders(WorldCell cell, string biome, string group) {
		if (!cell.getRenderData ().containsBiomeGroupBase (biome, group)) {
			// biome-group is not a base in this cell
			// check if any neighbor cells have biome-group base
			int y_index;
			
			if(CheckBorder0Conditions(cell, biome, group, out y_index)) 
				cell.getRenderData().addSprite(biome+"_"+group+"t_border_0", y_index);
			if(CheckBorder1Conditions(cell, biome, group, out y_index)) 
				cell.getRenderData().addSprite(biome+"_"+group+"_border_1", y_index);
			if(CheckBorder2Conditions(cell, biome, group, out y_index)) 
				cell.getRenderData().addSprite(biome+"_"+group+"_border_2", y_index);
			if(CheckBorder3Conditions(cell, biome, group, out y_index)) 
				cell.getRenderData().addSprite(biome+"_"+group+"_border_3", y_index);
			if (CheckBorder4Conditions(cell, biome, group, out y_index)) 
				cell.getRenderData().addSprite(biome+"_"+group+"_border_4", y_index);
			if(CheckBorder5Conditions(cell, biome, group, out y_index)) 
				cell.getRenderData().addSprite(biome+"_"+group+"_border_5", y_index);
			if(CheckBorder6Conditions(cell, biome, group, out y_index)) 
				cell.getRenderData().addSprite(biome+"_"+group+"_border_6", y_index);
			if(CheckBorder7Conditions(cell, biome, group, out y_index)) 
				cell.getRenderData().addSprite(biome+"_"+group+"_border_7", y_index);
			if(CheckBorder8Conditions(cell, biome, group, out y_index)) 
				cell.getRenderData().addSprite(biome+"_"+group+"_border_8", y_index);
			if(CheckBorder9Conditions(cell, biome, group, out y_index)) 
				cell.getRenderData().addSprite(biome+"_"+group+"_border_9", y_index);
			if(CheckBorder10Conditions(cell, biome, group, out y_index)) 
				cell.getRenderData().addSprite(biome+"_"+group+"_border_10", y_index);
			if(CheckBorder11Conditions(cell, biome, group, out y_index))
				cell.getRenderData().addSprite(biome+"_"+group+"_border_11", y_index);
		}
	}

	static void updateBordersOnSurroundingSectors(WorldSectorLevel level) {
	
		WorldSector sector;

		sector = level.getParent ().getNeighborSector (NeighborDirections.N);
		if (sector != null) {
			List<WorldCell> border = sector.levels.getLevel(level.y).getBorderCells(NeighborDirections.S);
			for(int i = 0; i < border.Count; ++i)
				GenerateTerrainCellBorders(border[i]);
		}

		sector = level.getParent ().getNeighborSector (NeighborDirections.NE);
		if (sector != null) {
			List<WorldCell> border = sector.levels.getLevel(level.y).getBorderCells(NeighborDirections.SW);
			for(int i = 0; i < border.Count; ++i)
				GenerateTerrainCellBorders(border[i]);
		}

		sector = level.getParent ().getNeighborSector (NeighborDirections.E);
		if (sector != null) {
			List<WorldCell> border = sector.levels.getLevel(level.y).getBorderCells(NeighborDirections.W);
			for(int i = 0; i < border.Count; ++i)
				GenerateTerrainCellBorders(border[i]);
		}

		sector = level.getParent ().getNeighborSector (NeighborDirections.SE);
		if (sector != null) {
			List<WorldCell> border = sector.levels.getLevel(level.y).getBorderCells(NeighborDirections.NW);
			for(int i = 0; i < border.Count; ++i)
				GenerateTerrainCellBorders(border[i]);
		}

		sector = level.getParent ().getNeighborSector (NeighborDirections.S);
		if (sector != null) {
			List<WorldCell> border = sector.levels.getLevel(level.y).getBorderCells(NeighborDirections.N);
			for(int i = 0; i < border.Count; ++i)
				GenerateTerrainCellBorders(border[i]);
		}

		sector = level.getParent ().getNeighborSector (NeighborDirections.SW);
		if (sector != null) {
			List<WorldCell> border = sector.levels.getLevel(level.y).getBorderCells(NeighborDirections.NE);
			for(int i = 0; i < border.Count; ++i)
				GenerateTerrainCellBorders(border[i]);
		}

		sector = level.getParent ().getNeighborSector (NeighborDirections.W);
		if (sector != null) {
			List<WorldCell> border = sector.levels.getLevel(level.y).getBorderCells(NeighborDirections.E);
			for(int i = 0; i < border.Count; ++i)
				GenerateTerrainCellBorders(border[i]);
		}

		sector = level.getParent ().getNeighborSector (NeighborDirections.NW);
		if (sector != null) {
			List<WorldCell> border = sector.levels.getLevel(level.y).getBorderCells(NeighborDirections.SE);
			for(int i = 0; i < border.Count; ++i)
				GenerateTerrainCellBorders(border[i]);
		}

	}

	static bool CheckBorder0Conditions(WorldCell cell, string biome, string group, out int y_index) {

		/*	
		 *   	  |   |
		 *     ___|___|___
		 *        | b | x
		 *     ___|___|___
		 *        | x | o
		 *        |   |
		 */

		if(
			cell.getNeighborCell(NeighborDirections.SE) != null && 
			cell.getNeighborCell(NeighborDirections.SE).getRenderData().containsBiomeGroupBase(biome, group, out y_index) &&
			!cell.getNeighborCell(NeighborDirections.S).getRenderData().containsBiomeGroupBase(biome, group) &&
			!cell.getNeighborCell(NeighborDirections.E).getRenderData().containsBiomeGroupBase(biome, group)
		)
			return true;
		return false;
	}

	static bool CheckBorder1Conditions(WorldCell cell, string biome, string group, out int y_index) {
		
		/*	
		 *   	  |   |
		 *     ___|___|___
		 *      x | b | 
		 *     ___|___|___
		 *      o | x | 
		 *        |   |
		 */
		
		if(
			cell.getNeighborCell(NeighborDirections.SW) != null && 
			cell.getNeighborCell(NeighborDirections.SW).getRenderData().containsBiomeGroupBase(biome, group, out y_index) &&
			!cell.getNeighborCell(NeighborDirections.S).getRenderData().containsBiomeGroupBase(biome, group) &&
			!cell.getNeighborCell(NeighborDirections.W).getRenderData().containsBiomeGroupBase(biome, group)
		)
			return true;
		return false;
	}

	static bool CheckBorder2Conditions(WorldCell cell, string biome, string group, out int y_index) {
		
		/*	
		 *   	o | x |
		 *     ___|___|___
		 *      x | b | 
		 *     ___|___|___
		 *        |   | 
		 *        |   |
		 */
		
		if(
			cell.getNeighborCell(NeighborDirections.NW) != null && 
			cell.getNeighborCell(NeighborDirections.NW).getRenderData().containsBiomeGroupBase(biome, group, out y_index) &&
			!cell.getNeighborCell(NeighborDirections.N).getRenderData().containsBiomeGroupBase(biome, group) &&
			!cell.getNeighborCell(NeighborDirections.W).getRenderData().containsBiomeGroupBase(biome, group)
		) 
			return true;
		return false;
	}

	static bool CheckBorder3Conditions(WorldCell cell, string biome, string group, out int y_index) {
		
		/*	
		 *   	  | x | o
		 *     ___|___|___
		 *        | b | x
		 *     ___|___|___
		 *        |   | 
		 *        |   |
		 */
		
		if(
			cell.getNeighborCell(NeighborDirections.NE) != null && 
			cell.getNeighborCell(NeighborDirections.NE).getRenderData().containsBiomeGroupBase(biome, group, out y_index) &&
			!cell.getNeighborCell(NeighborDirections.N).getRenderData().containsBiomeGroupBase(biome, group) &&
			!cell.getNeighborCell(NeighborDirections.E).getRenderData().containsBiomeGroupBase(biome, group)
		)
			return true;
		return false;
	}

	static bool CheckBorder4Conditions(WorldCell cell, string biome, string group, out int y_index) {
		
		/*	
		 *   	  |   | 
		 *     ___|___|___
		 *        | b | o
		 *     ___|___|___
		 *        |   | 
		 *        |   |
		 */

		if(
			cell.getNeighborCell(NeighborDirections.E) != null && 
			cell.getNeighborCell(NeighborDirections.E).getRenderData().containsBiomeGroupBase(biome, group, out y_index)
		)
			return true;
		return false;
	}

	static bool CheckBorder5Conditions(WorldCell cell, string biome, string group, out int y_index) {
		
		/*	
		 *   	  |   | 
		 *     ___|___|___
		 *        | b | 
		 *     ___|___|___
		 *        |   | 
		 *        | o |
		 */
		
		if(
			cell.getNeighborCell(NeighborDirections.S) != null && 
			cell.getNeighborCell(NeighborDirections.S).getRenderData().containsBiomeGroupBase(biome, group, out y_index)
		)
			return true;
		return false;
	}

	static bool CheckBorder6Conditions(WorldCell cell, string biome, string group, out int y_index) {
		
		/*	
		 *   	  |   | 
		 *     ___|___|___
		 *      o | b | 
		 *     ___|___|___
		 *        |   | 
		 *        |   |
		 */
		
		if(
			cell.getNeighborCell(NeighborDirections.W) != null && 
			cell.getNeighborCell(NeighborDirections.W).getRenderData().containsBiomeGroupBase(biome, group, out y_index)
		)
			return true;
		return false;
	}

	static bool CheckBorder7Conditions(WorldCell cell, string biome, string group, out int y_index) {
		
		/*	
		 *   	  | o | 
		 *     ___|___|___
		 *        | b | 
		 *     ___|___|___
		 *        |   | 
		 *        |   |
		 */
		
		if(
			cell.getNeighborCell(NeighborDirections.N) != null && 
			cell.getNeighborCell(NeighborDirections.N).getRenderData().containsBiomeGroupBase(biome, group, out y_index)
		)
			return true;
		return false;
	}

	static bool CheckBorder8Conditions(WorldCell cell, string biome, string group, out int y_index) {
		
		/*	
		 *   	  |   | 
		 *     ___|___|___
		 *        | b | o
		 *     ___|___|___
		 *        |   | 
		 *        | o |
		 */
		
		if(
			cell.getNeighborCell(NeighborDirections.E) != null && 
			cell.getNeighborCell(NeighborDirections.S) != null && 
			cell.getNeighborCell(NeighborDirections.E).getRenderData().containsBiomeGroupBase(biome, group, out y_index) &&
			cell.getNeighborCell(NeighborDirections.S).getRenderData().containsBiomeGroupBase(biome, group, out y_index)
		)
			return true;
		return false;
	}

	static bool CheckBorder9Conditions(WorldCell cell, string biome, string group, out int y_index) {
		
		/*	
		 *   	  |   | 
		 *     ___|___|___
		 *      o | b | 
		 *     ___|___|___
		 *        |   | 
		 *        | o |
		 */
		
		if(
			cell.getNeighborCell(NeighborDirections.W) != null && 
			cell.getNeighborCell(NeighborDirections.S) != null && 
			cell.getNeighborCell(NeighborDirections.W).getRenderData().containsBiomeGroupBase(biome, group, out y_index) &&
			cell.getNeighborCell(NeighborDirections.S).getRenderData().containsBiomeGroupBase(biome, group, out y_index)
		)
			return true;
		return false;
	}

	static bool CheckBorder10Conditions(WorldCell cell, string biome, string group, out int y_index) {
		
		/*	
		 *   	  | o | 
		 *     ___|___|___
		 *      o | b | 
		 *     ___|___|___
		 *        |   | 
		 *        |  |
		 */
		
		if(
			cell.getNeighborCell(NeighborDirections.W) != null && 
			cell.getNeighborCell(NeighborDirections.N) != null && 
			cell.getNeighborCell(NeighborDirections.W).getRenderData().containsBiomeGroupBase(biome, group, out y_index) &&
			cell.getNeighborCell(NeighborDirections.N).getRenderData().containsBiomeGroupBase(biome, group, out y_index)
		)
			return true;
		return false;
	}

	static bool CheckBorder11Conditions(WorldCell cell, string biome, string group, out int y_index) {
		
		/*	
		 *   	  | o | 
		 *     ___|___|___
		 *        | b | o
		 *     ___|___|___
		 *        |   | 
		 *        |   |
		 */
		
		if(
			cell.getNeighborCell(NeighborDirections.E) != null && 
			cell.getNeighborCell(NeighborDirections.N) != null && 
			cell.getNeighborCell(NeighborDirections.E).getRenderData().containsBiomeGroupBase(biome, group, out y_index) &&
			cell.getNeighborCell(NeighborDirections.N).getRenderData().containsBiomeGroupBase(biome, group, out y_index)
		)
			return true;
		return false;
	}

	static void GenerateUndergroundTerrainBase(WorldSectorLevel level) {
		/*WorldCell[,] cells = level.getAllCells();
		for(int x_ind = 0; x_ind < GameSettings.LoadedConfig.SectorLength_Cells; ++x_ind) {
			for(int z_ind = 0; z_ind < GameSettings.LoadedConfig.SectorLength_Cells; ++z_ind) {

			}
		}*/
	}

	static void GenerateUndergroundTerrainBorders(WorldSectorLevel level) {
		/*WorldCell[,] cells = level.getAllCells();
		for(int x_ind = 0; x_ind < GameSettings.LoadedConfig.SectorLength_Cells; ++x_ind) {
			for(int z_ind = 0; z_ind < GameSettings.LoadedConfig.SectorLength_Cells; ++z_ind) {

			}
		}*/
	}

}
