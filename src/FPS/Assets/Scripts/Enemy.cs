using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
 public float health = 100f;

 public float lookRadius = 100f;

    public Animator anim;

    public int damage = 10;


    public GameObject ThisObject;
    public monsterHolderScript myMonsterHolder;

 Transform target;
 NavMeshAgent agent;

//This gathers all variables upon the enemy loading in to the scene

    void Start ()
    {
        target = PlayerManager.instance.player.transform;

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        ThisObject = GetComponent<GameObject>();

        myMonsterHolder = ThisObject.GetComponent<monsterHolderScript>();
    
    }
    //This check to see if the player is within the specified distance of the player 
    //and if it is it will play the monsters running animation
    void Update ()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= 100)
        {
            agent.SetDestination(target.position);
                anim.SetBool ("isRunning", true);


            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
        }
    }


//will make the monster move towards the player and makes sure the monster
//is facing the player


    void FaceTarget ()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }



//Check to see if the monster has taken damage and when the monsters health 
//reaches 0 and calls the die function
    public void TakeDamage (float amount)
    {
        health -= amount; 
         if (health <= 0f)
             {

             Die();
             }
     }

//check to see if the enemy collides with the player and if it does it will damage the player
    void OnTriggerStay(Collider other){
        PlayerMovement FirstPersonPlayer = other.GetComponent<PlayerMovement>();
        if(FirstPersonPlayer != null){
            FirstPersonPlayer.TakeDamage(damage);

        }


    }

//removes the monster from the scene
    void Die ()
    {
 
            StartCoroutine(kill_enemy());

     myMonsterHolder.MonsterCount();   
    }

    //changes the enemies animation from running to dying

   IEnumerator kill_enemy() {
       
        anim.SetBool ("isRunning", false);
        anim.SetBool("isDead" , true);
            yield return new WaitForSeconds(1.5f); 
            gameObject.SetActive(false);

        }

        //displays the red mesh around the monster to display it's
        //looking radius
    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}
