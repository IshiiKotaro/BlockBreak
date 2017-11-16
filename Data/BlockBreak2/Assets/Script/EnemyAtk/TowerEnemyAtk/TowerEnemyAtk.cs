using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEnemyAtk : MonoBehaviour {

	float m_Ang = 0;


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//親を中心にぐるぐる回る
		Vector3 parentPos = transform.parent.position;
		m_Ang += 3.0f;
		if (m_Ang >= 360.0f)m_Ang -= 360.0f;

		//座標更新
		float moveX = Mathf.Cos (m_Ang * (Mathf.PI / 180));
		float moveY = Mathf.Sin (m_Ang * (Mathf.PI / 180));
		moveX *= 0.03f;
		moveY *= 0.03f;

		Vector3 newPos = new Vector3 ();
		newPos.x = moveX;
		newPos.y = moveY;
		newPos.z = 0;

		transform.Translate (newPos);



	}
}
