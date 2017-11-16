using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//移動ベクトル表示画像の処理は別クラスに分けてもいいかも？



public class MyBall : BallBase {

	//プレファブとか
	public GameObject m_pMiniBall;		//分裂弾
	public GameObject m_pMoveVecImage;	//移動方向のUI
	public GameObject m_pMyBallShield;	//シールド
	public GameObject m_pPowerUp;		//パワーアップ
	public GameObject m_pThrough;		//貫通



	//リスト関連及び動的生成する可能性のあるもの
	List<GameObject> m_MiniBallList;	//分裂ボールリスト
	List<int> m_DeleteBallList;			//分裂ボール削除用のリスト
	List<GameObject> m_MoveVecImageList;

	GameObject m_MyBallShield;			//
	GameObject m_PowerUpObj;			//
	GameObject m_ThroughObj;			//

	//ボールの最大スペック
	const int m_MaxAtk = 7;
	const int m_MaxDivision = 10;	//最大分裂数
	const int m_MaxBomb = 3;		//最大爆発範囲

	//フラグ
	bool m_isAimFlg;
	bool m_isShotFlg;         //射撃フラグ


	//
	int m_Division = 0;			//分裂数
	int m_Bomb = 0;
	int m_NowDivision = 0;			//現在の分裂数
	int m_ThroughCnt = 0;			//貫通するターン数

	Vector3 m_DownPos;        //タップした座標
	Vector3 m_UpPos;          //タップ離した座標
	Vector3 m_MovePos;      //現在タップしている座標

	//============================================
	//	ゲッター
	//============================================
	public int GetDivision(){ return m_Division; }

	public int GetMiniBallListSize(){ return m_MiniBallList.Count; }



	void Start () {
		m_isAimFlg = false;
		m_isShotFlg = false;
		m_isThroughFlg = false;
		m_MoveVec.x = 0.0f;
		m_MoveVec.y = 0.0f;
		m_MoveVec.z = 0.0f;//使わない
		m_NowDivision = 0;
		m_MaxHp = 10;

		//インスタンス生成
		m_MiniBallList = new List<GameObject> ();
		m_DeleteBallList = new List<int> ();
		m_MoveVecImageList = new List<GameObject> ();

		m_MyBallShield = (GameObject)Instantiate (m_pMyBallShield,transform.position,Quaternion.identity);
		m_MyBallShield.transform.parent = this.transform;

		m_PowerUpObj = (GameObject)Instantiate (m_pPowerUp,transform.position,Quaternion.identity);
		m_PowerUpObj.transform.parent = this.transform;

		m_ThroughObj = (GameObject)Instantiate (m_pThrough,transform.position,Quaternion.identity);
		m_ThroughObj.transform.parent = this.transform;

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

		m_NowDivision = 0;
		m_NowCombo = 0;
		if (m_ThroughCnt > 0) 
		{
			m_ThroughCnt--;
			if (m_ThroughCnt == 0)m_isThroughFlg = false;
		}
			


		//ボール座標の再設定
		transform.position = new Vector3(nowPos.x, -3.81f, 0.0f);
	}

	
	// Update is called once per frame
	void Update () {
		{
			Shot();
		}

		//座標更新
		PosUpdate();

		//壁チェック
		WallCheck();

		//小玉チェック
		MiniBallCheckDelete();

		//バフチェック
		BuffCheck();






	}


