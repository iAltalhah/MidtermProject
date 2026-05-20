using UnityEngine;

public class HandBehave : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void OnEnable()
    {
        animator.Play("handMoving");
    }
}
