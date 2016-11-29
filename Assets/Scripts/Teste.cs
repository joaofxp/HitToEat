using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Teste : MonoBehaviour {

	Text m_Text;

	void Start()
	{
		m_Text = GetComponent<Text>();
		StartCoroutine(Contagem());
	}

	public IEnumerator Contagem()
	{
		int i = 5;
		do {
			m_Text.text = i.ToString();
			yield return new WaitForSeconds(1);
			i--;
		} while (i >= 0);
		GameController.singleton.TrocaCena();
		yield return null;
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