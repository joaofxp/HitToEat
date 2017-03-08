﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

[SerializeField]
public class GameController : MonoBehaviour {

	public static GameController singleton;
    //LASERS
	public GameObject[] lasersLista;
	public GameObject[] powerUpsLista;
	public float 	laserEsperaDisparo;
	public float 	laserTempo;
	public bool  	laserPodeLigar;
    //POWERUPS
	public float 	powerUpPosicaoXMin;
	public float 	powerUpPosicaoXMax;
	public float	powerUpPosicaoZMin;
	public float 	powerUpPosicaoZMax;
    //JOGADORES
    public int jogadoresConfirmadosLista;
    public int numeroJogadores;
    public List<GameObject> jogadoresLista;
	public List<GameObject> meshPrefabLista;
	public List<Color> 		corLista;
    public List<int> meshJogadoresSelecionados;
	public List<int> texturaJogadoresSelecionados;
	public List<int> idJogadoresSelecionados;
    public List<Texture> texturas_alien;
    public List<Texture> texturas_homemGalinha;
    public List<Texture> texturas_zumbi;
    public List<List<Texture>> texturasLista; // lista para armazenar listas de texturas 
    //INPUTS
    public List<string> inputsPlayer1;
	public List<string> inputsPlayer2;
	public List<string> inputsPlayer3;
	public List<string> inputsPlayer4;
	public List<List<string>> Jogadores_INPUTS;
    //CONFIRMAR
    bool podeLigar;

	void Awake () 
	{
		//Gerenciamento da instancia do GameController
		if (singleton == null) {
			singleton = this;
		}
		else if (singleton != this)
		{
			Destroy (gameObject);
			Debug.Log("Objeto repetido!");
		}
		DontDestroyOnLoad(gameObject);
		//Encontrar os lasers existentes no level
		//Criar uma lista para guardar os controles dos jogadores
		Jogadores_INPUTS = new List<List<string>>();
		//Adiciona cada controle existente
		Jogadores_INPUTS.Add(inputsPlayer1);
		Jogadores_INPUTS.Add(inputsPlayer2);
		Jogadores_INPUTS.Add(inputsPlayer3);
		Jogadores_INPUTS.Add(inputsPlayer4);
        //Texturas
        texturasLista = new List<List<Texture>>();
        texturasLista.Add(texturas_zumbi);
        texturasLista.Add(texturas_homemGalinha);
        texturasLista.Add(texturas_alien);
        //Deixar em ordem alfabetica as meshs
        meshPrefabLista.Sort(
			delegate (GameObject meshPrefab1, GameObject meshPrefab2)
			{
				return meshPrefab1.name.CompareTo (meshPrefab2.name);
			}
		);

        texturasLista.Sort (
		    delegate (List<Texture> lista1, List<Texture> lista2) 
            {
			    return lista1.ToString().CompareTo (lista2.ToString ());
		    }
        );
		
		//Define que o numero de jogadores e 0
		numeroJogadores = 0;
	}

	void Start ()
	{
        ChecarCena();
    }

	void Update()
	{
		if (Input.GetButtonDown("Jogador_1_Comecar"))
		{
//            ReiniciarJogo();
		}
	}

	public void TrocaCena()
	{
		SceneManager.LoadScene(1);
        Invoke("ChecarCena", 3);
    }

    public void ReiniciarJogo()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void ChecarCena()
    {
        //Debug.Log("CHECK");
        ChecarJogadores();
        if (SceneManager.GetActiveScene().name == "CenaLevelCozinha" || SceneManager.GetActiveScene().name == "CenaLevelBanheiro")
        {
            lasersLista = GameObject.FindGameObjectsWithTag("Laser");
            //StartCoroutine(CriarNovoPowerUp());
            StartCoroutine(LaserLigar());
        }
        if (SceneManager.GetActiveScene().name == "CenaSelecaoDePersonagem")
        {
            jogadoresLista[0].GetComponentInChildren<SelecaoPersonagem>().podeParticipar = true;
            //jogadoresLista[0].GetComponentInChildren<SelecaoPersonagem>().modo.text = "Troca PRESONAGEM";
        }
    }

    public void ChecarJogadores()
    {
        jogadoresLista.Clear();
        jogadoresLista.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        //Deixar em ordem alfabetica a lista de jogadores
        jogadoresLista.Sort(
            delegate (GameObject Jogador1, GameObject Jogador2)
            {
                return Jogador1.name.CompareTo(Jogador2.name);
            }
        );
    }

	//COROUTINES
	public IEnumerator LaserLigar()
	{
        if (SceneManager.GetActiveScene().name == "CenaLevelCozinha" || SceneManager.GetActiveScene().name == "CenaLevelBanheiro")
        {
            yield return new WaitForSeconds(laserEsperaDisparo);
            int i = Random.Range(0, lasersLista.Length);
			if (!lasersLista[i].GetComponent<Laser>().Linha.enabled) {
				StartCoroutine(lasersLista[i].GetComponent<Laser>().DispararLaser(laserTempo));
			}
            yield return new WaitForSeconds(1 * Time.deltaTime);
            StartCoroutine(LaserLigar());
        }
	}
	/// <summary>
	/// Criar um power up na posicao x min e max, y ja predefinida e z min e max
	/// </summary>
	/// <returns>null</returns>
	public IEnumerator CriarNovoPowerUp()
	{
		yield return new WaitForSeconds(Random.Range(5,10));
		Instantiate(powerUpsLista[0], new Vector3(Random.Range(powerUpPosicaoXMin, powerUpPosicaoXMax),6,Random.Range(powerUpPosicaoZMin, powerUpPosicaoZMax)), Quaternion.identity);
		StartCoroutine(CriarNovoPowerUp());
	}
	/// <summary>
	/// Executa a acao do PowerUp
	/// </summary>
	/// <returns>null</returns>
	/// <param name="powerUpInstancia">Instancia do PowerUp</param>
	/// <param name="meuPowerUp">Tipo do PowerUp</param>
	/// <param name="jogadorInstancia">Jogador que pegou o PowerUp</param>
	/// <param name="powerUpDuracao">Duracao Do Efeito do PowerUp</param>
	public IEnumerator PowerUpAcao(PowerUp powerUpInstancia, PowerUp.powerUpType powerUpTipo, Jogador jogadorInstancia, float powerUpDuracao)
	{
		if (powerUpTipo == PowerUp.powerUpType.Agilidade && jogadorInstancia.puloForca == jogadorInstancia.puloForcaInicial)
		{
			jogadorInstancia.movimentoVelocidade = powerUpInstancia.powerUpAgilidadeMovimentoVelocidade;
			jogadorInstancia.puloForca = powerUpInstancia.powerUpAgilidadePuloForca;
			jogadorInstancia.powerUpAgilidadeMultiplicador *= 2;
			yield return new WaitForSeconds(powerUpDuracao);
			jogadorInstancia.movimentoVelocidade = jogadorInstancia.movimentoVelocidadeInicial;
			jogadorInstancia.puloForca = jogadorInstancia.puloForcaInicial;
			jogadorInstancia.powerUpAgilidadeMultiplicador /= 2;
		}
		yield return null;
	}
}
