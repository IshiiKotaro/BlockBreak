using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToStart : MonoBehaviour {

	SpriteRenderer m_TapToStart;
	float m_Ang = 0.0f;

	// Use this for initialization
	void Start () {
		m_TapToStart = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (m_TapToStart == null) 
		{
			Debug.Log ("スタートぬる");
			return;
		}

		//サインカーブで文字を明暗させる
		m_Ang += 2.0f;
		if (m_Ang > 360.0f)m_Ang -= 360.0f;

		float rad = m_Ang * Mathf.PI / 180.0f;

		float alpha = Mathf.Sin (rad);//-1~1
		alpha = (alpha + 1.0f) * 0.2f;//0~0.4
		alpha += 0.6f;//0.6~1.0

		//明暗
		SpriteManager.GetInstance.SetSpriteAlpha(m_TapToStart,alpha);

	}
}
