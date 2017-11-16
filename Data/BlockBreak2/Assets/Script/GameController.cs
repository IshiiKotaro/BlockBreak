using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*ゲーム全てを管理するクラス
 GameWorldみたいなもの*/


public class GameController : MonoBehaviour {

    public GameObject m_PlayerBall;


	MyBall m_Ball_CS;       //Myボールスクリプト




	// Use this for initialization
	void Start () {
        //コンポーネントの取得
		m_Ball_CS = m_PlayerBall.GetComponent<MyBall>();

	}
	
	// Update is called once per frame
	void Update () {



		NextWaveCheck ();

        //オブジェクト動的削除チェック
        ItemManager.GetInstance.CheckDelete();
        EnemyManager.GetInstance.CheckDelete();
        



        //Debug.Log("GameCon IListSize:" + ItemManager.GetInstance.GetListSize());


        //ゲームオーバーチェック



		
	}


	void NextWaveCheck()
	{
		bool NextOkFlg = false;
		if (m_Ball_CS.GetDivision () <= 0) {
			if (m_Ball_CS.GetNowHp () > 0)return;
		}
		else 
		{
			//ミニボールリストの中身とプレイヤーのHPが0になったら次のWAVEにする
			if(m_Ball_CS.GetMiniBallListSize() > 0)return;
			if(m_Ball_CS.GetNowHp () > 0)return;
		}



		//ブロック列生成チェック
		//if(NextOkFlg == true)
		{

			//ブロック列生成
			BlockManager.GetInstance.Create(1);
			//ボール再設置
			m_Ball_CS.Init();
			PlayerSupportManager.GetInstance.SetActiveBar ();
		}

	}


}
