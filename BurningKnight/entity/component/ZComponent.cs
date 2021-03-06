using System;
using BurningKnight.entity.buff;
using Lens.entity.component;
using Lens.util.math;

namespace BurningKnight.entity.component {
	public class ZComponent : Component {
		public float Z;
		public float ZVelocity;
		public bool Float;
		public float Gravity = 1;

		private float t;

		public override void Init() {
			base.Init();
			t = Rnd.Float(5f);
		}

		public override void Update(float dt) {
			base.Update(dt);

			if (Float) {
				t += dt;
				Z = 2.5f + (float) Math.Sin(t * 5f) * 1.5f;
				
				return;
			}
			
			Z += ZVelocity * dt * 20 * (Entity.TryGetComponent<BuffsComponent>(out var buffs) && buffs.Has<SlowBuff>() ? 0.5f : 1f);
			ZVelocity -= dt * 10 * Gravity;

			if (Z <= 0) {
				Z = 0;
				ZVelocity = 0;
			}
		}
	}
}