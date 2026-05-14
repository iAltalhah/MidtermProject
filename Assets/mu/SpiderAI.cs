using UnityEngine;
using UnityEngine.AI;

public class SpiderAI : MonoBehaviour
{
    public Transform player;

    public Transform dropPoint;

    public float dropSpeed = 3f;

    private NavMeshAgent agent;
    public SkinnedMeshRenderer spiderRenderer;

    private bool isDropping = false;
    private bool canChase = false;
    private bool canAttack = false;

    [SerializeField] GameManager gameManager;
    [SerializeField] int spiderDamage = 30;
    [SerializeField] Animator animator;

    [SerializeField] AudioSource spiderSFX;

    [Header("Spider Lifetime")]
    [SerializeField] private float destroyIfNoPlayerTime = 10f;
    private bool destroyTimerStarted = false;

    void Start()
    {
        spiderRenderer.enabled = false;

        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
    }

    void Update()
    {
        // نزول العنكبوت
        if (isDropping)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                dropPoint.position,
                dropSpeed * Time.deltaTime
            );

            // إذا وصل الأرض
            if (Vector3.Distance(transform.position, dropPoint.position) < 0.1f)
            {
                isDropping = false;

                agent.enabled = true;

                canChase = true;

                Invoke(nameof(EnableAttack), 1f);

                StartDestroyTimer();
            }
        }

        // المطاردة
        if (canChase)
        {
            agent.SetDestination(player.position);
        }
    }

    public void StartDrop()
    {
        spiderRenderer.enabled = true;
        animator.enabled = true;
        isDropping = true;
        spiderSFX.Play();

        Debug.Log("Spider Dropped!");
    }

    void EnableAttack()
    {
        canAttack = true;
    }

    void StartDestroyTimer()
    {
        if (destroyTimerStarted) return;

        destroyTimerStarted = true;

        Invoke(nameof(DestroySpider), destroyIfNoPlayerTime);
    }

    void DestroySpider()
    {
        Debug.Log("Spider did not find the player. Destroying itself.");

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("Spider Attacked Player!");

            CancelInvoke(nameof(DestroySpider));

            gameManager.DamagePlayer(spiderDamage);

            Destroy(gameObject);
        }
    }
}