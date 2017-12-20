using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SoundType:int
{
	BGM = 0,
	SE,
};


enum SEType:int
{
	HIT1 = 0,
	BOMB,
	GUN1,
	GUN2,
	THUNDER,
	DECISION,
	WARNING,
	ASSIST,
	BLOCKCREATE,
	SPRING,
	SWING,
	START,
};


enum BGMType:int
{
	TITLE = 0,
	STAGEBGM1,
}


public class SoundManager : SingletonMonoBehaviour<SoundManager>{

	public GameObject[] m_BGMPrefab;
	public GameObject[] m_SEPrefab;

	List<GameObject> m_BGMList;

	public void Awake(){
		if (this != GetInstance)
		{
			Destroy(this);
			return;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	//
	public void Init()
	{
		
	}


	/*SEの再生
	_Type	:再生するSEの種類
	_Vol	:ボリューム　0.0f~1.0f
	_Pitch	:ピッチ	デフォルト1.0f*/
	public void PlaySE(int _Type,float _Vol,float _Pitch)
	{
		GameObject se = (GameObject)Instantiate(
			m_SEPrefab[_Type],
			Vector3.zero,
			Quaternion.identity
		);
		SoundData seCS = se.GetComponent<SoundData> ();
		if (seCS == null)return;


		seCS.Init ((int)SoundType.SE,0,_Vol,_Pitch);
	}


	public void PlayBGM(int _Type)
	{
		
		GameObject bgm = (GameObject)Instantiate(
			m_BGMPrefab[_Type],
			Vector3.zero,
			Quaternion.identity
		);
		SoundData bgmCS2 = bgm.GetComponent<SoundData> ();
		if (bgmCS2 == null)return;

		bgmCS2.Init ((int)SoundType.BGM,_Type,1.0f,1.0f);


	}


	public void StopBGM(int _Type)
	{
		for (int cnt = 0; cnt < m_BGMList.Count; cnt++) 
		{
			AudioSource audio = m_BGMList [cnt].GetComponent<AudioSource> ();
			if (audio == null)continue;
			SoundData bgmCS = m_BGMList[cnt].GetComponent<SoundData> ();
			if (bgmCS == null)continue;
			if (bgmCS.GetBGMType () != _Type)continue;

			audio.Stop ();
		}
	}

}
