using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//importando a UI
using UnityEngine.UI;
//cenas ?
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;

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

    bool iswalking = false;

    //barrinha HUD
    public float staminaInicial = 100f;
    public float taxaDeDecrementoStamina = 5f;
    public float staminaAtual;
    public Slider sliderStamina;

    //adicionar cena de game over
    public bool morte;

    //run
    public float CorridaInicial = 10f;
    private float CorridaAtual;
    public bool RunStamina;
    private bool run;

    //colisao com a tag

    public float TaxaDeRecuperacao = 10f;
    


    void Start()
    {
        //movimentação
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;
        
        //animação
        movimentos = GetComponent<Animator>();

        iswalking = false;
        
        //stamina
        staminaAtual = staminaInicial;
        AtualiazarSliderStamina();

        //corrida
        CorridaAtual = velocidade;

    }

    // Update is called once per frame
    void Update()
    {
        //corrida
        run = Input.GetKey(KeyCode.LeftShift);
        
        if (run)
        {
            velocidade = CorridaInicial;
        }
        else
        {
            velocidade = CorridaAtual;
        }
        //---------------------------------
        //animação de movimento
        //if(Input.GetAxis("Horizontal") > 0)
        // {
        //    movimentos.SetBool("direita", true);
        // }
        // else
        // {
        //     movimentos.SetBool("direita", false);
        // }
        // if(Input.GetAxis("Vertical") > 0)
        // {
        //     movimentos.SetBool("cima", true);
        // }
        // else
        //{
        //    movimentos.SetBool("cima", false);
        // }
        // if(Input.GetAxis("Horizontal") < 0)
        // {
        //     movimentos.SetBool("esquerda", true);
        // }
        //else
        // {
        //    movimentos.SetBool("esquerda", false) ;
        // }
        // if (Input.GetAxis("Vertical") < 0)
        // {
        //     movimentos.SetBool("baixo", true);
        // }
        // else
        // {
        //    movimentos.SetBool("baixo", false) ;
        // }

        
        //-------------------------------
        //movimentação

        float meuX = Input.GetAxisRaw("Horizontal") * velocidade;
        float meuY = Input.GetAxisRaw("Vertical") * velocidade;

        rb.velocity = new Vector2(meuX, meuY);
       
       iswalking = meuX != 0 || meuY != 0;

       movimentos.SetFloat("MeuX", meuX);
       movimentos.SetFloat("MeuY", meuY);
       movimentos.SetBool("is walking", iswalking);
        if (Input.GetKeyDown(KeyCode.Z))
        {
            movimentos.SetTrigger("attack");
        }
       


        


        //--------------------------------
        //stamina
        if (RunStamina)
        {
            if(run && meuX != 0 || run && meuY != 0)
            {
                staminaAtual -= taxaDeDecrementoStamina * Time.deltaTime;

                AtualiazarSliderStamina();
            }
                

            
        }
        else
        {
            if(meuX != 0 || meuY != 0)
        {
            staminaAtual -= taxaDeDecrementoStamina * Time.deltaTime;
            
                AtualiazarSliderStamina();
        }
        }
    }
    //criando função propria da barrinha
    void AtualiazarSliderStamina()
    {
        sliderStamina.value = staminaAtual /  staminaInicial;
        if (morte) { 
            if(sliderStamina.value <= 0)
            {

                SceneManager.LoadScene(1);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("mana-add")) { 
        while (staminaAtual <= 100f)
        {
            staminaAtual += TaxaDeRecuperacao;
            
        }
        }
    }


}


