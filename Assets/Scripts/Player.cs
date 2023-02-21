using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    private Vector3 lastInteractDir;
    [SerializeField] LayerMask counterLayer;

    private ClearCounter selectedCounter;

    private bool isWalking;
    private void Start()
    {
        gameInput.OnInteractEvent += GameInput_OnInteract;
    }
    private void GameInput_OnInteract(object sender, EventArgs e)
    {
        Vector2 inputVector = gameInput.VectorInputNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, moveDir, out RaycastHit raycastHit, interactDistance, counterLayer))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                //has clear counter
                clearCounter.Interact();
            }
        }
    }
    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
        HandleInteractions();

    }
    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.VectorInputNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if(moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        if(Physics.Raycast(transform.position, moveDir, out RaycastHit raycastHit, interactDistance, counterLayer))
        {
            if(raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                //has clear counter

            }
        }
    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.VectorInputNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            //cannot move towards moveDir

            //Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                //can only move in Z direction
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    //can't move in any direction
                }
            }


        }

        if (canMove)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }
    public bool IsWalking()
    {
        return isWalking;
    }
}
