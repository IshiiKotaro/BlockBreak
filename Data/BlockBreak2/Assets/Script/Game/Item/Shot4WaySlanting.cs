using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shot4WaySlanting : ItemBase {

	public GameObject m_pMiniBall;


	void Update()
	{
		Recast ();
	}


	void OnTriggerEnter2D(Collider2D _Other)
	{
		if (_Other.gameObject.tag != "Ball")return;
		if (GetisPlayItem () == false)return;
		MyBall ballCS = _Other.GetComponent<MyBall> ();
		if (ballCS == null)return;

		if (m_NowRecast > 0)return;

		//
		EffectManager.GetInstance.CreateEffect ((int)EffectType.SKILL1,transform.position,0.0f);
		//SE
		SoundManager.GetInstance.PlaySE ((int)SEType.ASSIST,1.0f,1.0f);



		Vector3 moveVec = new Vector3();
		//for (int Cnt = 0; Cnt < 3; Cnt++) 
		{
			moveVec.x = 1;
			moveVec.y = 1;
			GameObject miniBall1 = (GameObject)Instantiate(m_pMiniBall,transform.position,Quaternion.identity);
			miniBall1.GetComponent<MiniBall> ().Init(1,10,moveVec);


			moveVec.x = 1;
			moveVec.y = -1;
			GameObject miniBall2 = (GameObject)Instantiate(m_pMiniBall,transform.position,Quaternion.identity);
			miniBall2.GetComponent<MiniBall> ().Init(1,10,moveVec);


			moveVec.x = -1;
			moveVec.y = 1;
			GameObject miniBall3 = (GameObject)Instantiate(m_pMiniBall,transform.position,Quaternion.identity);
			miniBall3.GetComponent<MiniBall> ().Init(1,10,moveVec);


			moveVec.x = -1;
			moveVec.y = -1;
			GameObject miniBall4 = (GameObject)Instantiate(m_pMiniBall,transform.position,Quaternion.identity);
			miniBall4.GetComponent<MiniBall> ().Init(1,10,moveVec);
		}
		SetRecast ();
	}




}
