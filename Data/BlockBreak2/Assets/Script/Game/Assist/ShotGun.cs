using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : AssistBase {

	public GameObject m_pMiniBall;


	Vector3 m_ShotVec;
	int m_NowTime = 0;
	int m_ShotCnt = 0;


	// Use this for initialization
	void Start () {
		EffectManager.GetInstance.CreateEffect ((int)EffectType.SKILL2,transform.position,0.0f);
		//SE
		SoundManager.GetInstance.PlaySE ((int)SEType.ASSIST,1.0f,1.0f);

		if (m_pAssistObject != null) 
		{
			m_AssistObject = (GameObject)Instantiate(m_pAssistObject,transform.position,Quaternion.identity);
			m_AObjectPos = m_AssistObject.transform.position;
		}

		//弾の移動ベクトルを計算
		m_ShotVec.x = 0;
		m_ShotVec.y = 1;
		int rand = Random.Range (0,8);
		m_ShotVec = Quaternion.Euler (0.0f,0.0f,(float)rand * 45.0f) * m_ShotVec;
		m_ShotVec.Normalize();
	}
	
	// Update is called once per frame
	void Update () {
		bool isEvent = EventManager.GetInstance.GetIsEvent ();
		if (isEvent == true)return;

		AssistObjectUpdate ();

		//弾発射処理
		m_NowTime++;
		if (m_NowTime < 90)return;
		if(m_NowTime % 5 == 0)
		{
			Vector3 vec = new Vector3();
			int rand = Random.Range (0,20);
			vec = Quaternion.Euler (0.0f, 0.0f, (float)rand) * m_ShotVec;

			GameObject miniBall1 = (GameObject)Instantiate(m_pMiniBall,transform.position,Quaternion.identity);
			miniBall1.GetComponent<MiniBall> ().Init(1,10,vec);

			//SEを鳴らす
			SoundManager.GetInstance.PlaySE((int)SEType.GUN1,1.0f,1.0f);

			m_ShotCnt++;
		}



		if(m_ShotCnt >= 6)
		{
			//自分自身を削除
			Destroy(m_AssistObject);
			Destroy(gameObject);
		}




	}
}
