using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : AssistBase {

	int m_NowTime = 0;

	// Use this for initialization
	void Start () {
		EffectManager.GetInstance.CreateEffect (2,transform.position,0.0f);

		EventManager.GetInstance.CamQuake (1.0f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		bool isEvent = EventManager.GetInstance.GetIsEvent ();
		if (isEvent == true)return;

		m_NowTime++;
		//オブジェクトデリート判定
		if(m_NowTime >= 10)Destroy(gameObject);

	}


	void OnTriggerEnter2D(Collider2D _Other)
	{
		bool isEvent = EventManager.GetInstance.GetIsEvent ();
		if (isEvent == true)return;

		if (_Other.gameObject.tag == "Block") 
		{
			Block blockCS = _Other.GetComponent<Block> ();
			if (blockCS == null)return;
			blockCS.BlockDamage (10,false);

			SoundManager.GetInstance.PlaySE ((int)SEType.BOMB,1.0f,1.0f);



		}
	}


}
