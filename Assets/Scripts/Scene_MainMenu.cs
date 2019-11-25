using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene_MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject.Find("StartButton").GetComponent<Button>
		().onClick.AddListener(()=>{ OnClickStart(); } );
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// 开始游戏(切换到 MainWeb Scene
	void OnClickStart(){
		SceneManager.LoadScene("MainWeb");
	}
}
