using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 暂时被 Music_Note中的 Update部分代替，属于弃用的状态。
public class Spwan_point : MonoBehaviour {

	// public float speed;
	// Use this for initialization
	void Start () {
		Instantiate(Resources.Load("mk_GP_left"));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Awake() {
		// GameObject testInt = Instantiate(Resources.Load("mk_GP_left"),
		// 					new Vector3(0,-10,30),Quaternion.identity) as GameObject;	
	}
}
