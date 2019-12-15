using System.Collections;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionBG : MonoBehaviour {



	public MovieTexture movTexture=null;

	// Use this for initialization

	void Start () {

		movTexture.Play();
		RawImage raw = GetComponent<RawImage> ();
		raw.texture = movTexture;
		StartCoroutine(Delay());

	}
	IEnumerator Delay()
	{
		yield return new WaitForSeconds(10);
		SceneManager.LoadSceneAsync(2);
	}
	void update()
	{
	}

}
