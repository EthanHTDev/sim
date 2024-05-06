using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class TestePorta : MonoBehaviour
{
    //variavel animação
    public Animator animacao;
    
    void Start()
    {
        
    }

    
    void Update()
    {
       
    }

    //detectar o player dentro da colisão
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            animacao.SetBool("aberto", true);
        }
        
    }

    //detectar o player fora da colisão
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animacao.SetBool("aberto", false);
        }
    }
}
