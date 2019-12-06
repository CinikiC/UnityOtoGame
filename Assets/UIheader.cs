/*using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UIHeader:MonoBehaviour	
{
	public Text head;
	public string str="Cytus";
	str
}*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIheader : MonoBehaviour 
{
	Image fillImage;
	bool isStartTime = false;
	float startTime = 0;
	float curTime;

	void Start () {
		fillImage = GameObject.Find("UICanvas/UI_Image/background").GetComponent<Image>();
		fillImage.fillAmount = 1;
	}

	void Update ()
	{
		curTime = Time.time;
		if (isStartTime && curTime-startTime<10)
		{
			fillImage.fillAmount = (startTime + 10 - curTime) / 10;
		}

	}


	public void TheClick()
	{
		isStartTime = true;
		startTime = Time.time;
		print("STart");
	}
}