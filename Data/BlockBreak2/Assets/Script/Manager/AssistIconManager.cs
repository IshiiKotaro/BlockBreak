using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistIconManager :SingletonMonoBehaviour<AssistIconManager>
{
	public GameObject[] m_AssistIconPrefab;

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

	//======================================
	//	ゲッター
	//======================================

	public int GetPrefabListSize(){ return m_AssistIconPrefab.Length; }


		
	void Update () {
		
	}


	public void Create(int _Id,Vector3 _Pos,GameObject _Owner)
	{
		//上限下限チェック


		//インスタンス生成
		GameObject assistIcon = (GameObject)Instantiate(
			m_AssistIconPrefab[_Id],
			_Pos,
			Quaternion.identity
		);


		AssistIconBase assistIconCS = assistIcon.GetComponent<AssistIconBase>();


		//親子関係とオーナーの設定
		assistIcon.transform.parent = _Owner.transform;

		if (assistIconCS == null)Debug.Log("アシストアイコンNULL");return;
		assistIconCS.SetIdAndOwner(_Id,_Owner);
	}


}
