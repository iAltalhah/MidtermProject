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
        isDropping = true;

        Debug.Log("Spider Dropped!");
    }

    void EnableAttack()
    {
        canAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canAttack) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("Spider Attacked Player!");
            gameManager.DamagePlayer();
            gameObject.SetActive(false);
        }
    }
}