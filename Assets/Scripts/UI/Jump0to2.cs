using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Jump0to2 : MonoBehaviour {

	UIFade uIFade;
	public void OnButtonClick()
	{
		uIFade.UI_FadeOut_Event();
		StartCoroutine(Delay());
	}
	// Use this for initialization
	void Start () {
		uIFade = GameObject.Find("Heading").GetComponent<UIFade>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	IEnumerator Delay()
	{
		yield return new WaitForSeconds(10);
		SceneManager.LoadSceneAsync(1);
	}
}
