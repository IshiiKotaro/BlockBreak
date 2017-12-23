using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour {

	public GameObject m_pHighScorePrefab;

	public Text m_pGameMode;
	public Text m_pMyScore;
	public Text m_pMaxCombo;
	public Text m_pClearBonus;
	public Text m_pTotalScore;

	GameObject m_HighScore;

	//string m_GameMode;
	int m_GameModeCnt;

	int m_MyScore = 0;		//自身が稼いだスコア
	int m_MyScoreAdd = 0;	//スコア足し込む用

	int m_MaxCombo = 0;
	int m_MaxComboAdd = 0;

	float m_ClearBunus = 0.0f;

	int m_TotalScore = 0;
	int m_TotalScoreAdd = 0;

	int m_GetMoney = 0;
	int m_GetMoneyAdd = 0;

	bool m_isHighScore = false;
	int m_BeforeHighScore = 0;

	int m_NowTime = 0;

	int m_Order = (int)Order.GAME_MODE;

	enum Order : int
	{
		GAME_MODE = 0,
		MY_SCORE,
		MAX_COMBO,
		CLEAR_BONUS,
		TOTAL_SCORE,
		GET_MONEY,
		MAX,
	}






	void Start()
	{
		//インスタンス生成
		m_HighScore = (GameObject)Instantiate(m_pHighScorePrefab,transform.position,Quaternion.identity);

		SpriteManager.GetInstance.SetSpriteAlpha(m_HighScore,0.0f);
		m_HighScore.transform.position = new Vector3(0,0,0);

		//初期化
		m_MyScore = 0;
		m_MyScoreAdd = 0;

		m_MaxCombo = 0;
		m_MaxComboAdd = 0;

		m_ClearBunus = 0.0f;

		m_TotalScore = 0;
		m_TotalScoreAdd = 0;

		m_GetMoney = 0;
		m_GetMoneyAdd = 0;

		m_isHighScore = false;
		m_BeforeHighScore = 0;

		m_NowTime = 0;

		//戦績を取得
		m_GameModeCnt = PlayerPrefs.GetInt("GameMode");
		m_MyScoreAdd = PlayerPrefs.GetInt("MyScore");
		m_MaxComboAdd = PlayerPrefs.GetInt ("MaxCombo");
		m_ClearBunus = PlayerPrefs.GetFloat ("ClearBonus");


		//スコアを求める。
		m_TotalScoreAdd = (int)((m_MyScoreAdd + (m_MaxComboAdd * 5000)) * m_ClearBunus);
		m_GetMoneyAdd = (int)(m_TotalScoreAdd * 0.0001);


		//セーブ
		int NowMoney = PlayerPrefs.GetInt("Money");
		NowMoney += m_GetMoneyAdd;
		PlayerPrefs.SetInt ("Money", NowMoney);

		//ハイスコアチェック と　ゲームモード識別
		switch (m_GameModeCnt)
		{
		case 0:
			m_pGameMode.text = "100WaveMode";

			m_BeforeHighScore = PlayerPrefs.GetInt ("100WaveHighScore");
			if (m_TotalScoreAdd <= m_BeforeHighScore)break;
			m_isHighScore = true;
			PlayerPrefs.SetInt ("100WaveHighScore",m_TotalScoreAdd);
			Debug.Log ("100モード更新");
			break;
		case 1:
			m_pGameMode.text = "QuickMode";

			m_BeforeHighScore = PlayerPrefs.GetInt ("QuickHighScore");
			if (m_TotalScoreAdd <= m_BeforeHighScore)break;
			m_isHighScore = true;
			PlayerPrefs.SetInt ("QuickHighScore", m_TotalScoreAdd);
			Debug.Log ("クイックモードハイスコア更新");
			break;
		case 2:
			m_pGameMode.text = "SmallMode";

			m_BeforeHighScore = PlayerPrefs.GetInt("LargeHighScore");
			if (m_TotalScoreAdd <= m_BeforeHighScore)break;
			m_isHighScore = true;
			PlayerPrefs.SetInt ("LargeHighScore",m_TotalScoreAdd);
			Debug.Log ("スモールモードハイスコア更新");
			break;
		}
		m_Order = (int)Order.GAME_MODE;

		Debug.Log ("highscore:" + m_BeforeHighScore);

		SoundManager.GetInstance.PlayBGM ((int)BGMType.RESULT);
	}


	void Update () 
	{
		switch (m_Order) 
		{
		case (int)Order.GAME_MODE:
			m_Order++;
			break;
		case (int)Order.MY_SCORE:
			if (MyScoreUpdate () == true)m_Order++;
			break;
		case (int)Order.MAX_COMBO:
			if (MaxComboUpdate () == true)m_Order++;
			break;
		case (int)Order.CLEAR_BONUS:
			if (ClearBonusUpdate () == true)m_Order++;
			break;
		case (int)Order.TOTAL_SCORE:
			if (TotalScoreUpdate () == true)m_Order++;
			break;
		case (int)Order.GET_MONEY:
			if (GetMoneyUpdate () == true)m_Order++;
			break;
		}

		//ハイスコア演出
		HighScoreUpdate();




		//テキスト更新
		m_pMyScore.text = "Score:" + m_MyScore;
		m_pMaxCombo.text = "MaxCombo:" + m_MaxCombo;
		m_pClearBonus.text = "ClearBonus:  ×" + m_ClearBunus;
		m_pTotalScore.text = "TotalScore:" + m_TotalScore;



	}


	bool MyScoreUpdate()
	{
		if (m_MyScoreAdd <= 0)return true;


		int Score = (int)(m_MyScoreAdd * 0.05);
		m_MyScore += Score;
		m_MyScoreAdd -= Score;
		if (m_MyScoreAdd <= 100) 
		{
			m_MyScore += m_MyScoreAdd;
			m_MyScoreAdd = 0;
		}
		return false;
	}


	bool MaxComboUpdate()
	{
		if (m_MaxComboAdd <= 0)return true;


		int Score = (int)(m_MaxComboAdd * 0.05);
		m_MaxCombo += Score;
		m_MaxComboAdd -= Score;
		if (m_MaxComboAdd <= 100) 
		{
			m_MaxCombo += m_MaxComboAdd;
			m_MaxComboAdd = 0;
		}
		return false;
	}


	bool ClearBonusUpdate()
	{
		return true;
	}


	bool TotalScoreUpdate()
	{
		if (m_TotalScoreAdd <= 0)return true;


		int Score = (int)(m_TotalScoreAdd * 0.05);
		m_TotalScore += Score;
		m_TotalScoreAdd -= Score;
		if (m_TotalScoreAdd <= 100) 
		{
			m_TotalScore += m_TotalScoreAdd;
			m_TotalScoreAdd = 0;
		}
		return false;
	}


	bool GetMoneyUpdate()
	{
		if (m_GetMoneyAdd<= 0)return true;


		int Score = (int)(m_GetMoneyAdd * 0.05);
		m_GetMoney += Score;
		m_GetMoneyAdd -= Score;
		if (m_GetMoney <= 50) 
		{
			m_GetMoney += m_GetMoneyAdd;
			m_GetMoneyAdd = 0;
		}
		return false;
	}


	void HighScoreUpdate()
	{
		if (m_isHighScore == false)return;
		if (m_Order < (int)Order.MAX)return;
		m_NowTime++;
		if (m_NowTime < 50)return;
		if (m_NowTime == 50) 
		{
			Vector3 effectPos = new Vector3 ();
			effectPos = transform.position;
			effectPos.y = 0.0f;
			for (int cnt = -1; cnt < 2; cnt++) 
			{
				//EffectManager.GetInstance.CreateEffect ((int)EffectType.SKILL1, new Vector3(effectPos.x + cnt,effectPos.y,effectPos.z), 0.0f);
				EffectManager.GetInstance.CreateEffect ((int)EffectType.SKILL1, new Vector3(0 + cnt,0,0), 0.0f);
			}
			SoundManager.GetInstance.PlaySE ((int)SEType.ASSIST,1.0f,1.0f);
			SpriteManager.GetInstance.SetSpriteAlpha(m_HighScore,1.0f);
		}




		Debug.Log ("～ハイスコア更新中～");

	}

	//ボタンクリック時
	public void OnTitleButtonClicked()
	{


		//SEを鳴らす
		SoundManager.GetInstance.PlaySE((int)SEType.DECISION,1.0f,1.0f);

		Application.LoadLevel ("StageSelect");

	}


}
