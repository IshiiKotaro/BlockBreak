using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : AssistBase {

	int m_NowTime = 0;



	void Start () {
		//カメラ揺らし
		EventManager.GetInstance.CamQuake (0.3f,-0.2f);

		//エフェクト生成
		EffectManager.GetInstance.CreateEffect((int)EffectType.THUNDER,transform.position,0.0f);

		//SEを鳴らす
		SoundManager.GetInstance.PlaySE((int)SEType.THUNDER,1.0f,1.0f);
		SoundManager.GetInstance.PlaySE ((int)SEType.BOMB, 1.0f, 1.0f);


	}
	
	// Update is called once per frame
	void Update () {
		bool isEvent = EventManager.GetInstance.GetIsEvent ();
		if (isEvent == true)return;

		
		m_NowTime++;
		if (m_NowTime >= 5)
		{
			//ブロックだけ取得して、全ブロックに1ダメージを与える。
			Block[] blockObjects = GameObject.FindObjectsOfType<Block>();
			foreach (Block Idx in blockObjects)
			{
				Idx.BlockDamage(1,true);
			}

			Destroy(gameObject);
			Destroy (m_AssistObject);
		}
	}
}
