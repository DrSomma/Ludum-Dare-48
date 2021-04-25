using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speedX = 2f;
    public float speedY = 5f;

    public float DigDamage = 0.5f;
    public float DigSpeed = 1f;

    public float speedDrill = 2;

    private Rigidbody2D _rigidbody2D;
    private Vector3 _movementForce;
    private Vector3 _drillDir;

    public GameObject maker;


    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    private void Move() {

        Vector3 newPos = transform.position + (Vector3.right * Input.GetAxis("Horizontal") * speedX * Time.deltaTime);
        _drillDir = Vector2.zero;

        if(Input.GetAxis("Horizontal") > 0)
        {
            _drillDir = Vector3.right;
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            _drillDir = Vector3.left;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            _drillDir = Vector3.down;
        }

        if(_drillDir != Vector3.zero)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _drillDir, 0.3f);

            if (hit.collider != null && hit.transform.tag != "Player")
            {
                float distance = Mathf.Abs(hit.point.y - transform.position.y);
                if(distance <= 0.2f)
                {
                    //Drill no move!
                    WorldTile tile = hit.collider.gameObject.GetComponent<WorldTile>();
                    tile.DigMe(DigDamage,DigSpeed);
                }
                else
                {
                    //Debug.Log(distance);
                    WorldTile.OnStopDigging();
                }
            }
            else
            {
                if(newPos.x < WorldGeneration.Instance.mapMinX)
                {
                    transform.position = new Vector3(WorldGeneration.Instance.mapMinX, transform.position.y);
                }else if(newPos.x > WorldGeneration.Instance.mapMaxX)
                {
                    transform.position = new Vector3(WorldGeneration.Instance.mapMaxX, transform.position.y);
                }
                else
                {
                    transform.position = newPos;
                    WorldTile.OnStopDigging();
                }
            }
        }
        else
        {
            WorldTile.OnStopDigging();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(transform.position, transform.position + _drillDir * 0.3f);
    }
}
