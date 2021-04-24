using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speedX = 2f;
    public float speedY = 5f;

    public float speedDrill = 2;

    private Vector3 _movementForce;

    public GameObject maker;


    void Awake()
    {
    }

    void Update()
    {
        Move();
    }

    private bool IsGrounded(out WorldTile tile)
    {
        tile = WorldGeneration.Instance.GetTile(transform.position.x, transform.position.y-1f);
        return tile != null; 
    }

    private void Move() {
        float moveX = Input.GetAxis("Horizontal") * speedX * Time.deltaTime;
        float moveY = 0;
        //Debug.Log("moveX: " + moveX);

        bool canMoveX = false;
        bool canMoveY = false;

        Vector2 drillDir = Vector2.zero;

        if (moveX != 0 && (transform.position.x + moveX) >= WorldGeneration.Instance.mapMinX && (transform.position.x + moveX) <= WorldGeneration.Instance.mapMaxX)
        {
            //Tile? 
            drillDir = moveX < 0 ? Vector2.left : Vector2.right;
            WorldTile tileNextX = WorldGeneration.Instance.GetTile(transform.position.x + drillDir.x, transform.position.y);
            if (tileNextX == null)
            {
                //can move
                canMoveX = true;
                drillDir = Vector2.zero;
            }
            else
            {
                //drill!
                if(Vector2.Distance(transform.position,tileNextX.transform.position) > 0.1f)
                {
                    drillDir = Vector2.zero;
                }
                //drillDir = moveX < 0 ? Vector2.left : Vector2.right;
            }
        }
        else
        {

        }


        if (!IsGrounded(out WorldTile tile))
        {
            //fall down
            moveY = -1 * speedY * Time.deltaTime;
            drillDir = Vector2.zero;
        }
        else
        {
            if (Input.GetAxis("Vertical") < 0)
            {
                drillDir = Vector2.down;
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            drillDir = Vector2.left;
        }

        //Do drill 
        if (drillDir != Vector2.zero)
        {
            maker.transform.position = new Vector3(transform.position.x + drillDir.x, transform.position.y + drillDir.y);
            Debug.Log($"Drill {transform.position.x} {transform.position.y}");
            WorldGeneration.Instance.DeleteTile(transform.position.x + drillDir.x, transform.position.y + drillDir.y);
        }
        else
        {
            transform.position += new Vector3(moveX,moveY);
        }

    }
}
