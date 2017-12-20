﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour {

	void Start()
	{
		FadeManager.GetInstance.AllInit ();
		SoundManager.GetInstance.Init ();

		SoundManager.GetInstance.PlayBGM ((int)BGMType.TITLE);

		//セーブデータ初期化　後で消す
		PlayerPrefs.SetInt ("100WaveHighScore",1);
		PlayerPrefs.SetInt ("QuickHighScore", 1);
		PlayerPrefs.SetInt ("LargeHighScore", 1);
		PlayerPrefs.SetInt ("Money", 114514);
	}


	void Update()
	{
		if (Input.GetButton ("Fire1") == false)return;

		//SEを鳴らす
		SoundManager.GetInstance.PlaySE((int)SEType.DECISION,1.0f,1.0f);

		Application.LoadLevel ("ModeSelect");
	}

}