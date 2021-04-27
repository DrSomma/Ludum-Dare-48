using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerMovement : MonoBehaviour
{
    public float Fuel = 1;
    public float FuelConsumption = 2f;
    public float FuelConsumptionDrill = 5f;

    public float speedX = 2f;
    public float speedY = 5f;
    public bool CanMove = true;

    private Vector3 _drillDir;

    public GameObject maker;

    private void Start()
    {
        Refuel();
    }

    public void Refuel()
    {
        Fuel = UpgradeManager.Instance.MaxFuel;
    }

    void Update()
    {
        if(CanMove)
            Move();
    }

    private Vector3 lastDir;
    private float curFuelConsumption;

    private void Move() {

        Vector3 newPos = transform.position + (Vector3.right * Input.GetAxis("Horizontal") * speedX * Time.deltaTime);
        _drillDir = Vector2.zero;

        if(Input.GetAxis("Horizontal") > 0)
        {
            _drillDir = Vector3.right;
            transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            _drillDir = Vector3.left;
            transform.rotation = Quaternion.identity;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            _drillDir = Vector3.down;
            transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
        }

        if(_drillDir != Vector3.zero)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _drillDir, 0.4f);

            if (hit.collider != null && hit.transform.tag != "Player")
            {
                float distance = Mathf.Abs(hit.point.y - transform.position.y);
                if(distance <= 0.1f || (Vector3.down == _drillDir && distance <= 0.3f))
                {
                    DoDrilling(hit);
                }
                else
                {
                    //Debug.Log(distance);
                    WorldTile.OnStopDigging();
                }
            }
            else
            {
                DoMovement(newPos);
            }
        }
        else
        {
            ResetSprite();
        }

        Fuel -= curFuelConsumption * Time.deltaTime;

        lastDir = _drillDir;

        //Check if fule 
        CheckGameOver();
    }

    private void ResetSprite()
    {
        //transform.rotation = Quaternion.identity;
        if (lastDir == Vector3.down)
        {
            transform.rotation = Quaternion.identity;
        }
        WorldTile.OnStopDigging();
        curFuelConsumption = FuelConsumption;
    }

    private void DoMovement(Vector3 newPos)
    {
        if (newPos.x < WorldGeneration.Instance.mapMinX)
        {
            transform.position = new Vector3(WorldGeneration.Instance.mapMinX, transform.position.y);
        }
        else if (newPos.x > WorldGeneration.Instance.mapMaxX)
        {
            transform.position = new Vector3(WorldGeneration.Instance.mapMaxX, transform.position.y);
        }
        else
        {
            transform.position = newPos;
            //transform.rotation = Quaternion.identity;
            curFuelConsumption = FuelConsumption;
            WorldTile.OnStopDigging();
        }
    }

    private void DoDrilling(RaycastHit2D hit)
    {
        //Drill no move!
        //can dig
        WorldTile tile = hit.collider.gameObject.GetComponent<WorldTile>();
        tile.DigMe(UpgradeManager.Instance.DrillSpeedMultiplier);
        curFuelConsumption = FuelConsumptionDrill;
    }

    private void CheckGameOver()
    {
        if(Fuel <= 0)
        {
            //Reset Player 
            Refuel();
            transform.position = new Vector2(0, 2.5f);
            WorldGeneration.Instance.GenerateRndMap();
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(transform.position, transform.position + _drillDir * 0.4f);
    }
}
