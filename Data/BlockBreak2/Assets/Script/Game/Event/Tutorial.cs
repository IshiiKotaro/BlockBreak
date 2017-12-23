using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

	public GameObject[] m_pTutorialMessagePrefab;
	GameObject m_Message1;
	GameObject m_Message2;
	GameObject m_Message3;

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
		if (m_Message3 == null) 
		{
			m_Message3 = (GameObject)Instantiate (m_pTutorialMessagePrefab[2],transform.position,Quaternion.identity);
		}
		if (m_Message2 == null) 
		{
			m_Message2 = (GameObject)Instantiate (m_pTutorialMessagePrefab[1],transform.position,Quaternion.identity);
		}
		if (m_Message1 == null) 
		{
			m_Message1 = (GameObject)Instantiate (m_pTutorialMessagePrefab[0],transform.position,Quaternion.identity);
		}
	



		SpriteManager.GetInstance.SetSpriteAlpha (m_Message1,1.0f);
		SpriteManager.GetInstance.SetSpriteAlpha (m_Message2,1.0f);
		SpriteManager.GetInstance.SetSpriteAlpha (m_Message3,1.0f);
	
		m_isTutorialEnd = false;
		m_isTap = false;
		m_MessageCnt = 0;


	}


	public void TutorialMessageUpdate ()
	{

	

		if (m_MessageCnt >= 3)return;
		EventManager.GetInstance.SetIsEvent (true);
		if (Input.GetButton ("Fire1") == false)
		{
			m_isTap = false;
			return;
		}
		if (m_isTap == true)return;

		m_isTap = true;
		if(m_MessageCnt == 0)SpriteManager.GetInstance.SetSpriteAlpha (m_Message1,0.0f);
		if(m_MessageCnt == 1)SpriteManager.GetInstance.SetSpriteAlpha (m_Message2,0.0f);
		if(m_MessageCnt == 2)SpriteManager.GetInstance.SetSpriteAlpha (m_Message3,0.0f);


		m_MessageCnt++;
		if (m_MessageCnt >= 3) 
		{
			
			EventManager.GetInstance.SetIsEvent (false);

			m_isTutorialEnd = true;
			Debug.Log ("Tutorial End");
		}

	}
}
