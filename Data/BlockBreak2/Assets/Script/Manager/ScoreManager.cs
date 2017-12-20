using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager  : SingletonMonoBehaviour<ScoreManager>{

	public GameObject m_pComboPrefab;

	GameObject nowCombo;

	int m_NowScore = 0;
	int m_AddScore = 0;
	int m_AddTime = 0;		//スコアを実際に加算するまでの時間
	int m_NowWave = 0;
	int m_NowCombo = 0;
	int m_MaxCombo = 0;

	public void Awake()
	{
		if(this != GetInstance)
		{
			Destroy(this);
			return;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	//===============================
	//	ゲッター
	//===============================

	public int GetScore(){ return m_NowScore; }

	public int GetAddScore(){ return m_AddScore; }

	public int GetWave(){ return m_NowWave; }

	public int GetMaxCombo(){ return m_MaxCombo; }

	public int GetNowCombo(){ return m_NowCombo; }


	//===============================
	//	セッター
	//===============================

	public void AddScore(int _Score)
	{
		m_AddScore += _Score;
		m_AddTime = 60;
	}

	public void AddWave(){ m_NowWave++; }


	public void AddCombo(Vector3 _Pos){
		m_NowCombo++;
	
		//今のコンボ数をグラフィカルに表示
		nowCombo = (GameObject)Instantiate(m_pComboPrefab,_Pos,Quaternion.identity);
		
	}


	public void ResetNowCombo(){ m_NowCombo = 0; }


	//
	public void Init()
	{
		m_NowScore = 0;
		m_AddScore = 0;
		m_AddTime = 0;
		m_NowWave = 0;

		m_NowCombo = 0;
		m_MaxCombo = 0;
	}


	void Start () {
		
	}
	

	void Update () {
		m_AddTime--;
		if(m_AddTime <= 0)
		{
			if (m_AddScore <= 0)return;
			//スコアを加算(ラチェクラのボルト方式)
			int Score;
			if (m_AddScore < 20) 
			{
				Score = m_AddScore;
			} 
			else
			{
				Score = (int)(m_AddScore * 0.2);
			}


			m_NowScore += Score;
			m_AddScore -= Score;
		}

		//コンボ
		if (m_NowCombo > m_MaxCombo) 
		{
			m_MaxCombo = m_NowCombo;
		}



	}





}
