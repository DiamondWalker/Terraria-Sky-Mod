using SkyMod.Content.Subworlds.SkyWorld;
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SkyMod.Content.Items
{
    // This is a basic item template.
    // Please see tModLoader's ExampleMod for every other example:
    // https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
    public class Teleporter : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.SkyMod.hjson' file.
		public override void SetDefaults()
		{
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.HoldUp;
		}

        public override bool? UseItem(Player player) {
			if (SubworldSystem.IsActive<SkyWorld>()) {
				SubworldSystem.Exit();
			} else {
				SubworldSystem.Enter<SkyWorld>();
			}
            return true;
        }
    }
}
