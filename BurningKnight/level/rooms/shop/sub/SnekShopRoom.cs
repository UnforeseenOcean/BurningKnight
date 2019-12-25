using BurningKnight.entity.creature.npc.dungeon;
using Microsoft.Xna.Framework;

namespace BurningKnight.level.rooms.shop.sub {
	public class SnekShopRoom : SubShopRoom {
		public override void Paint(Level level) {
			Snek.Place(GetTileCenter() * 16 + new Vector2(8, 8), level.Area);
		}
	}
}