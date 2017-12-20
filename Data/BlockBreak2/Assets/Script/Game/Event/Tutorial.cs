using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

	public GameObject[] m_pTutorialMessagePrefab;
	GameObject m_Message1;
	GameObject m_Message2;

	bool m_isTutorialEnd = false;
	bool m_isTap = false;
	int m_MessageCnt = 0;

	//=============================
	//	ゲッター
	//=============================

	public bool GetIsTutorialEnd(){ return m_isTutorialEnd; }


	//
	public void Init()
	{
		if (m_Message2 == null) 
		{
			m_Message2 = (GameObject)Instantiate (m_pTutorialMessagePrefab[1],transform.position,Quaternion.identity);
			Debug.Log ("Message2Create");
		}
		if (m_Message1 == null) 
		{
			m_Message1 = (GameObject)Instantiate (m_pTutorialMessagePrefab[0],transform.position,Quaternion.identity);
			Debug.Log ("Message1Create");
		}


		SpriteManager.GetInstance.SetSpriteAlpha (m_Message1,1.0f);
		SpriteManager.GetInstance.SetSpriteAlpha (m_Message2,1.0f);

	
		m_isTutorialEnd = false;
		m_isTap = false;
		m_MessageCnt = 0;

		//プレイ回数が一定以上ならそもそもこのメッセージを出さない。

		Debug.Log ("Tutorial Init");
	}


	public void TutorialMessageUpdate ()
	{



		Debug.Log ("MessageCnt:" + m_MessageCnt);
		if (m_MessageCnt >= 2)return;
		if (Input.GetButton ("Fire1") == false)
		{
			m_isTap = false;
			return;
		}
		if (m_isTap == true)return;

		m_isTap = true;
		if(m_MessageCnt == 0)SpriteManager.GetInstance.SetSpriteAlpha (m_Message1,0.0f);
		if(m_MessageCnt == 1)SpriteManager.GetInstance.SetSpriteAlpha (m_Message2,0.0f);



		m_MessageCnt++;
		if (m_MessageCnt >= 2) 
		{
			


			m_isTutorialEnd = true;
			Debug.Log ("Tutorial End");
		}

	}
}
