using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCharge : MonsterBase {
	public ParticleSystem ps5;
	ParticleSystem.EmissionModule em5;

	public override void Start() {
		base.Start();
		em5 = ps5.emission;
	}
	// Update is called once per frame
	public override void Update () {
		base.Update();
		if (collaborate) {
			em5.enabled = true;
		} else {
			em5.enabled = false;
		}
	}
}
