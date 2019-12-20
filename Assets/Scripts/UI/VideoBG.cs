using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI;

public class VideoBG : MonoBehaviour {

	public bool IsLoop = true;

	public MovieTexture movTexture=null;

	    // Use this for initialization

	    void Start () {

		        movTexture.loop = false;
		        movTexture.Play();
		        RawImage raw = GetComponent<RawImage> ();
		        raw.texture = movTexture;

		    }
	void update()
	{
	}

}
