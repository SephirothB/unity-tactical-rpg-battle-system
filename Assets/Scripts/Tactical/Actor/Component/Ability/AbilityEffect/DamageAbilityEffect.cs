using UnityEngine;
using System.Collections.Generic;
using Tactical.Core.Enums;
using Tactical.Grid.Component;
using Tactical.Actor.Model;

namespace Tactical.Actor.Component {

	public class DamageAbilityEffect : BaseAbilityEffect {

		public const string GetAttackNotification = "DamageAbilityEffect.GetAttackNotification";
		public const string GetDefenseNotification = "DamageAbilityEffect.GetDefenseNotification";
		public const string GetPowerNotification = "DamageAbilityEffect.GetPowerNotification";
		public const string TweakDamageNotification = "DamageAbilityEffect.TweakDamageNotification";

		private const int minDamage = -999;
		private const int maxDamage = 999;

		public override int Predict (Tile target) {
			Unit attacker = GetComponentInParent<Unit>();
			Unit defender = target.content.GetComponent<Unit>();

			// Get the attackers base attack stat considering
			// mission items, support check, status check, and equipment, etc
			int attack = GetStat(attacker, defender, GetAttackNotification, 0);

			// Get the targets base defense stat considering
			// mission items, support check, status check, and equipment, etc
			int defense = GetStat(attacker, defender, GetDefenseNotification, 0);

			// Calculate base damage
			int damage = attack - (defense / 2);
			damage = Mathf.Max(damage, 1);

			// Get the abilities power stat considering possible variations
			int power = GetStat(attacker, defender, GetPowerNotification, 0);

			// Apply power bonus
			damage = power * damage / 100;
			// TODO: don't cap the damage ?
			damage = Mathf.Max(damage, 1);

			// Tweak the damage based on a variety of other checks like
			// Elemental damage, Critical Hits, Damage multipliers, etc.
			damage = GetStat(attacker, defender, TweakDamageNotification, damage);

			// Clamp the damage to a range
			damage = Mathf.Clamp(damage, minDamage, maxDamage);
			return damage;
		}

		public override void Apply (Tile target) {
			Unit defender = target.content.GetComponent<Unit>();

			// Start with the predicted damage value
			int value = Predict(target);

			// Add some random variance
			value *= Mathf.FloorToInt(Random.Range(0.9f, 1.1f));

			// Clamp the damage to a range
			value = Mathf.Clamp(value, minDamage, maxDamage);

			// Apply the damage to the target
			Stats s = defender.GetComponent<Stats>();
			s[StatType.HP] -= value;
		}

		private int GetStat (Unit attacker, Unit target, string notification, int startValue) {
			var mods = new List<ValueModifier>();
			var info = new DamageAbilityEffectInfo(attacker, target, mods);
			this.PostNotification(notification, info);
			mods.Sort();

			float value = startValue;
			for (int i = 0; i < mods.Count; ++i) {
				value = mods[i].Modify(startValue, value);
			}

			int retValue = Mathf.FloorToInt(value);
			retValue = Mathf.Clamp(retValue, minDamage, maxDamage);
			return retValue;
		}

	}

}
