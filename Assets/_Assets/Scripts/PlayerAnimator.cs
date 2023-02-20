using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_Walking = "IsWalking";
    private Animator animator;


    [SerializeField]     private Player player;  
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        animator.SetBool(IS_Walking, player.IsWalking());
    }
}
