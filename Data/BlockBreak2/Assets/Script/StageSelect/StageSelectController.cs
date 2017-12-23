using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectController : MonoBehaviour {

	public Text m_p100HighScore;
	public Text m_pQuickHighScore;
	public Text m_pLargeHighScore;
	public Text m_pEndlessHighScore;

	public GameObject[] m_pButtonList;



	bool m_isTap = false;
	bool m_isNext = false;

	void Start()
	{
		SoundManager.GetInstance.PlayBGM ((int)BGMType.TITLE);
	}


	void Update()
	{
		//ハイスコア表示
		m_p100HighScore.text = "\nHighScore:" + PlayerPrefs.GetInt("100WaveHighScore");
		m_pQuickHighScore.text = "\nHighScore:" + PlayerPrefs.GetInt("QuickHighScore");
		m_pLargeHighScore.text = "\nHighScore:" + PlayerPrefs.GetInt("LargeHighScore");



		if (FadeManager.GetInstance.GetIsFadeEnd () == false)return;
		FadeManager.GetInstance.SetFadeEndReset ();


		switch (PlayerPrefs.GetInt ("GameMode")) 
		{
		case 0:
			Application.LoadLevel ("Main");
			Debug.Log ("100");
			break;
		case 1:
			Application.LoadLevel ("Main");
			Debug.Log ("Quick");
			break;
		case 2:
			Application.LoadLevel ("Main");
			Debug.Log ("Smal");
			break;
		case 3:
			Application.LoadLevel ("Main");
			break;
		}



	}



	public void On100Clicked()
	{
		if (m_isTap == true)return;
		m_isTap = true;
		//SEを鳴らす
		SoundManager.GetInstance.PlaySE((int)SEType.DECISION,1.0f,1.0f);
		PlayerPrefs.SetInt ("GameMode",0);
		ButtonMove ();
		//フェード開始
		FadeManager.GetInstance.FadeOutInit ();
	}


	public void OnTimeAtkClicked()
	{
		if (m_isTap == true)return;
		m_isTap = true;
		//SEを鳴らす
		SoundManager.GetInstance.PlaySE((int)SEType.DECISION,1.0f,1.0f);

		PlayerPrefs.SetInt("GameMode",1);
		ButtonMove ();

		//フェード開始
		FadeManager.GetInstance.FadeOutInit ();
	}


	public void OnEndlessClicked()
	{
		if (m_isTap == true)return;
		m_isTap = true;
		//SEを鳴らす
		SoundManager.GetInstance.PlaySE((int)SEType.DECISION,1.0f,1.0f);



		PlayerPrefs.SetInt("GameMode",3);
		ButtonMove ();

		//フェード開始
		FadeManager.GetInstance.FadeOutInit ();
	}


	public void OnLargeCrowdClicked()
	{
		if (m_isTap == true)return;
		m_isTap = true;
		//SEを鳴らす
		SoundManager.GetInstance.PlaySE((int)SEType.DECISION,1.0f,1.0f);

		PlayerPrefs.SetInt ("GameMode",2);
		ButtonMove ();
		//フェード開始
		FadeManager.GetInstance.FadeOutInit ();
	}


	public void OnBackModeClicked()
	{
		if (m_isTap == true)return;
		m_isTap = true;

		//SEを鳴らす
		SoundManager.GetInstance.PlaySE((int)SEType.DECISION,1.0f,1.0f);
		ButtonMove ();
		Application.LoadLevel ("Title");
	}


	void ButtonMove()
	{
		for (int cnt = 0; cnt < m_pButtonList.Length; cnt++) 
		{
			m_pButtonList [cnt].SetActive (false);
		}
	}

}
