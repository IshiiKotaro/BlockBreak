using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlockManager : SingletonMonoBehaviour<BreakBlockManager> {

	public GameObject m_pBreakBlock;


	public void Awake(){
		if (this != GetInstance)
		{
			Destroy(this);
			return;
		}
		DontDestroyOnLoad(this.gameObject);
	}


	public void Create(int _Id,Vector3 _Pos)
	{
		//インスタンス生成
		GameObject breakBlock = (GameObject)Instantiate(
			m_pBreakBlock,
			_Pos,
			Quaternion.identity
		);

		BreakBlock breakCS = breakBlock.GetComponent<BreakBlock> ();
		if (breakCS == null)return;

		breakCS.Init (_Id);

	}


}
