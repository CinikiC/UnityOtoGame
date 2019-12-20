using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelItem : MonoBehaviour,IPointerClickHandler{
	
	// 关卡数据暂不封装，用文件名代替
	public string NoteTxtName;
	public string MusicName;

	public string PicName;

	public string GameBGname;

	public void OnPointerClick(PointerEventData eventData)
    {
		SelectionMsg.Instance().NoteTxtName =  NoteTxtName;
        SelectionMsg.Instance().MusicName =  MusicName;
        SelectionMsg.Instance().PicName = PicName;
		SelectionMsg.Instance().GameBGname = GameBGname;

        SceneManager.LoadScene("sel_game");
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
