using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum AssistType:int
{
	SHOT_4WAY = 0,
	SHOT_GUN,
	BOMB,
	HYPER_LASER_R,
	HYPER_LASER_D,
	HYPER_LASER_L,
	HYPER_LASER_T,
	CROSS_LASER,
	THUNDER,
	METEOR,

}


public class AssistManager : SingletonMonoBehaviour<AssistManager>
{
	public GameObject[] m_AssistPrefab;

	//
	public void Awake()
	{
		if (this != GetInstance)
		{
			Destroy(this);
			return;
		}
		DontDestroyOnLoad(this.gameObject);
	}




	public void Create(int _Id,Vector3 _Pos)
	{
		//上限下限チェック



		//インスタンス生成
		GameObject assist = (GameObject)Instantiate(
			m_AssistPrefab[_Id],
			_Pos,
			Quaternion.identity
		);

		AssistBase assistCS = assist.GetComponent<AssistBase>();
		if (assistCS == null) {Debug.Log ("アシストCS　NULL");return;}


		assistCS.SetId(_Id);
	}
}
