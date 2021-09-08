using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D myBody;
    Vector2 movement;

    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float attackDistance = 2.0f; //should be 0 or very small for melee enemies
    public float aggroRange;
    bool isAggro = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            isAggro = isAggro || Vector2.Distance(player.transform.position, gameObject.transform.position) <= aggroRange;

            if (isAggro)
            {
                int xDirection = player.transform.position.x < gameObject.transform.position.x ? -1 : 1;
                gameObject.transform.localScale = new Vector3(xDirection, 1, 1);
                movement.x = xDirection;
                movement.y = player.transform.position.y < gameObject.transform.position.y ? -1 : 1;
                
                if(!TargetInAttackRange(player.transform.position)) 
                    myBody.MovePosition(myBody.position + movement * moveSpeed * Time.fixedDeltaTime);
            }
        }
    }

    bool TargetInAttackRange(Vector2 target){
        return Vector2.Distance(myBody.position, target) <= attackDistance;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            IKillable target = collision.collider.GetComponent<IKillable>();
            if (target != null)
            {
                target.TakeDamage(1);
                Debug.Log("damage taken");
            }
            Destroy(gameObject);
        }
    }
}