using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperLaser : AssistBase {

	public int m_pDirType;

	int m_NowTime = 0;
	int m_ShotTime = 20;
	bool m_isFire;

	// Use this for initialization
	void Start () {
		if (m_pAssistObject != null) 
		{
			m_AssistObject = (GameObject)Instantiate(m_pAssistObject,transform.position,Quaternion.identity);
			m_AObjectPos = m_AssistObject.transform.position;
		}

		switch (m_pDirType) 
		{
		case 0:
			EffectManager.GetInstance.CreateEffect ((int)EffectType.LASER_R,transform.position,0.0f);
			break;
		case 1:
			EffectManager.GetInstance.CreateEffect ((int)EffectType.LASER_R,transform.position,270.0f);
			break;
		case 2:
			EffectManager.GetInstance.CreateEffect ((int)EffectType.LASER_R,transform.position,180.0f);
			break;
		case 3:
			EffectManager.GetInstance.CreateEffect ((int)EffectType.LASER_R,transform.position,90.0f);
			break;
		}


		EffectManager.GetInstance.CreateEffect ((int)EffectType.SKILL2,transform.position,0.0f);
		//SE
		SoundManager.GetInstance.PlaySE ((int)SEType.ASSIST,1.0f,1.0f);

		m_ShotTime = 20;
		m_isFire = false;
	}
	
	// Update is called once per frame
	void Update () {

		bool isEvent = EventManager.GetInstance.GetIsEvent ();
		if (isEvent == true)return;
		m_NowTime++;

		AssistObjectUpdate ();

		if (m_NowTime == m_ShotTime) 
		{
			//発射エフェクト
			EffectManager.GetInstance.CreateEffect((int)EffectType.SKILL1,transform.position,0.0f);
		}

		if(m_NowTime >= m_ShotTime)m_isFire = true;



		//デリート
		if(m_NowTime >= 30)
		{
			Destroy (m_AssistObject);
			Destroy (gameObject);
			Debug.Log ("ハイパーデリート");
		}
	}


	void OnTriggerEnter2D(Collider2D _Other)
	{
		//if (m_NowTime < 20)return;
		bool isEvent = EventManager.GetInstance.GetIsEvent ();
		if (isEvent == true)return;

		Debug.Log ("ハイパーレーザー！");
		if (_Other.gameObject.tag == "Block")
		{
			Block blockCS = _Other.GetComponent<Block> ();
			if (blockCS == null)return;
			blockCS.BlockDamage (10,true);
		}
	}
}
