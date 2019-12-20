using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressDegree : MonoBehaviour {
	// 向 PDText 传 进度数字
	Text pdtext;
	
	// 显示的进度数字,这是一个 0 - 1000 范围内的数字
	// 显示时转为 百分数 ， 用整形便于比较
	int preDegree;

	// 音频时间长度
	private float music_len;

	public bool IsOver(){
		return preDegree==1000;
	}
	public void Load(float len){
		music_len = len;
		pdtext = GameObject.Find("PDText").GetComponent<Text>();
		pdtext.text = "0.0";
	}
	public void UpdateText(float curtime){
		// 当前时间
		float cur = curtime;
		
		// 当前进度数字 如有改变 ,则重新显示。
		int curDegree = ((int)(cur/music_len * 1000f));
		if(curDegree > preDegree){
			preDegree = curDegree;
			int befDot = curDegree/10 , aftDot = curDegree%10;
			pdtext.text = befDot.ToString() + '.' + aftDot.ToString();
			Debug.Log(preDegree.ToString());
		}
	}
}
