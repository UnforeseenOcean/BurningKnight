﻿using System.Collections.Generic;
using BurningKnight.level.rooms;
using Lens.entity.component.logic;

namespace BurningKnight.entity.door {
	public class IronLock : Lock {
		private List<Room> rooms = new List<Room>();

		public void CalcRooms() {
			rooms.Clear();
			
			foreach (var room in Area.Tags[Tags.Room]) {
				if (room.Overlaps(this)) {
					rooms.Add((Room) room);
				}
			}
		}

		protected override bool Disposable() {
			return false;
		}

		protected override bool Interactable() {
			return false;
		}

		private bool updatedRooms;

		public override void Update(float dt) {
			base.Update(dt);

			if (!updatedRooms || rooms.Count == 0) {
				CalcRooms();
			}
			
			var shouldLock = false;

			foreach (var r in rooms) {
				if (r.Type != RoomType.Connection && r.Tagged[Tags.Player].Count > 0 && r.Tagged[Tags.MustBeKilled].Count > 0) {
					shouldLock = true;
					break;
				}
			}

			if (shouldLock && !IsLocked) {
				SetLocked(true, null);
				GetComponent<StateComponent>().Become<ClosingState>();
			} else if (!shouldLock && IsLocked) {
				SetLocked(false, null);
				GetComponent<StateComponent>().Become<OpeningState>();
			}
		}
	}
}