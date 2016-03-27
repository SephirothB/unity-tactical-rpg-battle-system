using UnityEngine;

namespace Tactical {

	public class Loader : MonoBehaviour {
		public GameObject gameManager; // GameManager prefab to instantiate.

		private void Awake () {
			// Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
			if (GameManager.instance == null) {
				// Instantiate gameManager prefab
				Instantiate(gameManager);
			}
		}
	}
}
