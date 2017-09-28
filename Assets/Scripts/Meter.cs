using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Meter : MonoBehaviour {
	public Image bar;
	public Image bg;
	public float value = 0;
	public float MAX_VALUE = 100;
	private Vector2 max_size;
	// Use this for initialization
	void Start () {
		max_size = bg.rectTransform.sizeDelta;
	}
	
	// Update is called once per frame
	void Update () {
		float ratio = value / MAX_VALUE;
		bar.rectTransform.sizeDelta = new Vector2(ratio * max_size.x, max_size.y);
	}
}
