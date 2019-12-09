using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboBoard : MonoBehaviour {

//从 ScoreManager 读取combo数
	ScoreManager score_manager;

	// 向 ComboText 传combo数
	Text combotext;

	//上一次combo数 , 也是ComboBoard 上显示的连击数
	//连击数最多为 5位 十进制数
	private int preCombo;

	// Use this for initialization
	void Start () {
		score_manager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

		combotext = GameObject.Find("ComboText").GetComponent<Text>();
		combotext.text = "0";

		preCombo = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		// 如果combo数有更新则 传 text 新值
		int curCombo = score_manager.GetCombo();
		if(curCombo != preCombo){
			preCombo = curCombo;
			combotext.text = preCombo.ToString();
		}
	}
}
