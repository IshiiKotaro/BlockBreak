using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtkBase : MonoBehaviour {

    GameObject m_OwnerEnemy;    //持ち主のエネミー
	protected bool m_isDead = false;

    //===============================
    //  ゲッター
    //===============================

    public GameObject GetOwner() { return m_OwnerEnemy; }

    //===============================
    //  セッター
    //===============================

    public void SetOwner(GameObject _Owner) { m_OwnerEnemy = _Owner; }





}
