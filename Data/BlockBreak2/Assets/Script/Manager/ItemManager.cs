using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ItemManager : SingletonMonoBehaviour<ItemManager>
{

    public GameObject[] m_ItemPrefab;


    List<GameObject> m_ItemList;

    //==============================
    //ゲッター
    //==============================

    public int GetListSize() { return m_ItemList.Count; }



    //==============================
    //関数
    //==============================

    public void Awake()
    {
        if(this != GetInstance)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }



    private ItemManager()
    {
        m_ItemList = new List<GameObject>();
    }


    /*アイテム作成
    _Id: 作成するアイテムのID
    _Pos:作成するアイテムの座標*/
    public void Create(int _Id, Vector3 _Pos)
    {
        //インスタンス生成
        GameObject item = (GameObject)Instantiate(
            m_ItemPrefab[_Id],
            _Pos,
            Quaternion.identity
            );
        item.transform.parent = this.transform;


        m_ItemList.Add(item);

       // Debug.Log("ItemList:" + m_ItemList.Count);
       // Debug.Log("アイテム生成");



    }


    public void CheckDelete()
    {


        
        foreach (GameObject item in m_ItemList)
        {
            if(item == null)
            {
                Debug.Log("アイテムNULL");
                continue;
            }


            //CSコンポーネント取得
            ItemBase itemCS = item.GetComponent<ItemBase>();


            if (itemCS.GetIsDelete() == false) continue;



            item.SetActive(false);





        }
    }











}
