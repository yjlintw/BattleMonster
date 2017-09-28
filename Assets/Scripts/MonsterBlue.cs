using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBlue : MonsterBase {
	public ParticleSystem ps1;
	public ParticleSystem ps2;
	public ParticleSystem ps3;
	ParticleSystem.EmissionModule em1;
	ParticleSystem.EmissionModule em2;
	ParticleSystem.EmissionModule em3;
	private Collider shieldCollider;

	
	public override void Start() {
		base.Start();
		em1 = ps1.emission;
		em2 = ps2.emission;
		em3 = ps3.emission;
		shieldCollider = ps2.gameObject.GetComponent<Collider>();
	}



	public override void Action1(bool is_on) {
		if (is_on) {
			var damage = 0.1f;
			if (is_evolved) {
				action_multiplier = 20;
				em3.enabled = true;
				damage = 1f;
			} else {
				action_multiplier = 10;
				em1.enabled = true;
			}
			RaycastHit hit;
			Debug.DrawRay(transform.position + transform.right * 1.5f, transform.right);
			if (Physics.Raycast(transform.position + transform.right * 1.5f , transform.right, out hit)){
				Debug.Log("hit: " + hit.collider.gameObject.name);
     // if something is hit, check whether it's tagged "zombie":
	 			if (hit.collider.gameObject.CompareTag("Monster")) {
					 hit.collider.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
				 }
			}
			// Debug.Log("Action1: " + is_on);
		} else {
			em1.enabled = false;
			em3.enabled = false;
		}
	}

	void ApplyDamage(float value) {
		Debug.Log("Apply Damage");
		hit_points -= value;
		if (hit_points < 0) {
			hit_points = 0;
		}
	}

	public override void Action2(bool is_on) {
		if (is_on) {
			action_multiplier = 3;
			em2.enabled = true;
			shieldCollider.enabled = true;
			// Debug.Log("Action2: " + is_on);
		} else {
			em2.enabled = false;
			shieldCollider.enabled = false;
		}
		
	}	
}

