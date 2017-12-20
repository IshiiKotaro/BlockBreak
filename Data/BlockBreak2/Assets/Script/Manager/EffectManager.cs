using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum EffectType:int
{
	HIT = 0,
	HIT2,
	BOMB,
	LASER_R,
	THUNDER,
	SKILL1,
	SKILL2,

	MAX,
}


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


	public void CreateEffect(int _Id, Vector3 _Pos,float _Ang)
	{
		if (_Id < 0 || _Id >= (int)EffectType.MAX) 
		{
			Debug.Log("範囲外エフェクト" + _Id);
			return;
		}

		//インスタンス生成
		GameObject effect = (GameObject)Instantiate(
			m_pEffectPrefab[_Id],
			_Pos,
			Quaternion.identity
		);
		effect.transform.parent = this.transform;
		effect.transform.Rotate (new Vector3(0,0,_Ang));






		
	}


	public void CheckDelete()
	{
		
	}



}
