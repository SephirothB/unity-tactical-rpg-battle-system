using UnityEngine;
using System.Collections.Generic;
using Tactical.Actor.Component;

namespace Tactical.Actor.Model {

	public class DamageAbilityEffectInfo {

		public Unit attacker;
		public Unit target;
		public List<ValueModifier> mods;

		public DamageAbilityEffectInfo (Unit attacker, Unit target, List<ValueModifier> mods) {
			this.attacker = attacker;
			this.target = target;
			this.mods = mods;
		}
	}

}
