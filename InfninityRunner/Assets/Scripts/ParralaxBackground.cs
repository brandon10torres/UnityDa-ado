using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxBackground : MonoBehaviour {

	public Renderer[] capas;
	public float[] speed;
	public bool scrollActivated = true;
	private float offset = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (scrollActivated) 
		{
			for (int i = 0; i < capas.Length; i++) 
			{
				if (speed[i] != 0) 
				{
					offset = Time.time;
					float offsetElement = offset * speed[i];
					capas[i].material.mainTextureOffset = new Vector2 (offsetElement, 0);
				}
			}
		}
	}
}
