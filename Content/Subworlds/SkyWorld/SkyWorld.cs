using SubworldLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.WorldBuilding;

namespace SkyMod.Content.Subworlds.SkyWorld
{
    public class SkyWorld : Subworld
    {
        public override int Width => Main.maxTilesX * 2;

        public override int Height => Main.maxTilesY * 3;

        public override List<GenPass> Tasks => new List<GenPass>() {
            new TestGenPass()
        };
    }
}
