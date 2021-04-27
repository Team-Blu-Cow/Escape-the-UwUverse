using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UwUverse;

public class Player : GridEntity
{
    public static event Action player_died;

    private Vector2Int m_shotDirection = Vector2Int.zero;
    private MasterInput m_input;

    private bool m_hasShot = false;

    public int health = 2;
    public int maxHealth = 3;
    public int shotCooldown = 3;

    public float maxStepTime = 0.1f;
    public float stepTime = 0;

    private void Awake()
    {
        m_gridRef = GameObject.Find("Grid").GetComponent<TileGrid>();

        if (m_gridRef == null)
        {
            Debug.LogError("No grid could be found in scene, wrong name in hierarchy maybe?");
        }

        stepTime = 0;

        m_input = new MasterInput();
        m_input.PlayerMovement.Move.performed += ctx => BeginStep(new Vector2Int((int)ctx.ReadValue<Vector2>().x, (int)ctx.ReadValue<Vector2>().y));//Move(new Vector2Int((int)ctx.ReadValue<Vector2>().x, (int)ctx.ReadValue<Vector2>().y));
        m_input.PlayerMovement.Idle.performed += ctx => BeginStep(Vector2Int.zero);
        m_input.PlayerShoot.Direction.performed += ctx => SetShot(new Vector2Int((int)ctx.ReadValue<Vector2>().x, (int)ctx.ReadValue<Vector2>().y));
        m_input.PlayerShoot.Mouse.performed += ctx => SetShot(DirectionFromMouse());
        m_input.PlayerShoot.Undo.performed += ctx => UndoShot();
    }

    private void OnDestroy()
    {
        GameController.StepController().RemoveEntity();
        GameController.StepController().StepEvent -= Move;
    }

    // Start is called before the first frame update
    private void Start()
    {
        GameController.StepController().StepEvent += Move;
        GameController.StepController().AddEntity();

        CurrentNode = m_gridRef.GetNearestNode(transform.position);
        CurrentNode.AddObject(gameObject);
    }

    private void OnEnable()
    {
        m_input.Enable();
    }

    private void OnDisable()
    {
        m_input.Disable();
    }

    public void BeginStep(Vector2Int direction)
    {
        if (GameController.Instance.isPaused)
            return;

        Direction = direction;
        if (stepTime > maxStepTime)
        {
            TargetNode = CurrentNode.GetNeighbour(Direction);

            //hit logic
            // if (true)
            // {
            //     Hit(/*hit obj*/null, 0);
            // }

            //Shooting logic
            if (TargetNode != null && !TargetNode.isWall && !TargetNode.isHole)
            {
                HitDetectionLogic();
                ShootLogic();

                GameController.StepController().ApplyMove();
                GameController.Instance.stepController.BeginStep();
                stepTime = 0;
            }
        }
    }

    private void HitDetectionLogic()
    {
        const int CheckNum = 4;
        // get node offsets
        Vector2Int[] offsets = new Vector2Int[CheckNum];
        if (Direction != Vector2Int.zero)
        {
            offsets[0] = new Vector2Int(Direction.x - Direction.y, Direction.x + Direction.y);
            offsets[1] = new Vector2Int(Direction.x * 2, Direction.y * 2);
            offsets[2] = new Vector2Int(Direction.x + Direction.y, Direction.y - Direction.x);
            offsets[3] = Direction;
        }
        else
        {
            //offsets = new Vector2Int[CheckNum];
            offsets[0] = Vector2Int.left;
            offsets[1] = Vector2Int.right;
            offsets[2] = Vector2Int.down;
            offsets[3] = Vector2Int.up;
        }
        // get nodes at offsets
        GridNode[] nodes = new GridNode[CheckNum];
        nodes[0] = CurrentNode.GetNeighbour(offsets[0]);
        nodes[1] = CurrentNode.GetNeighbour(offsets[1]);
        nodes[2] = CurrentNode.GetNeighbour(offsets[2]);
        nodes[3] = CurrentNode.GetNeighbour(offsets[3]);

        // get danger vectors for each node
        Vector2Int[] dangerVectors = new Vector2Int[CheckNum];
        if (Direction != Vector2Int.zero)
        {
            dangerVectors[0] = new Vector2Int(Direction.y, -Direction.x);
            dangerVectors[1] = new Vector2Int(-Direction.x, -Direction.y);
            dangerVectors[2] = new Vector2Int(-Direction.y, Direction.x);
            dangerVectors[3] = dangerVectors[1];
        }
        else
        {
            dangerVectors[0] = Vector2Int.right;
            dangerVectors[1] = Vector2Int.left;
            dangerVectors[2] = Vector2Int.up;
            dangerVectors[3] = Vector2Int.down;
        }

        // check nodes for bullets with certain velocity
        GameObject[] bullets = new GameObject[CheckNum];
        GameObject[] enemies = new GameObject[CheckNum];

        for (int i = 0; i < CheckNum; i++) // add per object collision logic here
        {
            if (nodes[i].HasObjectOfType<bullet>(ref bullets[i]))
            {
                if (bullets[i] != null && (bullets[i].GetComponent<bullet>().m_direction == dangerVectors[i]))
                {
                    Hit(bullets[i], 1);
                }
            }
            if (nodes[i].HasObjectOfType<EnemyController>(ref enemies[i]))  // TODO @Adam: change BlockingEnemy
            {                                                               // to correct type when not cripplingy
                                                                            // tired
            
                

                if (enemies[i] != null && (enemies[i].GetComponent<GridEntity>().Direction == dangerVectors[i]))
                {
                    if (i == 3)//|| (i == 1  && Direction == -enemies[i].GetComponent<GridEntity>().Direction))
                    {
                        TargetNode = CurrentNode.GetNeighbour(dangerVectors[i]);
                    }
                    else
                    {
                        TargetNode = CurrentNode;
                    } 

                    Hit(enemies[i], 1);
                }
                else if(enemies[i] != null && (enemies[i].GetComponent<GridEntity>().Direction == Vector2Int.zero) && i == 3)
                {
                    TargetNode = CurrentNode;
                    Hit(enemies[i], 1);
                }
            } // TODO @Adam: implement this once direction can be retrived from enemies,
            //  //this logic should work out of the box once this is done
        }
    }

