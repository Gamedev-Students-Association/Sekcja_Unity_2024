using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temporaryplayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rigidBody;

    private Vector3 input;

    private void Update()
    {
        float inputx = Input.GetAxis("Horizontal");
        float inputy = Input.GetAxis("Vertical");

        input = new Vector3(inputx, inputy, 0);
        transform.position += input * Time.deltaTime * moveSpeed;
    }

        private void FixedUpdate()
    {
        Vector3 move = input * moveSpeed * Time.fixedDeltaTime;
        rigidBody.linearVelocity = new Vector2(move.x, rigidBody.linearVelocity.y);

        rigidBody.MovePosition(transform.position + move);
    }

}
