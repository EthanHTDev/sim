using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Transform waypointA;
    public Transform waypointB;
    public float movimentSpeed = 1f;
    private Animator animator;
    private bool isWalking = false;
<<<<<<< Updated upstream

    private Rigidbody2D rb;
=======
    public Rigidbody2D rb;
>>>>>>> Stashed changes
    private Vector3 scale;
    private Transform currentTarget;

    private Coroutine attackCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); //pegando o hud do animator
        rb = GetComponent<Rigidbody2D>(); //pengando o hud do rigidbody2D
        currentTarget = waypointA; //colocando as infirmaçoes do waypointA e transferindo para o curretTarget
        scale = transform.localScale; //
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget() 
    {
        Vector3 curTargetHorizontal = new Vector2(currentTarget.position.x, transform.position.y);
        Vector2 direction = (curTargetHorizontal - transform.position).normalized;

        transform.position += (Vector3)direction * movimentSpeed * Time.deltaTime;

        if(Vector2.Distance(curTargetHorizontal, transform.position) <= 0.2f)
        {
            SwitchTarget();
        }

        UpdateAnimation();
    }

    private void SwitchTarget()
    {
        if(currentTarget == waypointA)
        {
            currentTarget = waypointB;
            Flip();
        }
        else
        {
            currentTarget = waypointA;
            transform.localScale = scale;
        }
    }

    private void UpdateAnimation()
    {
        isWalking = (Vector2.Distance(transform.position, currentTarget.position) > 8.1f);
        animator.SetBool("isWalking", isWalking);
    }

    private void Flip()
    {
        Vector3 FlippedScale = scale;
        FlippedScale.x *= -1;
        transform.localScale = FlippedScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("zona")) 
        {
           
        }

        Moves  player = collision.GetComponent<Moves>();

        if (player == null)
        {
            player = collision.GetComponentInParent<Moves>();
        }
        if (player != null)
        {
            if(attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(AttackPlayer(player));
            }
        }
        else
        {
            Debug.LogWarning("player nao enontrado no objeto com a tag ZoneAttack");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("zona"))
        {
            Debug.Log("inimigo saiu da zona de attack");
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        } 
    }

    private IEnumerator AttackPlayer(Moves player)
    {
        player.TakeDamage(10);
        animator.SetTrigger("attack");
        Debug.Log("inimgo atacando");
        yield return new WaitForSeconds(1);
    }
}
