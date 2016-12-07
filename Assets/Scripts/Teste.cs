using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Teste : MonoBehaviour {

	public static Teste cFollow;
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;
	public float MidX;
	public float MidY;
	public float MidZ;
	public Transform target1;
	public Transform target2;
	public Vector3 Midpoint;
	public Vector3 distance;
	public float camDistance;
	public float CamOffset;
	public float bounds;

	void Awake ()
	{
		//checking if there's already a game manager
		if(cFollow == null)
		{
			DontDestroyOnLoad (gameObject);    
			cFollow = this;
		}
		else if(cFollow != this)
		{
			Destroy(gameObject);
		}    
	}

	// Use this for initialization
	void Start () 
	{
		camDistance = 10.0f;
		bounds = 12.0f;
	}

	// Update is called once per frame
	void Update () 
	{
		distance = target1.position - target2.position;
		if(camDistance >= 19.0f)
			camDistance = 19.0f;
		if (camDistance <= 10.0f)
			camDistance = 10.0f;
		if(distance.x < 0)
			distance.x = distance.x * -1;
		if(distance.z < 0)
			distance.z = distance.z * -1;
		if(target1.position.x < (transform.position.x -bounds))
		{
			Vector3 pos = target1.position;
			pos.x =  transform.position.x -bounds;
			target1.position = pos;
		}
		if(target2.position.x < (transform.position.x -bounds))
		{
			Vector3 pos = target2.position;
			pos.x =  transform.position.x -bounds;
			target2.position = pos;
		}
		if(target1.position.x > (transform.position.x +bounds))
		{
			Vector3 pos = target1.position;
			pos.x =  transform.position.x +bounds;
			target1.position = pos;
		}
		if(target2.position.x > (transform.position.x +bounds))
		{
			Vector3 pos = target2.position;
			pos.x =  transform.position.x +bounds;
			target2.position = pos;
		}
		if(distance.x > 15.0f)
		{
			CamOffset = distance.x * 0.3f;
			if(CamOffset >=8.5f)
				CamOffset = 8.5f;
		}else if(distance.x < 14.0f)
		{
			CamOffset = distance.x * 0.3f;
		}else if( distance.z < 14.0f)
		{
			CamOffset = distance.x * 0.3f;
		}
		MidX = (target2.position.x + target1.position.x) /2; 
		MidY = (target2.position.y + target1.position.y) /2;
		MidZ = (target2.position.z + target1.position.z) /2;
		Midpoint = new Vector3 (MidX, MidY, MidZ);
		if (target1)
		{
			Vector3 point = Camera.main.WorldToViewportPoint(Midpoint);
			Vector3 delta = Midpoint - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, camDistance + CamOffset)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}

	}
}

//	void Update() {
////		Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
////		Debug.DrawRay(transform.position, forward, Color.green);
////
////		Ray ray = GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
////		if (Physics.Raycast(ray, out hit))
////		{
////		}
//
//		RaycastHit hit;
//		if ( Physics.Raycast( transform.position, transform.forward, out hit, 100.0f ) )
//		{   
//			Debug.Log(hit.transform.name);
//		}
//
//		Debug.DrawRay(transform.position, transform.forward, Color.green);
//	}
////
////	void OnCollisionEnter(Collision collision) {
////		foreach (ContactPoint contact in collision.contacts) {
////			Debug.DrawRay(contact.point, contact.normal, Color.green, 2, false);
////		}
////	}

//	{
//		public bool flag;
//		public int i = 1;
//	}
//
//[CustomEditor(typeof(Teste))]
//public class MyScriptEditor : Editor
//{
//	override public void OnInspectorGUI()
//	{
//		Teste Teste = target as Teste;
//
//		Teste.flag = GUILayout.Toggle(Teste.flag, "Flag");
//
//		if(Teste.flag){
//			Teste.i = EditorGUILayout.IntSlider("I field:", Teste.i , 1 , 100);
//		}
//
//	}
//}