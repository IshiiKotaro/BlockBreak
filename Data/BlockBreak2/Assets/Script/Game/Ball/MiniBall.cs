using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBall : BallBase {


	int m_RefCnt = 0;
	//========================
	//	ゲッター
	//========================



	//



	public void Init(int _Atk,float _MoveSpeed,Vector3 _MoveVec){
		
		m_Atk = _Atk;
		m_Atk = 1;
		m_MoveSpeed = _MoveSpeed;
		m_MoveVec = _MoveVec;
		m_MoveVec.Normalize ();
	}


	void Update () {

		//イベント中か確認
		if (EventManager.GetInstance.GetIsEvent () == true)
		{
			m_isEvent = true;
		}
		else
		{
			m_isEvent = false;
		}
		if (m_isEvent == true)return;



		PosUpdate ();

		if (WallCheck () == true)m_RefCnt++;

	

		if (m_RefCnt >= 5) 
		{
			m_NowHp = 0;
		}
		if (m_NowHp <= 0)
		{
			Destroy (gameObject);
		}

	}

	//
	//何かしらのオブジェクトにぶつかった時
	void OnCollisionEnter2D(Collision2D _Other)
	{
		if (m_isEvent == true)return;
		if (BlockHit (_Other) == true)m_RefCnt++;
	}




}
