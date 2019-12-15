using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI;

public class SelectMusicBG : MonoBehaviour {

	  

	public MovieTexture movTexture=null;

	    // Use this for initialization

	    void Start () {

		        movTexture.loop = true;//Loop Playback
		        movTexture.Play();
		        RawImage raw = GetComponent<RawImage> ();
		        raw.texture = movTexture;

		    }
	void update()
	{
	}

}
