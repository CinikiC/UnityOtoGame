using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowGameEnd : MonoBehaviour {

	// public string testpicname;
	// Use this for initialization
	void Start () {
		RawImage raw = GameObject.Find("Pic").GetComponent<RawImage>();
		
		//仅测试图片效果
		// raw.texture = (Texture)Resources.Load(
		// 				"Textures/Pic/" + testpicname
		// 				,typeof(Texture));

		raw.texture = (Texture)Resources.Load(
						"Textures/Pic/" + SelectionMsg.Instance().PicName
						,typeof(Texture));
		//传递得分情况
		GameObject.Find("SongText").GetComponent<Text>().text = 
			SelectionMsg.Instance().MusicName;
		GameObject.Find("ScoreText").GetComponent<Text>().text = 
			SelectionMsg.Instance().score.ToString();
		GameObject.Find("PerfectText").GetComponent<Text>().text = 
			SelectionMsg.Instance().cntPerfect.ToString();
		GameObject.Find("GoodText").GetComponent<Text>().text = 
			SelectionMsg.Instance().cntGood.ToString();
		GameObject.Find("MissText").GetComponent<Text>().text = 
			SelectionMsg.Instance().cntMiss.ToString();


		//Load 得分等级 texture
		raw = GameObject.Find("ScoreDegree").GetComponent<RawImage>();
		if(SelectionMsg.Instance().scoreDegree == 0){
			raw.texture = (Texture)Resources.Load("Textures/S",typeof(Texture));
		}else if(SelectionMsg.Instance().scoreDegree == 1){
			raw.texture = (Texture)Resources.Load("Textures/A",typeof(Texture));
		}else{
			raw.texture = (Texture)Resources.Load("Textures/B",typeof(Texture));
		}

		//定义Back按钮
		GameObject.Find("BackButton").GetComponent<Button>
		().onClick.AddListener(()=>{ OnClickBack(); } );
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnClickBack(){
		SceneManager.LoadScene("end_sel");
	}
}
