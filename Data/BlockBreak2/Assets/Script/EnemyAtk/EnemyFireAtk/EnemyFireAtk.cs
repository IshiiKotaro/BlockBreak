using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireAtk : EnemyAtkBase {

	public int m_pLife;			//生存時間

	Vector3 m_MoveVec;


	void Start ()
	{
		m_MoveVec = new Vector3 (0.0f,-0.2f,0.0f);


	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate (m_MoveVec);
		m_MoveVec *= 0.9f;

		m_pLife--;
		if(m_pLife <= 0)
		{
			m_isDead = true;////////////////////////
			gameObject.SetActive(false);
		}


	}
}
