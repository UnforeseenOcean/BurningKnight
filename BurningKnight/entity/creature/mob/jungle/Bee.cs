using System;
using BurningKnight.entity.buff;
using BurningKnight.entity.component;
using BurningKnight.entity.creature.player;
using Lens.entity;
using Lens.graphics;
using Microsoft.Xna.Framework;

namespace BurningKnight.entity.creature.mob.jungle {
	public class Bee : Mob {
		protected float Speed = 1f;
	
		private static readonly Color color = ColorUtils.FromHex("#5ac54f");
		
		protected override Color GetBloodColor() {
			return color;
		}
		
		protected virtual string GetAnimation() {
			return "bee";
		}

		protected override string GetHurtSfx() {
			return "mob_bee_damage";
		}

		protected override void SetStats() {
			base.SetStats();

			Height = 12;
			
			AddAnimation(GetAnimation());
			SetMaxHp(5);
			
			Become<IdleState>();
			Flying = true;

			GetComponent<MobAnimationComponent>().ShadowOffset = -2;
			AddBody();
			
			var body = GetComponent<RectBodyComponent>();
			
			body.Body.LinearDamping = 0.5f;
			body.Body.Restitution = 1;
			body.Body.Friction = 0;
			body.KnockbackModifier = 2.5f;
			
			Depth = Layers.FlyingMob;
		}

		protected virtual void AddBody() {
			AddComponent(new RectBodyComponent(2, 9, 12, 1));		
			AddComponent(new SensorBodyComponent(2, 3, 12, 8));
		}

		protected override void OnHit(Entity e) {
			base.OnHit(e);

			if (e is Player) {
				e.GetComponent<BuffsComponent>().Add(new SlowBuff {
					Duration = 5
				});
			}
		}

		#region Bee States
		public class IdleState : SmartState<Bee> {
			public override void Update(float dt) {
				base.Update(dt);

				if (Self.CanSeeTarget()) {
					Become<ChaseState>();
				}
			}
		}

		public class ChaseState : SmartState<Bee> {
			private Vector2 lastSeen;

			public override void Update(float dt) {
				base.Update(dt);

				if (Self.Target == null) {
					Become<IdleState>();

					return;
				}

				var see = Self.CanSeeTarget();

				if (see) {
					lastSeen = Self.Target.Center;
				} else if (Self.DistanceTo(lastSeen) <= 16) {
					Become<IdleState>();
					return;
				}

				var a = Self.AngleTo(lastSeen);
				var force = 50f * dt * Self.Speed;

				Self.GetComponent<RectBodyComponent>().Velocity +=
					new Vector2((float) Math.Cos(a) * force, (float) Math.Sin(a) * force);

				Self.PushFromOtherEnemies(dt * 0.2f);
			}
		}
		#endregion
	}
}