using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVecSupport : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	

    /*矢印の回転
     _BallPos:
     _TapPos :*/
     public void Rotate(Vector3 _BallPos,Vector3 _TapPos)
    {
        //まず。矢印の座標を自機に合わせる。
        transform.position = new Vector3(_BallPos.x,_BallPos.y,_BallPos.z);



        //2ベクトルの内積計算
        float Dot = Vector3.Dot(_BallPos,_TapPos);

        //外積で右向きか左向きか求める




        //ダイレクトに角度に変換(180~-180)
        float Ang = (Dot - 1) * -1;
        Ang *= 90.0f;

        Ang -= transform.eulerAngles.z;





        //回転
        transform.Rotate(new Vector3(0.0f,0.0f, Ang));

    }




}
