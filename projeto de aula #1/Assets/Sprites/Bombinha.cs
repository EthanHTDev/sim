using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bombinha : MonoBehaviour
{

    public GameObject bombinha; //objeto
    public KeyCode inputKey = KeyCode.Space; //bot�o para colocar a bomba
    public float timeBomb = 3f; //tempo para explodir
    public int quantidadeBomb = 1; //quantidade de bombas
    private int bombRestantes; //bomba restantes no inventario


    //Esta fun��o � chamada quando o objeto se torna habilitado e ativo
    private void OnEnable()
    {
        bombRestantes = quantidadeBomb;
    }
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if(bombRestantes > 0 && Input.GetKeyDown(inputKey)) // se o seu invetario tiver mais bombas e apertar o espa�o ele ativa
        { 
            StartCoroutine(PlaceBomb());
        }
    }

    //criando uma courotine para fazer a bomba explodir e tambem dar um tempo pra cada explos�o
    private IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        GameObject bomb = Instantiate(bombinha, position, Quaternion.identity);
        bombRestantes--;

        yield return new WaitForSeconds(timeBomb);

        Destroy(bomb);
        bombRestantes++;
    }
}
