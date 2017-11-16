using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager :SingletonMonoBehaviour<EffectManager> {

	public GameObject[] m_pEffectPrefab;


	private EffectManager()
	{
		
	}

	public void Awake(){
		if (this != GetInstance)
		{
			Destroy(this);
			return;
		}
		DontDestroyOnLoad(this.gameObject);
	}


	public void CreateEffect(int _Id, Vector3 _Pos)
	{
		//インスタンス生成
		GameObject item = (GameObject)Instantiate(
			m_pEffectPrefab[_Id],
			_Pos,
			Quaternion.identity
		);
		item.transform.parent = this.transform;

		//エフェクト生成完了




		
	}


	public void CheckDelete()
	{
		
	}



}
