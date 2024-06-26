using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class AIController : MonoBehaviour
{
    public float moveSpeed;
    public TMP_InputField chatboxInputField;
    public GameObject door;

    private bool isMoving = false ;
    public bool get_is_moving()
    {
        return isMoving;
    }

    public static bool isMovingBis = false;

    private Vector2 input; 
    public Vector2 get_position()
    {
        return transform.position ;
    }
    
    private Animator animator;

    public LayerMask solidObjectsLayer;

    private void Awake(){
        animator = GetComponent<Animator>();
    }
    
    public void move(List<Parser.Goal> deplacements_) {
        /* if (!isMoving) {
             var targetPos = transform.position;
             targetPos.x += vector.x;
             targetPos.y += vector.y;*/
        StartCoroutine(Move(deplacements_));
    }

        //if (IsWalkable(targetPos))
        //{
        //{ yield return 
                    

       // }
        //Animation
        //animator.SetBool("isMoving", isMoving);
   // }
/*
    public void set_position(Vector3 vector) // attention ne fait plus rien
    {
        //if (!isMoving)
        
            var targetPos = transform.position;
            targetPos.x = vector.x;
            targetPos.y = vector.y;

           // if (IsWalkable(targetPos))
             //   StartCoroutine(Move(targetPos));
       // Move(targetPos);
        
    }*/

    private void Update()
    {

        // Check if any UI element is currently selected
        /*if (EventSystem.current.currentSelectedGameObject == chatboxInputField)
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
        if (isMovingBis)
        {
            input.x = -1;
            input.y = 0;

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
            isMovingBis = false;
        }

        animator.SetBool("isMoving", isMoving);*/
    }

    IEnumerator Move(List<Parser.Goal> deplacements_)
    {
    foreach (Parser.Goal move in deplacements_)
    {
        Vector3 targetPos = new Vector3() ;
        Vector2 vec_ = move.get_vec();
        int cat = move.get_cat();
        Door doorObj = door.GetComponent<Door>();

            if (IsWalkable(targetPos)){
                if (cat == 1)
                {
                    targetPos.x = transform.position.x + vec_.x;
                    targetPos.y = transform.position.y + vec_.y;
                    doorObj.Close();
                }
                else if (cat == 0)
                {
                    targetPos.x = vec_.x;
                    targetPos.y = vec_.y;
                    doorObj.Close();
                }
                else if (cat == 2)
                {
                    targetPos.y = vec_.y;
                    float x = transform.position.x;
                    if (x < -16)
                    {
                        targetPos.x = -16;
                    }
                    else if (x > 10)
                    {
                        targetPos.x = 10;
                    }
                    else
                    {
                        targetPos.x = x;
                    }
                    doorObj.Close();
                }
                else
                {
                    targetPos.x = vec_.x;
                    targetPos.y = vec_.y;
               
                }

                while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                    yield return null;
                }
                transform.position = targetPos;
               if (cat == 3)
                {
                    doorObj.Open();
                }
                
            }
    }
    
    }

    private bool IsWalkable(Vector3 targetPos){
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) != null){
            return false;
        }
        return true;
    }
}   