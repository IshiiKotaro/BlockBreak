using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

	public GameObject m_pBlackTex;
	public GameObject m_pGameOverTex;

	GameObject m_BlackTex;
	GameObject m_GameOverTex;

	bool m_isGameOver;
	bool m_isNext;


	float m_BlackAlpha;
	float m_GameOverPosY;
	float m_GameOverAlpha;
	int m_Time;


	public void Init()
	{
		m_isGameOver = false;
		m_isNext = false;

		m_BlackAlpha = 0.0f;
		m_GameOverPosY = 0.5f;
		m_GameOverAlpha = 0.0f;
		m_Time = 0;


		m_BlackTex  = (GameObject)Instantiate(
			m_pBlackTex, 
			transform.position, 
			Quaternion.identity
		);
		m_GameOverTex  = (GameObject)Instantiate(
			m_pGameOverTex, 
			transform.position, 
			Quaternion.identity
		);



		SpriteManager.GetInstance.SetSpriteAlpha (m_BlackTex,m_BlackAlpha);
		SpriteManager.GetInstance.SetSpriteAlpha (m_GameOverTex,m_BlackAlpha);
	}


	void GameOverCheck()
	{
		if (m_isGameOver == true)return;

		//大群モードの時だけ、ゲームオーバー条件が違うので、チェック
		int gamemode = (int)BlockMassIndex.GAMEOVER;
		if (PlayerPrefs.GetInt ("GameMode") == 2) 
		{
			gamemode = 29;
		}


		BlockMass[] massObjects = GameObject.FindObjectsOfType<BlockMass>();
		foreach (BlockMass Idx in massObjects)
		{
			if (Idx.GetIndex () <  gamemode)continue;

			//カメラの揺れ
			EventManager.GetInstance.CamQuake (0.1f,-0.1f);

			//SE再生
			SoundManager.GetInstance.PlaySE((int)SEType.LOSE,1.0f,1.0f);

			m_isGameOver = true;
		}
	}


	public void GameOverUpdate()
	{
		GameOverCheck ();

		if (m_isGameOver == false)return;
		EventManager.GetInstance.SetIsEvent (true);

		if (m_BlackAlpha < 0.4f)
		{
			m_BlackAlpha += 0.01f;
		}
		else
		{
			if (m_GameOverPosY > 0.0f)
			{
				m_GameOverPosY -= 0.02f;
			}
			if (m_GameOverAlpha < 1.0f) 
			{
				m_GameOverAlpha += 0.01f;
			}
		}

		m_Time++;
		if (m_Time >= 230) 
		{
			m_isNext = true;
		}


		//α値と座標の繁栄
		SpriteManager.GetInstance.SetSpriteAlpha (m_GameOverTex,m_GameOverAlpha);
		SpriteManager.GetInstance.SetSpriteAlpha (m_BlackTex,m_BlackAlpha);

		m_GameOverTex.transform.position = new Vector3 (0.0f,m_GameOverPosY,0.0f);



		//リザルトへGO
		if (m_isNext == true)
		{
			//戦績をリザルトに送る。
			PlayerPrefs.SetInt("MyScore",ScoreManager.GetInstance.GetScore());
			PlayerPrefs.SetInt ("MaxCombo",ScoreManager.GetInstance.GetMaxCombo());
			PlayerPrefs.SetFloat ("ClearBonus", 0.5f);

			EventManager.GetInstance.SetIsEvent (false);

			Application.LoadLevel ("Result");
		}

	}



}
