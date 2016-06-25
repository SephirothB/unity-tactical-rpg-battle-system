using UnityEngine;

namespace Tactical.Core.Exceptions {

	public abstract class Modifier {

		public readonly int sortOrder;

		public Modifier (int sortOrder) {
			this.sortOrder = sortOrder;
		}

	}

}
