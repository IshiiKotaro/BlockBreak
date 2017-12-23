using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossLaser : AssistBase {


	int m_NowTime = 0;
	int m_ShotTime = 20;
	bool m_isFire = false;


	// Use this for initialization
	void Start () {
		EffectManager.GetInstance.CreateEffect ((int)EffectType.LASER_R,transform.position,0.0f);
		EffectManager.GetInstance.CreateEffect ((int)EffectType.LASER_R,transform.position,90.0f);
		EffectManager.GetInstance.CreateEffect ((int)EffectType.LASER_R,transform.position,180.0f);
		EffectManager.GetInstance.CreateEffect ((int)EffectType.LASER_R,transform.position,270.0f);

		SoundManager.GetInstance.PlaySE ((int)SEType.LASER,1.0f,1.0f);
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
		}
	}


	void OnTriggerEnter2D(Collider2D _Other)
	{
		//if (m_NowTime < 20)return;
		bool isEvent = EventManager.GetInstance.GetIsEvent ();
		if (isEvent == true)return;

		if (_Other.gameObject.tag == "Block")
		{
			Block blockCS = _Other.GetComponent<Block> ();
			if (blockCS == null)return;
			blockCS.BlockDamage (10,true);
		}
	}
}
