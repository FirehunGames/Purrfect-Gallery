using UnityEngine;

public class catIdleAnimationSpeed : MonoBehaviour
{

    public float speed = 0f;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Speed", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
