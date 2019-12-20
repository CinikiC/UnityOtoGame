using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
	//总得分数
	private int TotalScore =0;

	//连击数
	private int combo = 0;

	//各种击中的数目，用以结算
	private int cntPerfectHit = 0 , cntGoodHit = 0,cntMissHit = 0;
	//判定 得分 的时间差，先置为 private const 试一试
	private const float Good_Tolerance = 0.2f , Perfect_Tolerance = 0.1f;
	
	//增加分数 的 梯度，置为 public 供外部调试
	public int Good_Score , Perfect_Score;
	

	public int GetTotalScore(){
		return TotalScore;
	}

	public int GetCombo(){
		return combo;
	}
	
	public int GetCntPerfect(){
		return cntPerfectHit;
	}

	public int GetCntGood(){
		return cntGoodHit;
	}

	public int GetCntMiss(){
		return cntMissHit;
	}
	public bool DoesGetHit(float timedif){
		int getscore = CheckScore(timedif);
		if(getscore < 0){
			combo = 0;
			cntMissHit ++;
			return false;
		}
		else if(getscore == Good_Score){
			TotalScore += getscore;
			cntGoodHit ++;
			combo ++;
			return true;
		}else if(getscore == Perfect_Score){
			TotalScore += getscore;
			cntPerfectHit ++;
			combo ++;
			return true;
		}else{
			return false;
		}
	}		

	//根据时间差，返回打击得分 ， -1f代表miss
	int CheckScore(float timedif){
		timedif = Mathf.Abs(timedif);

		if(timedif <= Perfect_Tolerance)
			return Perfect_Score;
		else if(Perfect_Tolerance < timedif  && timedif <= Good_Tolerance)
			return Good_Score;
		else
			return -1;
	}
}
