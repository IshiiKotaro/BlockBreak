using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SingletonMonoBehaviour<EnemyManager>
{
    public GameObject[] m_EnemyPrefab;

    List<GameObject> m_EnemyList;

    List<int> m_DeleteIdx;          //削除するインデックス


    private EnemyManager()
    {
        m_EnemyList = new List<GameObject>();
        m_DeleteIdx = new List<int>();

    }

	//====================================
	//ゲッター
	//====================================

	public int GetPrefabListSize(){ return m_EnemyPrefab.Length; }

	//
    public void Awake()
    {
        if (this != GetInstance)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }


    public void Create(int _Id,Vector3 _Pos,GameObject _Owner)
    {

        //上限下限チェック


        //インスタンス生成
        GameObject enemy = (GameObject)Instantiate(
            m_EnemyPrefab[_Id],
            _Pos,
            Quaternion.identity
            );


        EnemyBase enemyCS = enemy.GetComponent<EnemyBase>();

        //親子関係とオーナーの設定
        enemy.transform.parent = _Owner.transform;
        enemyCS.SetOwner(_Owner);

        //初期化
        enemyCS.Init();




        //リストに入れる
        m_EnemyList.Add(enemy);

    }


    public void CheckDelete()
    {
        //m_EnemyList.Clear();
        //return;




        //削除するエネミーを記憶
        for (int Cnt = 0; Cnt < m_EnemyList.Count; Cnt++)
        {
            if (m_EnemyList[Cnt] == null)continue;///////////////////////////////////////////////////////


            //スクリプトコンポーネント取得
            EnemyBase enemyCS = m_EnemyList[Cnt].GetComponent<EnemyBase>();
            
            if (enemyCS == null) Debug.Log("エネミースクリプトNULL"); continue;
            if (enemyCS.GetOwner() != null) Debug.Log("エネミーオーナーNULL"); continue;
            Debug.Log("エネミー削除リストに入れた");

            m_DeleteIdx.Add(Cnt);
        }

        //削除 
        if (m_DeleteIdx.Count == 0) return;
        for(int DCnt = m_DeleteIdx.Count - 1;DCnt >= 0; DCnt--)
        {
            m_EnemyList.RemoveAt(m_DeleteIdx[DCnt]);
            Debug.Log("エネミー削除完了");
        }
        m_DeleteIdx.Clear();



    }







}
