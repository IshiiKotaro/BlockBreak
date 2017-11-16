using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBase : MonoBehaviour {

	//Unity側で設定する変数
	//public AudioSource m_pHitSE;


	public float m_MoveSpeed;   //弾の移動速度


	protected static int m_NowCombo = 0;

	//
	const float m_cSideWall = 3.0f;	//壁のX　-X 座標
	const float m_cTopBottomWall = 4.5f; //壁のY　－Y座標

	//
	protected Vector3 m_MoveVec;      		//移動方向
	protected int m_Atk = 1;              	//ボールの攻撃力
	protected int m_MaxHp = 1;				//最大HP
	protected int m_NowHp = 1;				//現在のHP

	protected bool m_isThroughFlg = false;	  //貫通フラグ




	//===========================================================
	//	ゲッター
	//===========================================================

	public int GetAtk() { return m_Atk;  }

	public int GetNowHp(){ return m_NowHp; }


	//===========================================================
	//	セッター
	//===========================================================


	//===========================================================
	//	メンバ関数
	//===========================================================

	// Use this for initialization
	void Start () {
		Debug.Log ("ボール基底　Start()");



	}




	protected void PosUpdate()
	{
		if (m_NowHp <= 0)return;
		transform.Translate(m_MoveVec * Time.deltaTime * m_MoveSpeed);

		//ボールが真横に飛ばないようにする

		float AngDot = Vector3.Dot(m_MoveVec,Vector3.up);
		if(AngDot >= -0.15f && AngDot <= 0.15f)
		{
			m_MoveVec.y *= 1.2f;
			m_MoveVec.Normalize ();
		}
	}


	protected void WallCheck()
	{

		Vector3 Pos = transform.position;

		if (Pos.x <= -m_cSideWall)
		{
			if (m_MoveVec.x >= 0.0f) return;
			m_MoveVec.x = -m_MoveVec.x;
		}
		else if (Pos.x >= m_cSideWall)
		{
			if (m_MoveVec.x < 0.0f) return;
			m_MoveVec.x = -m_MoveVec.x;
		}

		if (Pos.y >= m_cTopBottomWall)
		{
			if (m_MoveVec.y < 0.0f) return;
			m_MoveVec.y = -m_MoveVec.y;
		}
		else if (Pos.y <= -m_cTopBottomWall)
		{
			if (m_MoveVec.y >= 0.0f) return;
			m_MoveVec.y = -m_MoveVec.y;
			m_NowHp = 0;

		}
	}


	//==============================オブジェクト毎のあたり判定================================
	protected void BlockHit(Collision2D _Other)
	{
		if (_Other.gameObject.tag != "Block")return;
		m_NowCombo ++;
		Debug.Log ("コンボ"+m_NowCombo);
		if(m_isThroughFlg == true)return;

		//跳ね返り処理
		//自身の座標と、接触座標を取得
		Vector3 HitPos = new Vector3();
		foreach (ContactPoint2D OtherPos in _Other.contacts)
		{
			HitPos.x = OtherPos.point.x;
			HitPos.y = OtherPos.point.y;
			HitPos.z = 0.0f;
		}
		Vector3 MyPos = transform.position;

		//ブロックの法線を求める。
		Vector3 Normal = MyPos - HitPos;

		m_MoveVec.Normalize();
		Normal.Normalize();

		//反射角を計算
		m_MoveVec = Vector3.Reflect(m_MoveVec, Normal);

		m_MoveVec.Normalize();

		//エフェクト生成
		EffectManager.GetInstance.CreateEffect (1, transform.position);



	}


	protected virtual void EnemyHit(Collision2D _Other)
	{
		if (_Other.gameObject.tag != "EnemyAtk")return;
		m_NowHp--;
	}

	protected void BarHit(Collision2D _Other)
	{
		if (_Other.gameObject.tag != "Bar")return;


		//移動ベクトルが下向きの時だけチェックする。
		if(m_MoveVec.y > 0)return;

		//移動方向反転
		m_MoveVec.y *= -1;

		Debug.Log ("バーとヒット");

	}


}
