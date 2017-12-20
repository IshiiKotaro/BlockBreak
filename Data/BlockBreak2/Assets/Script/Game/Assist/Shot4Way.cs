using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot4Way : AssistBase
{
	public GameObject m_pMiniBall;

	const int cMaxShotCnt = 2;
	int m_ShotCnt = 0;
	int m_NowTime = 0;


	void Start()
	{
		EffectManager.GetInstance.CreateEffect ((int)EffectType.SKILL2,transform.position,0.0f);
		//SE
		SoundManager.GetInstance.PlaySE ((int)SEType.ASSIST,1.0f,1.0f);

		if (m_pAssistObject != null) 
		{
			m_AssistObject = (GameObject)Instantiate(m_pAssistObject,transform.position,Quaternion.identity);
			m_AObjectPos = m_AssistObject.transform.position;
		}
	}



	void Update ()
	{

		bool isEvent = EventManager.GetInstance.GetIsEvent ();
		if (isEvent == true)return;
		AssistObjectUpdate ();

		m_NowTime++;
		if (m_NowTime % 14 == 0) 
		{
			//4方向にminiballをばら撒く
			//GameObject miniBall1 = (GameObject)Instantiate(m_pMiniBall,transform.position,Quaternion.identity);
			//miniBall1.GetComponent<MiniBall> ().Init(1,10,Vector3.up);
			//GameObject miniBall2 = (GameObject)Instantiate(m_pMiniBall,transform.position,Quaternion.identity);
			//miniBall2.GetComponent<MiniBall> ().Init(1,10,Vector3.down);
			//GameObject miniBall3 = (GameObject)Instantiate(m_pMiniBall,transform.position,Quaternion.identity);
			//miniBall3.GetComponent<MiniBall> ().Init(1,10,Vector3.left);
			//GameObject miniBall4 = (GameObject)Instantiate(m_pMiniBall,transform.position,Quaternion.identity);
			//miniBall4.GetComponent<MiniBall> ().Init(1,10,Vector3.right);
			m_ShotCnt++;

			//SEを鳴らす
			SoundManager.GetInstance.PlaySE((int)SEType.GUN1,1.0f,1.0f);
		}

		if(m_ShotCnt >= cMaxShotCnt)
		{
			Destroy (m_AssistObject);
			Destroy (gameObject);
		}
	}
}
