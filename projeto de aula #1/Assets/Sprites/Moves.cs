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

    //testes

    public Vector3 testePo;

    //adicionar cena de game over
    public bool morte;

    //run
    public float CorridaInicial = 10f;
    private float CorridaAtual;
    public bool RunStamina;
    private bool run;
    private bool stamina;

    //colisao com a tag

    public float TaxaDeRecuperacao = 10f;

    //vida do player
    public int playerHealth = 100;
    public Slider lifeSlider;

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

        //teste

        testePo = new Vector3(2f, 3f, 5f);
        Debug.Log(testePo);

    }

    // Update is called once per frame
    void Update()
    {
        //corrida
        run = Input.GetKey(KeyCode.LeftShift);
        
        if (run && stamina == true)
        {
            velocidade = CorridaInicial;
        }
        else
        {
            velocidade = CorridaAtual;
        }
        

        
        //-------------------------------
        //movimentação

        float meuX = Input.GetAxisRaw("Horizontal") * velocidade;
        float meuY = Input.GetAxisRaw("Vertical") * velocidade;

        rb.velocity = new Vector2(meuX, meuY);
       
       iswalking = meuX != 0 || meuY != 0;
        if(meuX != 0 || meuY != 0) { 
       movimentos.SetFloat("MeuX", meuX);
       movimentos.SetFloat("MeuY", meuY);

        }

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

        // vida do jogador
        lifeSlider.value = playerHealth * 0.01f;

    }
    //criando função propria da barrinha
    void AtualiazarSliderStamina()
    {
        sliderStamina.value = staminaAtual /  staminaInicial;
        if(staminaAtual <= 1)
        {
            stamina = false;
        }
        else
        {
            stamina = true;
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

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        Debug.Log("dano recebido " + damage + " vida restante" + playerHealth);
        
        if(playerHealth <= 0)
        {
            Debug.Log("game over");
            SceneManager.LoadScene(1);
        }
    }


}


