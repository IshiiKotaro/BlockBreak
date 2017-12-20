using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*ゲーム全てを管理するクラス
 GameWorldみたいなもの*/


public class GameController : MonoBehaviour{

    public GameObject m_PlayerBall;
	public Text m_pScoreLabel;
	public Text m_pAddScoreLabel;
	public Text m_pNowWave;

	MyBall m_Ball_CS;       		//Myボールスクリプト



	//
	float m_NowTime;		//タイムアタック用

	bool m_isNextWaveLarge;	//大群モードOnかどうか
	int   m_CreateCnt;		//大群モード用　1度にどれくらいの列を作るか


	int m_FinishWave;


	bool m_isSceneUpdate;	//シーン更新をしてもいいか
	bool m_isResult;		//リザルトに行ってもいいのか



	//
	public void Awake()
	{

	}


	public void Start () 
	{
        //コンポーネントの取得
		m_Ball_CS = m_PlayerBall.GetComponent<MyBall>();

		//マネージャ初期化
		BlockManager.GetInstance.Init ();
		EventManager.GetInstance.Init ();
		ScoreManager.GetInstance.Init ();
		ItemManager.GetInstance.Init ();

		FadeManager.GetInstance.FadeInInit ();

		m_isSceneUpdate = false;

		//
		m_isNextWaveLarge = false;



		//BGM再生
		SoundManager.GetInstance.PlayBGM((int)BGMType.STAGEBGM1);
	}




	
	// Update is called once per frame
	void Update () 
	{
		//シーン更新チェック
		if (m_isSceneUpdate == false) 
		{
			if (FadeManager.GetInstance.GetIsFadeEnd () == false)return;
			FadeManager.GetInstance.SetFadeEndReset ();
			m_isSceneUpdate = true;
		}


		if (m_isSceneUpdate == false)return;
		//次のウェーブへ
		if (BlockManager.GetInstance.GetisNextWave () == true)
		{
			bool isCreate = true;
			bool isAddWave = true;
			if (PlayerPrefs.GetString ("GameMode") != "EndressMode" && ScoreManager.GetInstance.GetWave() >= 100)
			{
				isCreate = false;
				isAddWave = false;
			}

			//大群モード以外の時
			if (PlayerPrefs.GetString ("GameMode") != "LargeCrowdMode") 
			{
				BlockManager.GetInstance.NextWave (false, isAddWave, isCreate,-1);
				m_Ball_CS.Init ();
			} 
			else 
			{
				m_isNextWaveLarge = true;
			}





		}

		//クイックモード専用処理
		if (PlayerPrefs.GetString ("GameMode") == "QuickMode")
		{
			m_NowTime += (1.0f * Time.deltaTime);
			if (m_NowTime >= 5)
			{
				if(BlockManager.GetInstance.NextWave(false,true,true,-1) == true)m_NowTime = 0;
			}
		}

		//大群モード専用処理
		if (m_isNextWaveLarge == true) 
		{
			if (m_CreateCnt < 3)
			{
				if (BlockManager.GetInstance.NextWave (true, true, true,-1) == true) 
				{
					m_CreateCnt++;
				}
				m_Ball_CS.Init ();
			}
			else 
			{
				m_isNextWaveLarge = false;
				m_CreateCnt = 0;
			}
		}







		//イベント管理 全消し　スタート　ゲームオーバー等
		EventManager.GetInstance.EventUpdate();






		//テキスト表示
		int Score = ScoreManager.GetInstance.GetScore();
		m_pScoreLabel.text = "Score:" + Score;
		int AddScore = ScoreManager.GetInstance.GetAddScore();
		m_pAddScoreLabel.text = "" + AddScore;
		int NowWave = ScoreManager.GetInstance.GetWave();
		m_pNowWave.text = "Wave:" + NowWave;













	}
		



}
