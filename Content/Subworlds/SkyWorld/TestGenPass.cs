using SkyMod.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.WorldBuilding;

namespace SkyMod.Content.Subworlds.SkyWorld {
    public class TestGenPass() : GenPass("Test", 1f) {
        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration) {
            progress.Message = Language.GetTextValue("idk");

            SpawnIsland(Main.maxTilesX / 2, Main.maxTilesY / 2, 3200, 150);
            /*SimplexNoise surfaceNoise = new SimplexNoise();
            SimplexNoise caveNoise = new SimplexNoise();
            for (int x = 0; x < Main.maxTilesX; x++) {
                float noiseValue = 0.0f;
                float freq1 = 0.001f;
                float amp1 = 100.0f;
                for (int i = 0; i < 16; i++) {
                    noiseValue += amp1 * surfaceNoise.Noise1D(freq1 * x);
                    freq1 *= 2;
                    amp1 /= 2;
                }

                float noiseStartY = 0.5f * Main.maxTilesY - 800;
                float noiseEndY = 0.5f * Main.maxTilesY + 1600;
                int grassLoc = (int)Math.Round(noiseValue + Main.maxTilesY / 2);
                for (int y = grassLoc; y < Main.maxTilesY; y++) {
                    float multiplier = (y - noiseStartY) / (noiseEndY - noiseStartY);
                    float value = 0.0f;
                    float freq2 = 1.0f;
                    float amp2 = 1.0f;
                    float maxAmp = 0.0f;
                    for (int i = 0; i < 8; i++) {
                        value += amp2 * (surfaceNoise.Noise2D(freq2 * 0.000875f * x, freq2 * 0.000625f * y) + 1) / 2;
                        maxAmp += amp2;
                        freq2 *= 2;
                        amp2 /= 2;
                    }
                    value /= maxAmp;
                    
                    if (value - 0.05f > multiplier) WorldGenUtil.PlaceTile(y == grassLoc ? TileID.HallowedGrass : TileID.Dirt, x, y);
                }
            }*/
        }

        private void SpawnIsland(int x, int y, int width, int height) {
            SimplexNoise surfaceNoise = new SimplexNoise();
            SimplexNoise undersideNoise = new SimplexNoise();

            int minX = x - (int)Math.Floor(0.5f * width);
            int maxX = x + (int)Math.Ceiling(0.5f * width);

            for (int currX = minX; currX <= maxX; currX++) {
                float xProg = (float)(currX - minX) / (maxX - minX); // 0 to 1 range of how far along the island we are
                float xMultiplier = Math.Abs(xProg - 0.5f) * 2; // convert to distance from center of the island in 0 to 1 range
                xMultiplier = (float)Math.Pow(xMultiplier, 1.6); // this pushes the x's towards the corners, making the center flat part of the island wider

                // Kyle Mcdonald's easing function (-20x^7 + 70x^6 - 84x^5 + 35x^4)
                xMultiplier = -20 * xMultiplier * xMultiplier * xMultiplier * xMultiplier * xMultiplier * xMultiplier * xMultiplier
                    + 70 * xMultiplier * xMultiplier * xMultiplier * xMultiplier * xMultiplier * xMultiplier
                    - 84 * xMultiplier * xMultiplier * xMultiplier * xMultiplier * xMultiplier
                    + 35 * xMultiplier * xMultiplier * xMultiplier * xMultiplier;
                // we need to invert it
                xMultiplier = 1.0f - xMultiplier;

                float surfaceHeight = 0.0f;
                float freq1 = 0.001f;
                float amp1 = 200.0f;
                for (int i = 0; i < 16; i++) {
                    surfaceHeight += amp1 * (surfaceNoise.Noise1D(freq1 * currX) + 1) / 2;
                    freq1 *= 2;
                    amp1 /= 2;
                }

                float bottomHeight = height;
                float freq2 = 0.002f / 3;
                float amp2 = 600.0f;
                for (int i = 0; i < 16; i++) {
                    bottomHeight += amp2 * (undersideNoise.Noise1D(freq2 * currX) + 1) / 2;
                    freq2 *= 2;
                    amp2 /= 2;
                }
                bottomHeight *= xMultiplier;

                int topY = (int)Math.Round(-surfaceHeight + y);
                int bottomY = (int)Math.Round(-surfaceHeight + bottomHeight + y);

                topY += 8;
                bottomY -= 8;

                if (topY <= bottomY) {
                    for (int currY = topY; currY <= bottomY; currY++) {
                        ushort tileType;
                        if (bottomY - topY < 50) {
                            tileType = TileID.HardenedSand;
                        } else if (currY == topY) {
                            tileType = TileID.HallowedGrass;
                        } else if (currY > topY + 5) {
                            tileType = TileID.Stone;
                        } else {
                            tileType = TileID.Dirt;
                        }
                        WorldGenUtil.PlaceTile(tileType, currX, currY);
                    }
                }
            }
        }
    }
}
