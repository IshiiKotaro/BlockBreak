using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*カメラを管理するクラス
画面揺れに関しては　EventManager経由で呼び出す*/	
public class CameraController : MonoBehaviour{

	Vector2 m_QuakeVec;

	//==============================
	//	セッター
	//==============================
	void SetAspect()
	{
		Camera cam = gameObject.GetComponent<Camera> ();

		// 理想の画面の比率
		float targetRatio = 9f / 16f;	
		// 現在の画面の比率
		float currentRatio = Screen.width * 1f / Screen.height;
		// 理想と現在の比率
		float ratio = targetRatio / currentRatio;

		//カメラの描画開始位置をX座標にどのくらいずらすか
		float rectX = (1.0f - ratio) / 2f;
		//カメラの描画開始位置と表示領域の設定
		cam.rect = new Rect (rectX, 0f, ratio, 1f);
	}


	public void SetQuakeDis(float _QuakeX,float _QuakeY)
	{
		m_QuakeVec.x = _QuakeX;
		m_QuakeVec.y = _QuakeY;
	}


	//
	void Start () {
		SetAspect ();
	}
	
	// Update is called once per frame
	void Update () {


		//画面揺れ
		QuakeUpdate();

	}


	void QuakeUpdate()
	{
		if (m_QuakeVec.magnitude < 0.01f)return;
		transform.position = new Vector3 (m_QuakeVec.x,m_QuakeVec.y,-10);
		m_QuakeVec *= -0.9f;

	}


}
