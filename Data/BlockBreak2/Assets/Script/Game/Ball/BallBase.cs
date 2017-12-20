using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBase : MonoBehaviour {

	//Unity側で設定する変数
	//public AudioSource m_pHitSE;


	public float m_MoveSpeed;   //弾の移動速度

	//
	const float m_cSideWall = 2.6f;	//壁のX　-X 座標
	const float m_cTopBottomWall = 4.5f; //壁のY　－Y座標

	//
	protected Vector3 m_MoveVec;      		//移動方向
	protected int m_Atk = 1;              	//ボールの攻撃力
	protected int m_MaxHp = 1;				//最大HP
	protected int m_NowHp = 1;				//現在のHP

	protected bool m_isHit = false;
	protected bool m_isEvent = false;		//イベントフラグ


	//===========================================================
	//	ゲッター
	//===========================================================

	public int GetAtk() { return m_Atk;  }

	public int GetNowHp(){ return m_NowHp; }

	public Vector3 GetMoveVec(){ return m_MoveVec; }

	public bool GetIsEvent(){ return m_isEvent; }

	//===========================================================
	//	セッター
	//===========================================================

	public void SetMoveVec(Vector3 _Vec){ m_MoveVec = _Vec; }

	public void SetEvent(bool _Event){ m_isEvent = _Event; }

	//===========================================================
	//	メンバ関数
	//===========================================================

	// Use this for initialization
	void Start () {
		//Debug.Log ("ボール基底　Start()");



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
			m_MoveVec.y += 0.1f;
			m_MoveVec.Normalize ();
		}
	}


	//壁チェック　跳ね返ったらtrueを返す
	protected bool WallCheck()
	{

		Vector3 Pos = transform.position;
		bool isRef = false;

		if (Pos.x <= -m_cSideWall)
		{
			if (m_MoveVec.x < 0.0f) 
			{
				m_MoveVec.x = -m_MoveVec.x;
				isRef = true;
			}
		}
		else if (Pos.x >= m_cSideWall)
		{
			if (m_MoveVec.x >= 0.0f) 
			{
				m_MoveVec.x = -m_MoveVec.x;
				isRef = true;
			}
		}

		if (Pos.y >= m_cTopBottomWall)
		{
			if (m_MoveVec.y >= 0.0f) 
			{
				m_MoveVec.y = -m_MoveVec.y;
				isRef = true;
			}
		}
		else if (Pos.y <= -m_cTopBottomWall)
		{
			if (m_MoveVec.y < 0.0f) 
			{
				m_MoveVec.y = -m_MoveVec.y;
				isRef = true;
				m_NowHp = 0;
			}
		}


		if (isRef == true)return true;
		return false;
	}


	//==============================オブジェクト毎のあたり判定================================
	protected bool BlockHit(Collision2D _Other)
	{
		if (_Other.gameObject.tag != "Block")return false; 
		if (m_isHit == true)return false;

		m_isHit = true;


		//スコア加算
		ScoreManager.GetInstance.AddScore(200 * (ScoreManager.GetInstance.GetNowCombo() + 1));

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
		//EffectManager.GetInstance.CreateEffect (1, transform.position);


		return true;
	}


}
