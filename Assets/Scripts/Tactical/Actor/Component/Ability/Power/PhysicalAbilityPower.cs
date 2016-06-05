using UnityEngine;
using Tactical.Core.Enums;

namespace Tactical.Actor.Component {

	public class PhysicalAbilityPower : BaseAbilityPower {

		public int level;

		protected override int GetBaseAttack () {
			return GetComponentInParent<Stats>()[StatType.ATK];
		}

		protected override int GetBaseDefense (Unit target) {
			return target.GetComponent<Stats>()[StatType.DEF];
		}

		protected override int GetPower () {
			return level;
		}

	}

}
