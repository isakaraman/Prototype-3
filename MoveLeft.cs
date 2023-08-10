using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float leftBound;

    private PlayerController playerControllerScript;

    void Start()
    {
        playerControllerScript =GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playerControllerScript.gameStarts==true)
        {
            MoveToLeft();
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

    void MoveToLeft()
    {
        if (playerControllerScript.gameOver == false)
        {
            if (Input.GetKey(KeyCode.LeftShift) && playerControllerScript.onGround==false)
            {
                speed = 17;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift) && playerControllerScript.onGround == false)
            {
                speed = 10;
            }
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }
}
