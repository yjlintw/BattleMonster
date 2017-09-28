using UnityEngine;
public class MarkerGO : MonoBehaviour {

	// [SerializeField]
	// private bool _enabled;
	// public bool isEnabled {
	// 	private set {
	// 		_enabled = value;
	// 	} 
	// 	get {
	// 		return _enabled;
	// 	}
	// }
	protected TrackerHub trackerHub;
	protected FirebaseHub firebaseHub;
	public int id = -1;
	[SerializeField]
	private bool _highlight;
	public bool highlight {
		private set {
			_highlight = value;
		}
		get {
			return _highlight;
		}
	}
	public Marker marker {
		set; get;
	}

	public void setHighlight() {
		highlight = true;
	}

	public void unsetHighlight() {
		highlight = false;
	}

	public virtual void Start() {
		trackerHub = GameObject.FindWithTag("TrackerHub").GetComponent<TrackerHub>();
		firebaseHub = GameObject.FindWithTag("TrackerHub").GetComponent<FirebaseHub>();
	}

	public virtual void Update() {
	}

	// public void setActive(bool value) {
	// 	isEnabled = value;
	// }
}
