using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour {

	UIFade uIFade;
	// Use this for initialization
	void Start () {
		uIFade = GameObject.Find("Heading").GetComponent<UIFade>();
		uIFade.UI_FadeIn_Event();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
