using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMove : MonoBehaviour {

	// 此速度与 Music_Note 部分 产生Note的 提前时延 有关
	// 28（距离） = test_speed * 时延
	public float test_speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		// Vector3
		// 191112准备定义速度，初步计算是 z 轴负方向 7  = (30 - 28)/4
		GetComponent<Rigidbody>().velocity = new Vector3(0,0,-test_speed);
		// transform.Translate(0,0,-1);
	}
}
