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

    private Vector3 _drillDir;
    private SpriteRenderer _spriteRenderer;

    public GameObject maker;


    void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
            _spriteRenderer.flipX = true;
            transform.rotation = Quaternion.identity;
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            _drillDir = Vector3.left;
            _spriteRenderer.flipX = false;
            transform.rotation = Quaternion.identity;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            _drillDir = Vector3.down;
            transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
            _spriteRenderer.flipX = false;
        }

        if(_drillDir != Vector3.zero)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _drillDir, 0.4f);

            if (hit.collider != null && hit.transform.tag != "Player")
            {
                float distance = Mathf.Abs(hit.point.y - transform.position.y);
                if(distance <= 0.1f || (Vector3.down == _drillDir && distance <= 0.25f))
                {
                    //Drill no move!
                    WorldTile tile = hit.collider.gameObject.GetComponent<WorldTile>();
                    tile.DigMe(DigDamage,DigSpeed);
                }
                else
                {
                    Debug.Log(distance);
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
                    transform.rotation = Quaternion.identity;
                    WorldTile.OnStopDigging();
                }
            }
        }
        else
        {
            transform.rotation = Quaternion.identity;
            //_spriteRenderer.flipX = false;
            WorldTile.OnStopDigging();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(transform.position, transform.position + _drillDir * 0.4f);
    }
}
