using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtkManager : SingletonMonoBehaviour<EnemyAtkManager>
{
    public GameObject[] m_EnemyAtkPrefab;

    List<GameObject> m_EnemyList;


    public void Awake()
    {
        if (this != GetInstance)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }


    private EnemyAtkManager()
    {
        m_EnemyList = new List<GameObject>();
    }



    public void Create(int _Id, Vector3 _Pos, GameObject _Owner)
    {
        //上限下限チェック



        //インスタンス生成
        GameObject enemyAtk = (GameObject)Instantiate(
            m_EnemyAtkPrefab[_Id],
            _Pos,
            Quaternion.identity
            );

        //親子関係設定
        enemyAtk.transform.parent = _Owner.transform;



        //リストに入れる
        m_EnemyList.Add(enemyAtk);
    }


    public void CheckDelete()
    {

    }



}
