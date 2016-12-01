using UnityEngine;
using System.Collections;

public class BreakUponPunch : MonoBehaviour {

	public GameObject VersaoQuebrada;
	public GameObject VersaoIntacta;

	public float MinimumForce = 2.0f;


	void DestroyObject(){

			VersaoIntacta.SetActive(false);
			VersaoQuebrada.SetActive(true);
	}

	void OnCollisionEnter ( Collision other ){


		Rigidbody Rb = other.transform.gameObject.GetComponent<Rigidbody>();

		if(Rb.velocity.x > MinimumForce || Rb.velocity.y > MinimumForce || Rb.velocity.z > MinimumForce || Rb.velocity.x < -MinimumForce || Rb.velocity.y < -MinimumForce || Rb.velocity.z < -MinimumForce){
		//if(Rb.velocity.y < 0.0f){
			DestroyObject();
		}
	}

	void OnTriggerStay(Collider other){

		if(other.transform.tag == "SocoDetector"){
			if(other.transform.parent.gameObject.GetComponent<Jogador>().socoEstaSocando == true){
				DestroyObject();
			}
		}
	}
}