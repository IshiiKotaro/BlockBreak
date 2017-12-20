using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundData : MonoBehaviour {

	AudioSource m_SoundData;

	int m_SoundType;	//サウンドの種類　BGMなのかSEなのか
	int m_BGMType = -1;
	int m_SEType = -1;

	//==================================
	//	ゲッター
	//==================================

	public int GetBGMType(){ return m_BGMType; }


	/*初期化
	_SoundType		:BGM SE識別
	_DetailedType 	:↑の詳細ID　BGMなら、タイトルBGMなのかステージBGMなのか等
	_Vol 			:ボリューム
	_Pitch 			:ピッチ*/
	public void Init(int _SoundType,int _DetailedType,float _Vol,float _Pitch)
	{
		m_SoundData = GetComponent<AudioSource> ();
		m_SoundData.volume = _Vol;
		m_SoundData.pitch = _Pitch;

		m_SoundType = _SoundType;

	}


	void Update () {
		if (m_SoundData == null)return;
		if (m_SoundData.isPlaying == true)return;
		if (m_SoundType == (int)SoundType.BGM)return;

		Destroy (gameObject);
	}
}
