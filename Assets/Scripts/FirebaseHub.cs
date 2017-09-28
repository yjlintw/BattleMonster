using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FirebaseHub: MonoBehaviour {
	private Dictionary<int, bool> evolvedMap = new Dictionary<int, bool>();
	private Dictionary<string, int> tokenIdMap = new Dictionary<string, int> {
        {"1", 9},
        {"2", 8},
        {"3", 7}
    };
	public TrackerHub trackerhub;	
	void Start() {

		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://magictable-f840c.firebaseio.com/");
		FirebaseApp.DefaultInstance.SetEditorP12FileName("MagicTable-d3a92bbd38bc.p12");

		FirebaseApp.DefaultInstance.SetEditorServiceAccountEmail("magictable-f840c@appspot.gserviceaccount.com");

		// Set this before calling into the realtime database.
		DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
		var refe = FirebaseDatabase.DefaultInstance.GetReference("monsters");
		refe.ValueChanged += HandleValueChanged;
	}

	void HandleValueChanged(object sender, ValueChangedEventArgs args) {
		if (args.DatabaseError != null) {
			Debug.LogError(args.DatabaseError.Message);
			return;
		}
		// Do something with the data in args.Snapshot
		DataSnapshot snapshot = args.Snapshot;
		foreach(DataSnapshot s in snapshot.Children) {
			Debug.Log(s.Child("name"));
			string monsterId = (string)s.Child("monsterId").GetValue(true);
			double timeTogether = (double)s.Child("timeTogether").GetValue(true);
			Debug.Log(monsterId + ":" + timeTogether);
			evolvedMap[tokenIdMap[monsterId]] = timeTogether > 60;
			// trackerhub.setValue(tokenIdMap[monsterId], timeTogether > 60);
		}
	}

	public bool getEvolved(int id) {
		if (evolvedMap.ContainsKey(id)) {
			return evolvedMap[id];
		}
		return false;	
	}
}
