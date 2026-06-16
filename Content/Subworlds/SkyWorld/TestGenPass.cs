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

            SimplexNoise noise = new SimplexNoise();
            for (int x = 0; x < Main.maxTilesX; x++) {
                /*float amp = 1.0f;
                for (int i = 0; i < noiseMaps.Length; i++) {
                    noiseValue += noiseMaps[i].get(x) * amp;
                    amp /= 2;
                }
                noiseValue = noiseValue / 2 + 0.5f;*/
                float noiseValue = 0.0f;
                float freq = 0.0001f;
                float amp = 400.0f;
                for (int i = 0; i < 16; i++) {
                    noiseValue += amp * noise.snoise1(freq * x);
                    freq *= 2;
                    amp /= 2;
                }

                int grassLoc = (int)Math.Round(noiseValue + Main.maxTilesY / 2);
                WorldGenUtil.PlaceTile(TileID.HallowedGrass, x, grassLoc);
                for (int y = grassLoc + 1; y < Main.maxTilesY; y++) {
                    WorldGenUtil.PlaceTile(TileID.Dirt, x, y);
                }
            }
        }
    }
}
