using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSelectController : MonoBehaviour {


	public void OnStageSelectClicked()
	{
		//SEを鳴らす
		SoundManager.GetInstance.PlaySE((int)SEType.DECISION,1.0f,1.0f);

		Application.LoadLevel ("StageSelect");
	}


	public void OnShopClicked()
	{
		//SEを鳴らす
		SoundManager.GetInstance.PlaySE((int)SEType.DECISION,1.0f,1.0f);

		Application.LoadLevel ("Shop");
	}


	public void OnAssitIconClicked()
	{
		//SEを鳴らす
		SoundManager.GetInstance.PlaySE((int)SEType.DECISION,1.0f,1.0f);

		Application.LoadLevel ("Main");
	}


	public void OnBackTitleClicked()
	{
		//SEを鳴らす
		SoundManager.GetInstance.PlaySE((int)SEType.DECISION,1.0f,1.0f);

		Application.LoadLevel ("Title");
	}

}
