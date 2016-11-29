using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraCenter : MonoBehaviour {

	public List<GameObject> playerLista;
	Vector3 midVector;
	public float zoomMinimo;
	public float zoomMaximo;
	public float zoomVelocidade;
	public float zoomMultiplicador;

	void Update () 
	{
		Vector3 somaPosicoesPlayers = new Vector3();
		foreach (GameObject player in playerLista) 
		{
			somaPosicoesPlayers += player.transform.position;
		}
		//Calculo de ponto medio e a distancia do zoom
		Vector3 pontoMedio = somaPosicoesPlayers / 3f;
		float distancia = -somaPosicoesPlayers.magnitude;
		//Verificar os limites da camera
		if (distancia < zoomMinimo)
		{
			distancia = zoomMinimo;
		}
		if (distancia > zoomMaximo)
		{
			distancia = zoomMaximo;
		}
		//Movimentacao e zoom da camera
		Vector3 cameraDestino = pontoMedio - transform.forward * distancia * zoomMultiplicador;
		transform.position = Vector3.Slerp(transform.position, cameraDestino, zoomVelocidade);
		// Snap quando estiver perto para evitar um efeito estranho do slerp
		if ((cameraDestino - transform.position).magnitude <= zoomVelocidade)
		{
			transform.position = cameraDestino;
		}

	}
}
