using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager  : SingletonMonoBehaviour<ScoreManager>{

	int m_pNowScore = 0;



	public void Awake()
	{
		if(this != GetInstance)
		{
			Destroy(this);
			return;
		}
		DontDestroyOnLoad(this.gameObject);
	}


	public void AddScore(int _Score)
	{
		m_pNowScore += m_pNowScore;
	}


	void Start () {
		
	}
	

	void Update () {
		
	}





}