	void Shot()
	{
		if (m_isShotFlg == true) return;

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
		}
		else
		{
			//タップをやめた。
			if (m_isAimFlg == false) return;
			if (m_isShotFlg == true) return;
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


	void MiniBallCheckDelete(){
		m_DeleteBallList.Clear ();

		for (int cnt = 0; cnt < m_MiniBallList.Count; cnt++)
		{
			if (m_MiniBallList [cnt] == null) { m_DeleteBallList.Add (cnt); }
			//スクリプトコンポーネント取得
			MiniBall miniCS = m_MiniBallList [cnt].GetComponent<MiniBall> ();
			if (miniCS == null) { m_DeleteBallList.Add (cnt);}
			if (miniCS.GetNowHp () <= 0) { m_DeleteBallList.Add (cnt); }
		}

		//末尾から順番に消していく(リストのずれを無くすため。)
		for(int cnt = m_DeleteBallList.Count;cnt > 0;cnt--){

			//インスタンスそのものを削除
			Destroy (m_MiniBallList[m_DeleteBallList[cnt - 1]]);

			//リスト削除
			m_MiniBallList.RemoveAt(m_DeleteBallList[cnt - 1]);
			Debug.Log ("不要ボール削除");
		}
	}


	void BuffCheck()
	{
		ShieldCheck ();

		PowerUpCheck ();

		ThroughCheck ();

	}


	void ShieldCheck()
	{
		//NULLチェック
		if(m_MyBallShield == null)return;
		MyBallShield MyShieldCS = m_MyBallShield.GetComponent<MyBallShield> ();
		if (MyShieldCS == null) { Debug.Log ("MyBallShieldNull");return; }

		//
		if (MyShieldCS.GetNowHp () > 0) {
			m_MyBallShield.SetActive (true);
		} else {
			m_MyBallShield.SetActive (false);
		}
	}


	void PowerUpCheck()
	{
		if (m_PowerUpObj == null)return;
		if (m_Atk >= 2) {
			m_PowerUpObj.SetActive (true);
		}
		else
		{
			m_PowerUpObj.SetActive (false);
		}

	}


	void ThroughCheck()
	{
		if (m_ThroughObj == null)return;
		if (m_isThroughFlg == true) 
		{
			m_ThroughObj.SetActive (true);
		}
		else
		{
			m_ThroughObj.SetActive (false);	
		}

	}
		
	//===========================================================================================================
	//何かしらのオブジェクトにぶつかった時　自機ボール専用部分
	//===========================================================================================================
	void OnCollisionEnter2D(Collision2D _Other){
		Debug.Log ("MyBall.OnCollision");
		//HitSEの再生
		//m_pHitSE.Play();

		BlockHit(_Other);

		EnemyHit(_Other);

		ItemHit (_Other);

		BarHit (_Other);


		//分裂
		Division(_Other);
	}


	protected override void EnemyHit(Collision2D _Other)
	{
		//NULLチェック
		if(m_MyBallShield == null){m_NowHp--;return;}
		MyBallShield MyShieldCS = m_MyBallShield.GetComponent<MyBallShield> ();
		if (MyShieldCS == null) { Debug.Log ("MyBallShieldNull");m_NowHp--;return; }


		if (MyShieldCS.GetNowHp () <= 0) {
			m_NowHp--;
		} 
		else 
		{
			MyShieldCS.Damage ();
		} 
	}


	void ItemHit(Collision2D _Other)
	{
		//=================================
		//アイテム取得 タグで識別
		//=================================
		if(_Other.gameObject.tag == "ItemPowerUp")
		{
			m_Atk = 2;
		}
		if (_Other.gameObject.tag == "ItemShield")
		{


			//シールド起動
			MyBallShield MyShieldCS = m_MyBallShield.GetComponent<MyBallShield> ();
			if (MyShieldCS == null) { Debug.Log ("MyBallShieldNull");return; }
			MyShieldCS.SetShield ();
			Debug.Log ("シールド生成");

		}
		if (_Other.gameObject.tag == "ItemDivision")
		{
			m_Division++;
		}
		if(_Other.gameObject.tag == "ItemScoreUp")
		{
			
		}
		if(_Other.gameObject.tag == "ItemBar")
		{
			Debug.Log ("バー生成");
			PlayerSupportManager.GetInstance.SetActiveBar ();
		}
		if(_Other.gameObject.tag == "ItemThrough")
		{
			if (m_isThroughFlg == true)return;
			m_isThroughFlg = true;
			m_ThroughCnt = 2;
			Debug.Log ("貫通弾");
		}






	}


	void Division(Collision2D _Other)
	{
		//ボール分裂処理
		if(_Other.gameObject.tag == "Block")
		{
			if (m_NowDivision < m_Division) 
			{
				//ミニボール生成
				GameObject miniBall = (GameObject)Instantiate(
					m_pMiniBall,
					transform.position,
					Quaternion.identity
				);

				miniBall.GetComponent<MiniBall> ().Init(m_Atk / 2 + 1,m_MoveSpeed,m_MoveVec * -1);




				m_MiniBallList.Add (miniBall);
				m_NowDivision++;


				Debug.Log ("ミニボール生成！！");
			}
		}
	}

}
