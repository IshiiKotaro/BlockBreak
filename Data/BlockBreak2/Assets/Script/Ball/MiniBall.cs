using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBall : BallBase {


	//========================
	//	ゲッター
	//========================



	//


	public void Init(int _Atk,float _MoveSpeed,Vector3 _MoveVec){
		m_Atk = _Atk;
		m_Atk = 1;
		m_MoveSpeed = _MoveSpeed;
		m_MoveVec = _MoveVec;
	}


	void Update () {
		PosUpdate ();

		WallCheck();

	}

	//
	//何かしらのオブジェクトにぶつかった時
	void OnCollisionEnter2D(Collision2D _Other)
	{

		BlockHit (_Other);
			
		EnemyHit (_Other);

		BarHit (_Other);

	}




}
