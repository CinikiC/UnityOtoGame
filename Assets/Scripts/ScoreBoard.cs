using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//显示 当前分数 练级数
//歌曲名字 与 歌手名
public class ScoreBoard : MonoBehaviour {

	//从 ScoreManager 读取分数
	ScoreManager score_manager;

	// 向 ScoreText 传分数 text
	Text scoretext;

	//上一次分数 , 也是ScoreBoard 上显示的分数
	private int preScore;


	// Use this for initialization
	void Start () {
		score_manager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

		scoretext = GameObject.Find("ScoreText").GetComponent<Text>();
		scoretext.text = "000000";

		preScore = 0;



	}
	
	// Update is called once per frame
	void Update () {

		// 如果分数有更新则 传 text 新值
		int curScore = score_manager.GetTotalScore();
		if(curScore != preScore){
			preScore = curScore;
			scoretext.text = PreZero(6 - preScore.ToString().Length) + preScore.ToString();
		}
	}
	
	string PreZero(int cntzero ){
		string strzero = "";
		if(cntzero < 0) return strzero;

		for(int i = 0;i<cntzero;++i){
			strzero += '0';
		}
		return strzero;
	}

}