    private void ShootLogic()
    {
        if (m_hasShot)
        {
            // shoot if: node had no bullets already, wont be occupied by the player or a wall next step
            if (m_shotDirection == Direction && !CurrentNode.GetNeighbour(m_shotDirection).HasObjectOfType<bullet>())
                Shoot(CurrentNode.GetNeighbour(m_shotDirection));
            else if (!CurrentNode.GetNeighbour(m_shotDirection).HasObjectOfType<bullet>() && !CurrentNode.HasObjectOfType<bullet>())
                Shoot(CurrentNode);
            m_hasShot = false;
        }
    }

    private void Move()
    {
        CurrentNode.RemoveObject(gameObject);
        CurrentNode = TargetNode;
        CurrentNode.AddObject(gameObject);
        LeanTween.move(gameObject, m_gridRef.GridCoordToWorldCoord(CurrentNode.position), 0.1f);
    }

    public override void Hit(GameObject obj, int damage)
    {
        //base.Hit(obj, damage);

        // object specific
        if (obj != null && obj.GetComponent<bullet>() != null)
        {
            obj.GetComponent<bullet>().BulletDestroy();
        }

        health -= damage;
        //Debug.Log("Hit");
        if (health < 1)
        {
            player_died?.Invoke();
            /*die();*/
        }
    }

    private void SetShot(Vector2Int in_direction)
    {
        if (!m_hasShot)
            m_hasShot = true;

        m_shotDirection = in_direction;
    }

    private void UndoShot()
    {
        m_hasShot = false;
        m_shotDirection = Vector2Int.zero;
    }

    private void Shoot(GridNode in_Node)
    {
        GameObject bullet = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/bullet"), Vector3.zero, Quaternion.identity);
        bullet.GetComponent<bullet>().createBullet(in_Node, m_shotDirection);
    }

    private Vector2Int DirectionFromMouse()
    {
        Vector2 directionf = GameController.Instance.camera.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
        directionf.Normalize();
        Vector2Int directioni = Vector2Int.RoundToInt(directionf);
        return directioni;
    }

    private void Update()
    {
        stepTime += Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        //         Vector2Int direction = Vector2Int.zero;
        //
        //         Vector2Int[] offsets = new Vector2Int[4];
        //
        //         offsets[0] = Vector2Int.right;
        //         offsets[1] = Vector2Int.left;
        //         offsets[2] = Vector2Int.up;
        //         offsets[3] = Vector2Int.down;
        //
        //         Gizmos.color = Color.red;
        //         foreach (var offset in offsets)
        //         {
        //             //Gizmos.DrawWireCube(m_targetPosition + new Vector3(offset.x,offset.y,0), Vector3.one * 0.8f);
        //             if (CurrentNode != null)
        //                 Gizmos.DrawWireCube(CurrentNode.worldPosition + new Vector3(offset.x, offset.y, 0), Vector3.one * 0.8f);
        //         }
    }
}