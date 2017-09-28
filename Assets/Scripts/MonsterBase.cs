using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBase : MarkerGO {
	public bool in_slot = false;
	public float act_points = 0;
	public float ACT_POINTS_MAX = 100;

	public float hit_points = 100;
	public float HIT_POINTS_MAX = 100;

	public Meter hitpointsbar;
	public Meter actbar;

	protected bool using_action = false;
	protected int action_multiplier = 1;
	private int current_action_num = 0;

	public bool is_evolved = false;
	public bool collaborate = false;


	// Use this for initialization
	public override void Start () {
		base.Start();	
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
		// checkAround();
		is_evolved = firebaseHub.getEvolved(id);
		if (in_slot) {
			act_points += (Time.deltaTime * 10);
		} else {
			act_points -= (Time.deltaTime * 10 * 0.2f * action_multiplier );
		}
		if (act_points < 0) {
			act_points = 0;
		} else if (act_points > ACT_POINTS_MAX) {
			act_points = ACT_POINTS_MAX;
		}
		actbar.MAX_VALUE = ACT_POINTS_MAX;
		actbar.value = act_points;
		hitpointsbar.MAX_VALUE = HIT_POINTS_MAX;
		hitpointsbar.value = hit_points;
		if (using_action && !in_slot) {
			// Debug.Log("Using Action: " + using_action);
			switch(current_action_num) {
				case 0:
				Action1(act_points > 0);
				Action2(false);
				break;
				case 1:
				Action1(false);
				Action2(act_points > 0);
				break;
			}
		} else {
			Action1(false);
			Action2(false);
		}
	}

	// public void checkAround() {
	// 	RaycastHit hit;

    //     Vector3 p1 = transform.position;
    //     float distanceToObstacle = 0;

    //     // Cast a sphere wrapping character controller 10 meters forward
    //     // to see if it is about to hit anything.
    //     if (Physics.SphereCast(p1, 2, transform.forward, out hit, 10))
    //     {
	// 		if (hit.collider.gameObject.tag == "Monster") {
   	// 	        distanceToObstacle = hit.distance;
	// 			Debug.Log(hit.collider.gameObject.name);
	// 		}
    //     }
	// }

	public virtual void Action1(bool is_on) {
		action_multiplier = 10;
	}


	public virtual void Action2(bool is_on) {
		action_multiplier = 3;
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "MonsterSlot"){
			Debug.Log("enter slot");
			in_slot = true;
		} else if (other.tag == "ActionSlot1") {

		} else if (other.tag == "ActionSlot2") {

		}
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "MonsterSlot") {
			using_action = false;
		} else if (other.tag == "ActionSlot1") {
			Debug.Log("in ActionSlot 1");
			using_action = true;	
			in_slot = false;
			current_action_num = 0;
		} else if (other.tag == "ActionSlot2") {
			Debug.Log("in ActionSlot 2");
			using_action = true;
			in_slot = false;
			current_action_num = 1;
		}

		if (other.tag == "Monster") {
			MarkerGO otherGO = other.gameObject.GetComponent<MarkerGO>();
			MarkerGO thisGO = gameObject.GetComponent<MarkerGO>();
			if (otherGO.id != thisGO.id) {
				collaborate = true;
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "MonsterSlot"){
			Debug.Log("leave slot");
			in_slot = false;
		} else if (other.tag == "ActionSlot1") {
			using_action = false;
			action_multiplier = 1;
		} else if (other.tag == "ActionSlot2") {
			using_action = false;
			action_multiplier = 1;
		}

		if (other.tag == "Monster") {
			collaborate = false;
		}
	}

	void OnCollisionEnter(Collision col) {
		hit_points -= 0.1f;
		if (hit_points < 0) {
			hit_points = 0;
		}
	}
	// void OnParticleCollision(GameObject other) {
    //     // Rigidbody body = other.GetComponent<Rigidbody>();
	// 	// Debug.Log("on particle collide");
    //     // if (body) {
    //     //     Vector3 direction = other.transform.position - transform.position;
    //     //     direction = direction.normalized;
    //     //     body.AddForce(direction * 5);
    //     // }
	// 	hit_points -= 0.1f;
	// 	if (hit_points < 0) {
	// 		hit_points = 0;
	// 	}
    // }

	// void OnTriggerStay(Collider other) {
	// 	if (other.tag == "MonsterSlot") {
	// 		Debug.Log("in slot");
	// 	}
	// }
}
