using System;

namespace BurningKnight.entity.creature.mob.boss.rooms {
	public static class DmRoomRegistry {
		public static Type[] Rooms = {
			typeof(DmEnemyRoom),
			typeof(DmMazeRoom),
			typeof(DmBulletDodgeRoom),
			typeof(DmPlatformRoom),
			typeof(DmPadsRoom),
			typeof(DmSpikeRoom),
		};
	}
}