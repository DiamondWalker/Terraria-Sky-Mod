using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SkyMod.Util {
    public class WorldGenUtil {
        /*public static bool TileAt(int x, int y) {
            return WorldGen.InWorld(x, y) && Main.tile[x, y] != null && Main.tile[x, y].active();
        }

        public static bool WallAt(int x, int y) {
            return WorldGen.InWorld(x, y) && Main.tile[x, y] != null && Main.tile[x, y].wall != WallID.None;
        }*/

        public static void PlaceTile(int type, int x, int y) {
            if (!WorldGen.InWorld(x, y)) return;
            Tile tile = Main.tile[x, y];
            tile.ResetToType((ushort)type);
            //WorldGen.PlaceTile(x, y, type, false, true);
            /*if (!WorldGen.InWorld(x, y)) return;
            if (Main.tile[x, y] == null) Main.tile[x, y] = new Tile();
            Main.tile[x, y].type = (ushort)type;
            SetSlope(TileSlope.Full, x, y);
            Main.tile[x, y].active(true);*/
        }

        /*public static void PlaceTile<T>(int x, int y) where T : ModTile {
            PlaceTile(ModContent.TileType<T>(), x, y);
        }

        public static void PlaceWall(int type, int x, int y) {
            if (!WorldGen.InWorld(x, y)) return;
            if (Main.tile[x, y] == null) Main.tile[x, y] = new Tile();
            Main.tile[x, y].wall = (ushort)type;
        }

        public static void PlaceWall<T>(int x, int y) where T : ModWall {
            PlaceWall(ModContent.WallType<T>(), x, y);
        }

        public static void SetSlope(TileSlope slope, int x, int y) {
            if (!TileAt(x, y)) return;

            if (slope == TileSlope.Half) {
                Main.tile[x, y].slope(0);
                Main.tile[x, y].halfBrick(true);
            } else {
                Main.tile[x, y].slope((byte)slope);
                Main.tile[x, y].halfBrick(false);
            }
        }

        public static void Actuate(int x, int y) {
            if (!TileAt(x, y)) return;

            Main.tile[x, y].inActive(true);
        }

        public static void PlaceFurniture(int type, int x, int y) {
            if (!WorldGen.InWorld(x, y)) return;
            WorldGen.PlaceObject(x, y, type);
            NetMessage.SendObjectPlacment(-1, x, y, type, 0, 0, -1, -1);
        }

        public static void PlaceFurniture<T>(int x, int y) where T : ModTile {
            PlaceFurniture(ModContent.TileType<T>(), x, y);
        }

        public static void PlaceChest(int type, int x, int y, IChestLootComponent lootTable) {
            int chestID = WorldGen.PlaceChest(x, y, (ushort)type, false, 0);
            if (chestID >= 0) {
                Chest chest = Main.chest[chestID];

                var list = lootTable.Provide();
                for (int i = 0; i < list.Count; i++) {
                    chest.item[i].SetDefaults(list[i].type);
                    chest.item[i].stack = list[i].stackSize;
                }
            }

            NetMessage.SendObjectPlacment(-1, x, y, (ushort)type, 0, 0, -1, -1);
        }

        public static void PlaceChest<T>(int x, int y, IChestLootComponent lootTable) where T : ModTile {
            PlaceChest(ModContent.TileType<T>(), x, y, lootTable);
        }

        public static void DeleteTile(int x, int y) {
            if (!WorldGen.InWorld(x, y) || Main.tile[x, y] == null) return;
            Main.tile[x, y].type = 0;
            Main.tile[x, y].active(false);
        }

        public static void PlaceCircle(int type, int x, int y, int radius) {
            for (int x2 = x - radius; x2 <= x + radius; x2++) {
                for (int y2 = y - radius; y2 <= y + radius; y2++) {
                    int distX = x2 - x;
                    int distY = y2 - y;
                    if (Math.Sqrt(distX * distX + distY * distY) <= radius) {
                        PlaceTile(type, x2, y2);
                    }
                }
            }
        }

        public static void PlaceCircle<T>(int x, int y, int radius) where T : ModTile {
            PlaceCircle(ModContent.TileType<T>(), x, y, radius);
        }

        public static bool IsTileOfType(int type, int x, int y) {
            return TileAt(x, y) && Main.tile[x, y].type == type;
        }

        public static bool IsTileOfType<T>(int x, int y) where T : ModTile {
            return IsTileOfType(ModContent.TileType<T>(), x, y);
        }

        public static bool IsWallOfType(int type, int x, int y) {
            return TileAt(x, y) && Main.tile[x, y].wall == type;
        }

        public static bool IsWallOfType<T>(int x, int y) where T : ModWall {
            return IsWallOfType(ModContent.WallType<T>(), x, y);
        }*/
    }
}
