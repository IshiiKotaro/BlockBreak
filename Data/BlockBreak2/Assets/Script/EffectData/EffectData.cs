using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectData : MonoBehaviour {

	ParticleSystem m_Particle;

	// Use this for initialization
	void Start () 
	{
		
		m_Particle = GetComponent<ParticleSystem> ();
		if (m_Particle == null) 
		{
			Debug.Log ("パーティクル取得に失敗しました");
		}

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (m_Particle == null)return;

		if (m_Particle.isPlaying == true)return;

		Destroy (gameObject);
		Debug.Log ("パーティクルを削除しました");
	}
}
