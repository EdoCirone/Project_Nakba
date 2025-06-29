using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform player;
    [SerializeField] private Animator anim;
    //public AudioClip hitSound;



    private LifeController life;
    private Rigidbody2D rb;
    private float x;
    private float y;
    private Vector2 dir;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        life = GetComponent<LifeController>();
        anim = GetComponent<Animator>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("TargetPoint");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            MoveTowardsTarget();
            //Animate();

        }


    }

    void MoveTowardsTarget()
    {
        Vector2 currentPos = transform.position;
        Vector2 targetPos = player.position;
        dir = (targetPos - currentPos).normalized;
        x = dir.x;
        y = dir.y;
        transform.position = Vector2.MoveTowards(currentPos, targetPos, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {

            collision.transform.SetParent(transform);
            //LifeController lifeController = collision.collider.GetComponent<LifeController>();

            //if (lifeController != null)
            //{

            //    lifeController.AddHp(-1);
            //}
        }

        //if (collision.collider.CompareTag("Bullet"))
        //{
        //    //Bullet bullet = collision.collider.GetComponent<Bullet>();
        //    if (bullet != null)
        //    {

        //        //AudioController.Play(hitSound, transform.position, 1);
        //        life.AddHp(-bullet.Damage);
        //    }
        //}
    }

    //private void Animate()
    //{
    //    float direction = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime).magnitude;
    //    bool isMoving = direction > 0.1f;
    //    bool isTurning = direction != 0;
    //    bool dead = !life.IsAlive;

    //    if (isTurning)
    //    {
    //        anim.SetFloat("x", x);
    //        anim.SetFloat("y", y);
    //    }

    //    if (isMoving)
    //    {
    //        anim.SetFloat("x", x);
    //        anim.SetFloat("y", y);
    //    }

    //    anim.SetBool("isMoving", isMoving);


    //}
}
