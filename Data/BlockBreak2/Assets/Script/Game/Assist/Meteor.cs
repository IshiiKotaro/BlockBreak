using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : AssistBase {

	int m_NowTime = 0;
	float[] m_HitX;
	int[] m_HitTime;

	// Use this for initialization
	void Start () {
		
		if (m_pAssistObject != null) 
		{
			m_AssistObject = (GameObject)Instantiate(m_pAssistObject,transform.position,Quaternion.identity);
			m_AObjectPos = m_AssistObject.transform.position;
		}

		m_HitX = new float[3];
		m_HitTime = new int[3];

		for (int cnt = 0; cnt < 3; cnt++) 
		{
			m_HitX [cnt] = (float)(Random.Range (-20,20) * 0.1f);
			m_HitTime [cnt] = 120 + (cnt * 20);
		}


	}
	
	// Update is called once per frame
	void Update () {
		bool isEvent = EventManager.GetInstance.GetIsEvent ();
		if (isEvent == true)return;
		m_NowTime++;

		AssistObjectUpdate ();


		//爆発
		for(int cnt = 0;cnt < 3;cnt++)
		{
			if (m_NowTime == m_HitTime [cnt]) 
			{
				AssistManager.GetInstance.Create ((int)AssistType.BOMB,new Vector3(m_HitX[cnt],(float)(cnt * 2 - 2),0.0f));
			}
		}

		if (m_NowTime == m_HitTime [2] + 2) 
		{
			Destroy (m_AssistObject);
			Destroy (gameObject);
		}


	}
}
