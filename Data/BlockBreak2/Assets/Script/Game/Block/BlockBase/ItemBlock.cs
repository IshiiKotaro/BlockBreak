using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBlock : BlockBase {




	void OnCollisionEnter2D(Collision2D _Other)
	{
		//Debug.Log("ItemBlockクラスのあたり判定");
		m_Hp = 0;
	}

}
