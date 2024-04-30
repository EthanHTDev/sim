using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//importando a UI
using UnityEngine.UI;
//cenas ?
using UnityEngine.SceneManagement;

public class Moves : MonoBehaviour
{
    // Start is called before the first frame update

    //private Vector3 moves;
    //public float meuY;
    //public float meuX;
    //public int sense;
    //movimentação
    private Rigidbody2D rb;
    public float velocidade = 5f;
    //animação variavel
    public Animator movimentos;
    //barrinha HUD
    public float staminaInicial = 100f;
    public float taxaDeDecrementoStamina = 5f;
    public float staminaAtual;
    public Slider sliderStamina;


    void Start()
    {
        //movimentação
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;
        movimentos = GetComponent<Animator>();
        
        //stamina
        staminaAtual = staminaInicial;
        AtualiazarSliderStamina();

    }

    // Update is called once per frame
    void Update()
    {
        
       

        //animação de movimento
        if(Input.GetAxis("Horizontal") > 0)
        {
            movimentos.SetBool("direita", true);
        }
        else
        {
            movimentos.SetBool("direita", false);
        }
        if(Input.GetAxis("Vertical") > 0)
        {
            movimentos.SetBool("cima", true);
        }
        else
        {
            movimentos.SetBool("cima", false);
        }
        if(Input.GetAxis("Horizontal") < 0)
        {
            movimentos.SetBool("esquerda", true);
        }
        else
        {
            movimentos.SetBool("esquerda", false) ;
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            movimentos.SetBool("baixo", true);
        }
        else
        {
            movimentos.SetBool("baixo", false) ;
        }

        //-------------------------------
        //movimentação

        float meuX = Input.GetAxisRaw("Horizontal") * velocidade;
        float meuY = Input.GetAxisRaw("Vertical") * velocidade;

        


        rb.velocity = new Vector2(meuX, meuY);


        //--------------------------------

         if(meuX != 0 || meuY != 0)
        {
            staminaAtual -= taxaDeDecrementoStamina * Time.deltaTime;
            AtualiazarSliderStamina();
        }

    }
    //criando função propria da barrinha
    void AtualiazarSliderStamina()
    {
            sliderStamina.value = staminaAtual / staminaInicial;
        if(sliderStamina.value <= 0)
        {

            SceneManager.LoadScene(2);
        }
    }

    
}
