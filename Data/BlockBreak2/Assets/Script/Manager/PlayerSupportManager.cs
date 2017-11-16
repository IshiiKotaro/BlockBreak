using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーをサポートするものまねーじゃー　今のところバーだけを管理。
public class PlayerSupportManager : SingletonMonoBehaviour<PlayerSupportManager> {
	
	//バーのプレファブデータ
	public GameObject BarStand;
	public GameObject BarShield;




	public void Awake()
	{
		if(this != GetInstance)
		{
			Destroy(this);
			return;
		}
		DontDestroyOnLoad(this.gameObject);
	}
		

	//===========================================
	//	セッター
	//===========================================
	public void SetActiveBar()
	{
		if (BarShield == null)return;
		if (BarStand == null)return;

		BarShield.SetActive (true);
		BarStand.SetActive (true);

		//Debug.Log ("バーをアクティブにする！");

	}

	//


}
