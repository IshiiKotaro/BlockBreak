using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringUp : ItemBase {


	void Update()
	{
		Recast ();
	}
		

	void OnTriggerEnter2D(Collider2D _Other)
	{
		if (_Other.gameObject.tag != "Ball")return;
		if (GetisPlayItem () == false)return;
		MyBall ballCS = _Other.GetComponent<MyBall> ();
		if (ballCS == null)return;

		if (m_NowRecast > 0)return;

		//
		EffectManager.GetInstance.CreateEffect ((int)EffectType.SKILL1,transform.position,0.0f);
		//SE
		SoundManager.GetInstance.PlaySE ((int)SEType.SPRING,1.0f,1.0f);

		//ベクトルを上方向に変える(ただし、真上に飛ばすととある条件下で∞コンボになる。)
		Vector3 vec = ballCS.GetMoveVec();
		vec.y = 1;
		vec.Normalize ();

		ballCS.SetMoveVec (vec);

		SetRecast ();

	}
}
