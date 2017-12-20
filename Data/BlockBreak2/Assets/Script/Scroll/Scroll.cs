using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour {

	public float m_StartX;
	public float m_EndX;
	public float m_MoveSpeed;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//移動
		transform.position += new Vector3 (m_MoveSpeed,0,0);

		//元の座標に戻す
		if (transform.position.x >= m_EndX) 
		{
			transform.position = new Vector3 (m_StartX,0,0);
		}

	}
}
