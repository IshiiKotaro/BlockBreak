using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear : MonoBehaviour {

	public GameObject m_pStageClearMessage;
	public GameObject[] m_pBlockPrefab;

	GameObject m_StageClearMessage;
	List<GameObject> m_BlockList;


	float m_NowTime = 0;
	int m_NowIndex = 0;

	bool m_isClear;
	bool m_isFanfare;



	//======================================
	//	セッター
	//======================================



	void Start()
	{
		
	}


	public void Init()
	{
		//クリアメッセージ生成
		m_StageClearMessage = (GameObject)Instantiate(
			m_pStageClearMessage, 
			transform.position,
			Quaternion.identity
		);
		SpriteManager.GetInstance.SetSpriteAlpha (m_StageClearMessage,0.0f);



		m_BlockList = new List<GameObject> ();
		//=====================================
		//ブロック生成
		//=====================================
		for (int cnt = 0; cnt < 6; cnt++)
		{
			GameObject block = (GameObject)Instantiate(
				m_pBlockPrefab[cnt],
				transform.position,
				Quaternion.identity
			);
				
			//拡縮と座標の調整
			float blockSize = 0.6f;
			block.transform.localScale = new Vector3(blockSize,blockSize,1.0f);

			float x = (float)(((cnt - 2) * 1.4f)* block.transform.localScale.x) -  (blockSize * 0.7f);
			block.transform.position = new Vector3 (x,transform.position.y,transform.position.z);

			//ブロックのα値を0に、
			SpriteManager.GetInstance.SetSpriteAlpha(block,0.0f);


			m_BlockList.Add (block);
		}
		m_NowTime = 0;
		m_NowIndex = 0;

		m_isClear = false;
		m_isFanfare = false;
	}

	
	// Update is called once per frame
	public void GameClearUpdate () {

		GameClearCheck ();

		if (m_isClear == false)return;

		m_NowTime += (1 * Time.deltaTime);

		BlockBreak ();

		Fanfare ();





	}


	void GameClearCheck()
	{
		if (m_isClear == true)return;

		//100Waveのクリア判定
		Clear100WaveModeCheck();

		//クイックモードのクリア判定
		ClearQuickModeCheck();

		//大群モードのクリア判定
		ClearLargeCrowdModeCheck();

		//
		if(m_isClear == false)return;


		//勝利の準備をする
		SpriteManager.GetInstance.SetSpriteAlpha (m_StageClearMessage,1.0f);
		for (int cnt = 0; cnt < m_BlockList.Count; cnt++)
		{
			SpriteManager.GetInstance.SetSpriteAlpha (m_BlockList[cnt],1.0f);
		}

		//SE再生
		SoundManager.GetInstance.PlaySE((int)SEType.LOSE,1.0f,1.0f);


		EventManager.GetInstance.SetIsEvent (true);
	}


	void BlockBreak()
	{
		//いい感じの間隔でブロックを破壊し、その下にある　Clear！の文字を出す
		if (m_NowTime <= 0.2 * m_NowIndex + 2.0f)return; 
		if (m_isFanfare == true)return;


		if (m_NowIndex >= m_BlockList.Count) 
		{
			m_isFanfare = true;
			m_NowTime = 0.0f;
			return;
		}

		//今のIndexが指してるブロックを消す
		SpriteManager.GetInstance.SetSpriteAlpha(m_BlockList[m_NowIndex],0.0f);

		//破壊SE
		SoundManager.GetInstance.PlaySE((int)SEType.HIT1,1.0f,1.0f);

		//エフェクト
		EffectManager.GetInstance.CreateEffect((int)EffectType.SKILL2,m_BlockList[m_NowIndex].transform.position,0.0f);

		//カメラ揺らす
		EventManager.GetInstance.CamQuake(-0.2f,-0.2f);

		m_NowIndex++;
	}


	void Fanfare()
	{
		if (m_isFanfare == false)return;

		//一定間隔で花火散らす







		if (m_NowTime < 5)return;

		//戦績をリザルトに送る。
		PlayerPrefs.SetInt("GameMode",0);
		PlayerPrefs.SetInt("MyScore",ScoreManager.GetInstance.GetScore());
		PlayerPrefs.SetInt ("MaxCombo",ScoreManager.GetInstance.GetMaxCombo());
		PlayerPrefs.SetFloat ("ClearBonus", 1.25f);

		EventManager.GetInstance.SetIsEvent (false);

		Application.LoadLevel ("Result");
	}


	void Clear100WaveModeCheck()
	{
		if (PlayerPrefs.GetInt ("GameMode") != 0)return; 
		if (ScoreManager.GetInstance.GetWave () < 100)return; 
		if (BlockManager.GetInstance.GetisBlockAllDelete () == false)return;


		m_isClear = true;
	}


	void ClearQuickModeCheck()
	{
		if (PlayerPrefs.GetInt ("GameMode") != 1)return; 
		if (ScoreManager.GetInstance.GetWave () < 100)return; 
		if (BlockManager.GetInstance.GetisBlockAllDelete () == false)return;

		m_isClear = true;
	}

	//大群モードのクリア判定
	void ClearLargeCrowdModeCheck()
	{
		if (PlayerPrefs.GetInt ("GameMode") != 2)return; 
		if (ScoreManager.GetInstance.GetWave () < 100)return; 
		if (BlockManager.GetInstance.GetisBlockAllDelete () == false)return;

		m_isClear = true;
	}

}
