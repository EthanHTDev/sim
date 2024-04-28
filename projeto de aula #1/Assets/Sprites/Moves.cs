using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moves : MonoBehaviour
{
    // Start is called before the first frame update

    //private Vector3 moves;
    //public float meuY;
    //public float meuX;
    //public int sense;
    private Rigidbody2D rb;
    public float velocidade = 5f;
    public Animator movimentos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;
        movimentos = GetComponent<Animator>();
        //moves = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //animação de movimento
        if (Input.GetAxis("Horizontal") > 0) 
        {
            movimentos.SetBool("direita", true);
        }
        else
        {
            movimentos.SetBool("direita", false);
        }
        if(Input.GetAxis("Horizontal") < 0)
        {
            movimentos.SetBool("esquerda", true);
        }
        else
        {
            movimentos.SetBool("esquerda", false);
        }
        if (Input.GetAxis("Vertical") > 0) 
        {
            movimentos.SetBool("cima", true);
        }
        else
        {
            movimentos.SetBool("cima", false);
        }
        if(Input.GetAxis("Vertical") < 0)
        {
            movimentos.SetBool("baixo", true);
        }
        else
        {
            movimentos.SetBool("baixo", false);
        }

        //-------------------------------

        float meuX = Input.GetAxisRaw("Horizontal") * velocidade;
        float meuY = Input.GetAxisRaw("Vertical") * velocidade;


        rb.velocity = new Vector2(meuX, meuY);

        
      

        

    }
}
