using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//移動ベクトル表示画像の処理は別クラスに分けてもいいかも？



public class MyBall : BallBase {

	//プレファブとか
	public GameObject m_pMoveVecImage;	//移動方向のUI
	public GameObject m_pAfterimageBall;			//残像用ボール


	//リスト関連及び動的生成する可能性のあるもの
	List<GameObject> m_MoveVecImageList;
	List<GameObject> m_AfterimageList;
	List<Vector3> m_BallPosList;

	//
	const int m_MaxAtk = 7;
	const float cSwapDis = 1.0f;
	bool m_isSwap = false;

	//フラグ
	bool m_isAimFlg;
	bool m_isShotFlg;         	//射撃フラグ


	//

	Vector3 m_DownPos;        //タップした座標
	Vector3 m_UpPos;          //タップ離した座標
	Vector3 m_MovePos;      //現在タップしている座標

	//
	float m_TrembleDis = 0.002f;	//震わせる距離


	//============================================
	//	ゲッター
	//============================================


	//============================================
	//	セッター
	//============================================




	void Start () {
		m_isAimFlg = false;
		m_isShotFlg = false;
		m_MoveVec.x = 0.0f;
		m_MoveVec.y = 0.0f;
		m_MoveVec.z = 0.0f;//使わない
		m_MaxHp = 10;

		//インスタンス生成
		m_MoveVecImageList = new List<GameObject> ();
		m_AfterimageList = new List<GameObject> ();
		m_BallPosList = new List<Vector3> ();
		//
		//m_MyBallShield.transform.parent = this.transform;


		//移動ベクトル表示画像のインスタンス生成
		for(int cnt = 0;cnt < 10;cnt++)
		{
			GameObject moveVec = (GameObject)Instantiate (
				m_pMoveVecImage,
				transform.position,
				Quaternion.identity
			);
			moveVec.transform.parent = transform;
			m_MoveVecImageList.Add (moveVec);
		}

		//残像生成
		for(int cnt = 0;cnt < 3;cnt++)
		{
			GameObject afterimage = (GameObject)Instantiate (
				m_pAfterimageBall,
				transform.position,
				Quaternion.identity
			);
			afterimage.transform.parent = transform;
			SpriteManager.GetInstance.SetSpriteAlpha (afterimage,(float)((3 - cnt)/3.0f));
			m_AfterimageList.Add (afterimage);


			Vector3 pos = new Vector3 ();
			m_BallPosList.Add (pos);
		}




	}


	public void Init()
	{
		//現在の座標を記憶
		Vector3 nowPos = transform.position;

		//パラメータ初期化
		m_isAimFlg = false;
		m_isShotFlg = false;
		m_MoveVec.x = 0.0f;
		m_MoveVec.y = 0.0f;
		m_MoveVec.z = 0.0f;//使わない

		m_NowHp = m_MaxHp;
		m_Atk = 1;

		ScoreManager.GetInstance.ResetNowCombo ();

		//ボール座標の再設定
		transform.position = new Vector3(nowPos.x, -3.81f, 0.0f);
	}

	
	// Update is called once per frame
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

		//残像の座標更新
		for (int cnt = m_BallPosList.Count - 1; cnt >= 1; cnt--) 
		{
			m_BallPosList [cnt] = m_BallPosList [cnt - 1];
			m_AfterimageList [cnt].transform.position = new Vector3 (m_BallPosList[cnt].x,m_BallPosList[cnt].y,m_BallPosList[cnt].z);
		}
		m_BallPosList [0] = transform.position;
		m_AfterimageList [0].transform.position = new Vector3 (m_BallPosList[0].x,m_BallPosList[0].y,m_BallPosList[0].z);


		Shot();


		//座標更新
		if (m_isShotFlg == true) 
		{
			PosUpdate();	
		}


		//壁チェック
		WallCheck();


		m_isHit = false;
		//マネージャーに次のWAVEに進めって教えてあげる
		if (m_NowHp <= 0)
		{
			BlockManager.GetInstance.SetisNextWave (true);
		}

	}


	void Shot()
	{
		if (m_isShotFlg == true) return;

		if (SwapCheck () > cSwapDis) 
		{
			m_isSwap = true;
		}
		else 
		{
			m_isSwap = false;
		}




		//タップした
		if (Input.GetButton("Fire1") == true)
		{
			if (m_isAimFlg == false)
			{
				//タップした座標を取得
				m_DownPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				//エイム画像を表示
				for(int cnt = 0;cnt < m_MoveVecImageList.Count;cnt++)
				{
					m_MoveVecImageList[cnt].SetActive (true);
				}


				m_isAimFlg = true;
			}
			//現在タップしている座標を記憶
			m_UpPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			//移動ベクトル表示画像の座標更新
			if(m_isSwap == true)
			{
				for(int cnt = 0;cnt < m_MoveVecImageList.Count;cnt++)
				{
					Vector3 movePos = new Vector3 (transform.position.x,transform.position.y,transform.position.z);
					Vector3 addVec = (m_DownPos - m_UpPos);
					addVec.Normalize ();
					addVec = addVec * (float)cnt;
					movePos += addVec;
					movePos.z = 0.0f;

					m_MoveVecImageList [cnt].transform.position = new Vector3(movePos.x,movePos.y,movePos.z);
				}
				//ボールを小刻みに震わせる。
				transform.position = new Vector3(transform.position.x + m_TrembleDis,transform.position.y,transform.position.z);
				m_TrembleDis *= -1.0f;

			}

		}
		else
		{
			//タップをやめた。
			if (m_isAimFlg == false) return;
			if (m_isShotFlg == true) return;
			//スワイプ出来ていたのか？
			if(m_isSwap == false)
			{
				m_isAimFlg = false;
				m_isShotFlg = false;
				return;
			}



			m_isShotFlg = true;

			//移動ベクトル画像を非表示に
			for(int cnt = 0;cnt < m_MoveVecImageList.Count;cnt++)
			{
				m_MoveVecImageList [cnt].SetActive (false);
			}


			//移動方向の決定
			Vector3 Vec;
			Vec = m_DownPos - m_UpPos;


			m_MoveVec.x = Vec.x;
			m_MoveVec.y = Vec.y;
			m_MoveVec.Normalize();


			//UpPos　DownPosが同じ座標だとバグる
			if(m_UpPos == m_DownPos)
			{
				m_MoveVec = Vector3.up;
			}
		}
	}
		

	float SwapCheck()
	{
		//スワイプと認識できる距離を離したのか。
		float XDis = (m_DownPos.x - m_UpPos.x)*(m_DownPos.x - m_UpPos.x);
		float YDis = (m_DownPos.y - m_UpPos.y)*(m_DownPos.y - m_UpPos.y);
		float RDis = XDis + YDis;
		RDis = Mathf.Sqrt (RDis);
		return RDis;
	}


		
	//===========================================================================================================
	//何かしらのオブジェクトにぶつかった時　自機ボール専用部分
	//===========================================================================================================
	void OnCollisionEnter2D(Collision2D _Other){
		if (m_isEvent == true)return;


		Debug.Log ("MyBall.OnCollision");
		//HitSEの再生
		//m_pHitSE.Play();

		BlockHit(_Other);

	}

}
