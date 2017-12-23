using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : SingletonMonoBehaviour<EventManager> {

	public GameObject m_pAllDeleteBonus;
	public GameObject m_pGameStart;
	public GameObject m_pGameOver;
	public GameObject m_pGameClear;
	public GameObject m_pTutorial;
	public GameObject m_pChainBonus;

	AllDeleteBonus m_AllDeleteCS;
	GameStartMessage m_GameStartCS;
	GameOver m_GameOverCS;
	GameClear m_GameClearCS;
	Tutorial m_TutorialCS;
	ChainBonus m_ChainBonusCS;


	bool m_isEvent = false;		//イベントフラグ
	bool m_isStart = false;		

	int m_StartBlockCnt = 0;	//初期のブロック列数



	//========================
	//ゲッター
	//========================

	public bool GetIsEvent(){ return m_isEvent; }

	//========================
	//セッター
	//========================

	public void SetIsEvent(bool _flg ){ m_isEvent = _flg; }

	public void AddBonusCnt(int _Add){
		if (m_AllDeleteCS == null)return;
		m_AllDeleteCS.AddBonusCnt (_Add);
	}


	public void SetChainBonus()
	{
		if (m_ChainBonusCS == null)return;

		m_ChainBonusCS.SetChainBonus ();
	}



	public void CamQuake(float _QuakeX,float _QuakeY)
	{
		
		CameraController[] CamObjects = GameObject.FindObjectsOfType<CameraController>();
		foreach (CameraController Idx in CamObjects)
		{
			Idx.SetQuakeDis (_QuakeX,_QuakeY);
		}
	}

	//

	public void Awake(){
		if (this != GetInstance)
		{
			Destroy(this);
			return;
		}
		DontDestroyOnLoad(this.gameObject);
	}


	void Start()
	{


		//Init ();
	}


	public void Init()
	{

		m_AllDeleteCS = m_pAllDeleteBonus.GetComponent<AllDeleteBonus> ();
		m_GameStartCS = m_pGameStart.GetComponent<GameStartMessage> ();
		m_GameOverCS = m_pGameOver.GetComponent<GameOver> ();
		m_GameClearCS = m_pGameClear.GetComponent<GameClear> ();
		m_TutorialCS = m_pTutorial.GetComponent<Tutorial> ();
		m_ChainBonusCS = m_pChainBonus.GetComponent<ChainBonus> ();

		if (m_AllDeleteCS == null)Debug.Log("AllDeleteCS　NULL");
		if (m_GameStartCS == null)Debug.Log("GameStartCS　NULL");
		if (m_GameOverCS == null)Debug.Log("GameOverCS　NULL");
		if (m_GameClearCS == null)Debug.Log("GameClearCS　NULL");
		if (m_TutorialCS == null)Debug.Log("TutorialCS　NULL");
		if (m_ChainBonusCS == null)Debug.Log ("ChainBonusCS NULL");

		if (m_AllDeleteCS != null)
		{
			m_AllDeleteCS.Init ();
		}
		if (m_GameStartCS != null) 
		{
			m_GameStartCS.Init ();
		}
		if (m_GameOverCS != null) 
		{
			m_GameOverCS.Init ();
		}
		if (m_GameClearCS != null) 
		{
			m_GameClearCS.Init ();
		}
		if (m_TutorialCS != null) 
		{
			m_TutorialCS.Init ();
		}
		if (m_ChainBonusCS != null) 
		{
			m_ChainBonusCS.Init();
		}

	
		m_isEvent = false;
		m_isStart = false;

		//初期生成ブロック数
		m_StartBlockCnt = 4;
	}


	public void EventUpdate() 
	{

		//チュートリアルのメッセージを先に挟む
		m_TutorialCS.TutorialMessageUpdate ();
		if (m_TutorialCS.GetIsTutorialEnd () == false)return;


		if (m_isStart == false)
		{
			//初期ブロック生成
			if (BlockManager.GetInstance.NextWave (true,false,true,-1) == true)m_StartBlockCnt--;
			if(m_StartBlockCnt <= 0)m_isStart = true;
		}

		//ReadyGoの更新
		m_GameStartCS.StartMessage ();


		//ゲームオーバーチェック & Update
		m_GameOverCS.GameOverUpdate();


		//クリアチェック
		m_GameClearCS.GameClearUpdate();


		//イベント　全消しチェック
		m_AllDeleteCS.AllBlockBreakCheck();

		//チェインボーナス
		m_ChainBonusCS.ChainBonusUpdate();


	}
		

}
