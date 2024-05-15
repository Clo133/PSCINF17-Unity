using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public TMP_InputField chatboxInputField;
    private bool isMoving;
    private Vector2 input;
    public Vector2 get_position()
    {
        return transform.position ;
    }

    private Animator animator;

    public LayerMask solidObjectsLayer;
    public LayerMask doorLayer;

    private void Awake(){
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

        // Check if any UI element is currently selected
        if (chatboxInputField.isActiveAndEnabled)
        {
            // UI element is selected, do not process other inputs
            return;
        }

        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {

                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
            }
        }

        animator.SetBool("isMoving", isMoving);
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        if(targetPos.x > 14.5){
            SceneManager.LoadScene(2);
        }
        isMoving = false;
    }

    private bool IsWalkable(Vector3 targetPos){
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) != null || Physics2D.OverlapCircle(targetPos, 0.2f, doorLayer) != null)
        {
            return false;
        }
        return true;
    }
}