using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

	// 四个击打点的按下状态
	// 实时更新
	bool[] IsHit = new bool[4];
	
	// 屏幕宽度
	float width;
	// 三个屏幕宽度分界点,暂时只用三个分界点进行判定四个区域
	float[] hedge = new float[3];

	// 通过该接口 将 击打点 按下状态暴露给外界
	public void DoseHit(bool[] hitquery){
		for(int i = 0 ;i < 4;++i){
			hitquery[i] = IsHit[i];
			IsHit[i] = false;
		}
	}	

	// Use this for initialization
	void Start () {
		// 允许多点触屏
		// 初始化屏幕宽度 与 屏幕分界
		Input.multiTouchEnabled = true;	
		width = 1f*Screen.width;
		hedge[0] = 48f * width / 140f;
		hedge[1] = 70f * width / 140f;
		hedge[2] = 92f * width / 140f;

		for(int i = 0 ;i < 4;++i)
			IsHit[i] = false;
	}

	// Update is called once per frame
	// 通过Update实时更新 IsHit 的状态
	void Update () {
#if UNITY_ANDROID
		MobileInput();
#else
		DesktopInput();
#endif
	}

	//PC版本
	void DesktopInput(){
		IsHit[0] = Input.GetKey(KeyCode.D)?true:false;
		IsHit[1] = Input.GetKey(KeyCode.F)?true:false;
		IsHit[2] = Input.GetKey(KeyCode.J)?true:false;
		IsHit[3] = Input.GetKey(KeyCode.K)?true:false;
	}

	//移动版本
	void MobileInput(){
		if (Input.touchCount <= 0)
			return; 
		
		int touchcnt = Input.touchCount;
		for(int i = 0 ;i < touchcnt ; ++i){
			//只根据横坐标判定是否点击
			Touch touch = Input.touches[i];
			int flagind = CheckTouch(touch.position.x,width);
			if(flagind != -1){
				IsHit[flagind] = true;
			}
		}
	}

	// 根据点击横坐标判定区域 决定击打点
	int CheckTouch(float pos_x,float range){
		if(pos_x < hedge[0])
			return 0;
		else if( hedge[0] <=pos_x && pos_x < hedge[1])
			return 1;
		else if( hedge[1] <=pos_x && pos_x < hedge[2])
			return 2;
		else if( hedge[2] <=pos_x)
			return 3;
		return -1;
	}
}
