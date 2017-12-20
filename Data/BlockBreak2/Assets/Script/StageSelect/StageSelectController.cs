using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectController : MonoBehaviour {

	public Text m_p100HighScore;
	public Text m_pQuickHighScore;
	public Text m_pLargeHighScore;
	public Text m_pEndlessHighScore;

	bool m_isTap = false;
	bool m_isNext = false;

	void Update()
	{
		//ハイスコア表示
		m_p100HighScore.text = "\nHighScore:" + PlayerPrefs.GetInt("100WaveHighScore");
		m_pQuickHighScore.text = "\nHighScore:" + PlayerPrefs.GetInt("QuickHighScore");
		m_pLargeHighScore.text = "\nHighScore:" + PlayerPrefs.GetInt("LargeHighScore");



		if (FadeManager.GetInstance.GetIsFadeEnd () == false)return;
		FadeManager.GetInstance.SetFadeEndReset ();


		switch (PlayerPrefs.GetString ("GameMode")) 
		{
		case "100WaveMode":
			Application.LoadLevel ("Main");
			break;
		case "QuickMode":
			Application.LoadLevel ("Main");
			break;
		case "LargeCrowdMode":
			Application.LoadLevel ("Main");
			break;
		case "EndlessMode":
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
		PlayerPrefs.SetString ("GameMode","100WaveMode");

		//フェード開始
		FadeManager.GetInstance.FadeOutInit ();


	}


	public void OnTimeAtkClicked()
	{
		if (m_isTap == true)return;
		m_isTap = true;
		//SEを鳴らす
		SoundManager.GetInstance.PlaySE((int)SEType.DECISION,1.0f,1.0f);

		PlayerPrefs.SetString ("GameMode","QuickMode");


		//フェード開始
		FadeManager.GetInstance.FadeOutInit ();
	}


	public void OnEndlessClicked()
	{
		if (m_isTap == true)return;
		m_isTap = true;
		//SEを鳴らす
		SoundManager.GetInstance.PlaySE((int)SEType.DECISION,1.0f,1.0f);

		PlayerPrefs.SetString ("GameMode","EndlessMode");


		//フェード開始
		FadeManager.GetInstance.FadeOutInit ();
	}


	public void OnLargeCrowdClicked()
	{
		if (m_isTap == true)return;
		m_isTap = true;
		//SEを鳴らす
		SoundManager.GetInstance.PlaySE((int)SEType.DECISION,1.0f,1.0f);

		PlayerPrefs.SetString ("GameMode","LargeCrowdMode");

		//フェード開始
		FadeManager.GetInstance.FadeOutInit ();
	}


	public void OnBackModeClicked()
	{
		//SEを鳴らす
		SoundManager.GetInstance.PlaySE((int)SEType.DECISION,1.0f,1.0f);

		Application.LoadLevel ("ModeSelect");
	}

}
