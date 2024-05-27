using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro.EditorUtilities;
using TreeEditor;
using UnityEngine;
using UnityEngine.Animations;



public class Testes : MonoBehaviour
{

    public Transform targetA ;
    public Transform targetB ;

    private Transform targertOfficial;
    public int velocidade;

    public SpriteRenderer sr;

    public Animator movimento;

    //a partir daqui eu não faço ideia

    private Coroutine attackCoroutine;

    //vida do inimigo

    private int vidaInimigo = 50;


 

    // Start is called before the first frame update
    void Start()
    {

        targertOfficial = targetA;
        movimento = GetComponent<Animator>();
        Debug.Log("vida do inimigo" + vidaInimigo);

    }

    // Update is called once per frame
    void Update()
    {

        if (targertOfficial == targetA && transform.position == targetA.position) 
        {
            targertOfficial = targetB;
            
            
        }
        if (targertOfficial == targetB && transform.position == targetB.position)
        {
            targertOfficial = targetA;
        }

        
        transform.position = Vector2.MoveTowards(transform.position, targertOfficial.position, velocidade * Time.deltaTime);
        


        if(transform.position.x > targertOfficial.position.x)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }

        movimento.SetBool("isWalking", true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("zona"))
        {

        }

        Moves player = other.GetComponent<Moves>();

        if (player == null)
        {
            player = other.GetComponent<Moves>();
        }

        if (player == null)
        {
            player = other.GetComponentInParent<Moves>();
        }
        if (player != null)
        {
            if (attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(AttackPlayer(player));
            }
        }
        else
        {
            Debug.LogWarning("player nao enontrado no objeto com a tag ZoneAttack");
        }

        if (other.CompareTag("attackzone"))
        {
            EnemyTakeDamage(vidaInimigo);
        }

    }

    

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("zona"))
        {
            Debug.Log("inimigo saiu da zona de attack");
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }
    }

    public void EnemyTakeDamage(int damage)
    {
        vidaInimigo -= damage;

        if (vidaInimigo <= 0)
        {
            //Destroy(gameObject);
        }
    }

    private IEnumerator AttackPlayer(Moves player)
    {
        player.TakeDamage(10);
        movimento.SetTrigger("attack");
        Debug.Log("inimgo atacando");
        yield return new WaitForSeconds(1);
    }

   

}











