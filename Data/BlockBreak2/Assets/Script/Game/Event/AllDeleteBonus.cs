using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDeleteBonus : MonoBehaviour {

	public GameObject m_pAllBreakMessage;
	public GameObject m_pBlack;

	const float cDefMessageX = 10.0f;		//全消しメッセージの初期座標



	GameObject m_AllBreakMessage;
	GameObject m_Black;

	bool m_isAllBlockBreak = false;	//全消しフラグ
	int m_AllBlockBreakCnt = 0;		//全消し回数
	int m_BonusCnt = 0;				//ボーナスブロック生成カウント
	int m_MaxBonusCnt = 4;
	bool m_isEvent = false;


	//フェード
	float m_FeedAlpha = 0.0f;


	//全消しメッセージX座標
	float m_MessageX = cDefMessageX;


	//========================
	//ゲッター
	//========================

	public bool GetisEvent(){ return m_isEvent; }

	//========================
	//セッター
	//========================

	public void AddBonusCnt(int _Add){ m_BonusCnt += _Add; }



	public void Init()
	{
		//変数初期化
		m_isAllBlockBreak = false;
		m_AllBlockBreakCnt = 0;
		m_BonusCnt = 0;
		m_MaxBonusCnt = 4;
		m_isEvent = false;

		m_FeedAlpha = 0.0f;
		m_MessageX = cDefMessageX;

		//
		m_AllBreakMessage = (GameObject)Instantiate(m_pAllBreakMessage,transform.position,Quaternion.identity);
		m_Black = (GameObject)Instantiate(m_pBlack,transform.position,Quaternion.identity);

		SpriteRenderer BlackSP = m_Black.GetComponent<SpriteRenderer> ();
		var color = BlackSP.color;
		color.a = m_FeedAlpha;
		BlackSP.color = color;

		MessagePosInit ();
	}


	void MessagePosInit()
	{
		m_AllBreakMessage.transform.position = new Vector3 (cDefMessageX,transform.position.y,0.0f);
		m_MessageX = cDefMessageX;
	}



	//全消しチェック
	public void AllBlockBreakCheck()
	{

		if (m_isAllBlockBreak == false){
			//目標のWaveに到達してるかチェック
			switch (PlayerPrefs.GetString ("GameMode")) 
			{
			case "100WaveMode":
				if (ScoreManager.GetInstance.GetWave () >= 100)return;
				break;
			case "QuickMode":
				if (ScoreManager.GetInstance.GetWave () >= 100)return;
				break;
			case "LargeCrowdMode":
				if (ScoreManager.GetInstance.GetWave () >= 100)return;
				break;
			}

			//全てのブロック列が死滅してるかチェック
			if(BlockManager.GetInstance.GetisBlockAllDelete() == false)return;
			m_isAllBlockBreak = true;
			m_BonusCnt = 0;
			m_isEvent = true;
		} 



		if (m_isAllBlockBreak == false)return; 
		{
			//
			MessagePosUpdate();


			//メッセージが終わったら
			if(FeedIn() == false)return;

			//ボーナスブロック列生成
			if (m_BonusCnt < m_MaxBonusCnt)
			{
				BlockManager.GetInstance.NextWave(true,true,true,m_BonusCnt);

			} 
			else 
			{
				//ボーナスブロック生成終了
				m_isAllBlockBreak = false;
				m_AllBlockBreakCnt++;
				m_MaxBonusCnt = 4 + (m_AllBlockBreakCnt / 2);
				if (m_MaxBonusCnt > 12)m_MaxBonusCnt = 12;
				m_isEvent = false;

				//ブロックマネージャーに知らせる
				BlockManager.GetInstance.SetisNextWave (false);

				//後始末
				FeedOut();
				MessagePosInit ();
			}
		}
	}


	//フェードインが終わったらtrueを返す
	bool FeedIn()
	{
		m_FeedAlpha += 0.2f * Time.deltaTime;

		SpriteRenderer BlackSP = m_Black.GetComponent<SpriteRenderer> ();
		var color = BlackSP.color;
		color.a = m_FeedAlpha;
		BlackSP.color = color;



		if (m_FeedAlpha <= 0.3f) return false;

		m_FeedAlpha = 0.3f;
		return true;
	}

	//フェードアウト
	bool FeedOut()
	{
		m_FeedAlpha = 0.0f;

		SpriteRenderer BlackSP = m_Black.GetComponent<SpriteRenderer> ();
		var color = BlackSP.color;
		color.a = m_FeedAlpha;
		BlackSP.color = color;
		return true;
	}


	void MessagePosUpdate()
	{
		if (m_MessageX >= 0.05f) 
		{
			m_MessageX *= 0.9f;
		}
		else
		{
			if (m_MessageX >= 0.0f)m_MessageX *= -1.0f;
			m_MessageX *= 1.1f;
		}
		m_AllBreakMessage.transform.position = new Vector3 (m_MessageX, transform.position.y, 0.0f);
	}
}
