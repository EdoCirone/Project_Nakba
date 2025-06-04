using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private Animator animator;
    private TopDownMovement topDownMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        topDownMovement = GetComponent<TopDownMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Animation();
    }

    private void Animation()
    {
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
            return;
        }

        if (topDownMovement.Dir != Vector2.zero)
        {
            animator.SetBool("isMoving", true);
            animator.SetFloat("moveX", topDownMovement.Dir.x);
            animator.SetFloat("moveY", topDownMovement.Dir.y);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

    }
}
