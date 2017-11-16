using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*自身の周りを　円を描くように攻撃する敵*/


public class EnemyTower : EnemyBase {

    
    float Ang = 0.0f;


    public override void Init()
    {

		Vector3 atkPos = transform.position;
		atkPos.y -= 0.6f;
		EnemyAtkManager.GetInstance.Create(0, atkPos,transform.gameObject);
    }





}
