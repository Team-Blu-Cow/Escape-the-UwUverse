using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UwUverse;

public class Player : GridEntity
{
    public static event Action<bool> player_died;

    private Vector2Int m_shotDirection = Vector2Int.zero;
    private MasterInput m_input;

    private bool m_hasShot = false;

    public int health = 2;
    public int maxHealth = 3;
    public int shotCooldown = 3;

    public float maxStepTime = 0.1f;
    public float stepTime = 0;

    public bool testBool = false;

    private void Awake()
    {
        if (GameController.Instance.stepController != null)
            GameController.StepController().Reset();

        m_gridRef = GameObject.Find("Grid").GetComponent<TileGrid>();

        if (m_gridRef == null)
        {
            Debug.LogError("No grid could be found in scene, wrong name in hierarchy maybe?");
        }

        stepTime = 0;

        m_input = new MasterInput();
        m_input.PlayerMovement.Move.performed += ctx => PreStep(new Vector2Int((int)ctx.ReadValue<Vector2>().x, (int)ctx.ReadValue<Vector2>().y));//Move(new Vector2Int((int)ctx.ReadValue<Vector2>().x, (int)ctx.ReadValue<Vector2>().y));
        m_input.PlayerMovement.Idle.performed += ctx => PreStep(Vector2Int.zero);
        m_input.PlayerShoot.Direction.performed += ctx => SetShot(new Vector2Int((int)ctx.ReadValue<Vector2>().x, (int)ctx.ReadValue<Vector2>().y));
        m_input.PlayerShoot.Mouse.performed += ctx => SetShot(DirectionFromMouse());
        m_input.PlayerShoot.Undo.performed += ctx => UndoShot();
    }

    private void OnDestroy()
    {
        if (GameController.Instance != null && GameController.StepController() != null)
        {
            GameController.StepController().RemoveEntity();
            GameController.StepController().StepEvent -= Move;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        GameController.StepController().StepEvent += Move;
        GameController.StepController().AddEntity();

        CurrentNode = m_gridRef.GetNearestNode(transform.position);
        CurrentNode.AddObject(gameObject);

        health = maxHealth;

        GameController.LevelStats().Reset();
    }

    private void OnEnable()
    {
        m_input.Enable();
    }

    private void OnDisable()
    {
        m_input.Disable();
    }

    public void PreStep(Vector2Int direction)
    {
        if (GameController.Instance.isPaused)
            return;

        Direction = direction;
        if (stepTime > maxStepTime)
        {
            TargetNode = CurrentNode.GetNeighbour(Direction);

            //Shooting logic
            if (TargetNode != null && TargetNode.isTraversable)
            {
                GameController.Instance.stepController.BeginStep();

                StartCoroutine(WaitForGridEntities(direction));
            }
        }
    }

    public IEnumerator WaitForGridEntities(Vector2Int direction)
    {
        while (GameController.StepController().entitiesMoved < GameController.StepController().entityCount - 1)
        {
            yield return null;
        }

        BeginStep(direction);
    }

    public void BeginStep(Vector2Int direction)
    {
        if (GameController.Instance.isPaused)
            return;

        HitDetectionLogic();
        ShootLogic();

        GameController.StepController().ApplyMove();
        stepTime = 0;
    }

    private void HitDetectionLogic()
    {
        /* Offsets & Nodes diagram
        *
        * P = player
        * n = index in list
        * ┌───┐
        * │ n │ = nodes[n]
        * └───┘
        * ✢n✢ = dangerVectors[n]
        *
        *
        *
        *      ┌───┐
        *      │↓1↓│
        *      └───┘
        *┌───┐ ┌───┐ ┌───┐
        *│→0→│ │↓3↓│ │←2←│
        *└───┘ └───┘ └───┘
        *      ╔═══╗
        *      ║↑P↑║
        *      ╚═══╝
        */

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

        if (Direction != Vector2Int.zero)
            CheckForCollisionsMoving(nodes, dangerVectors);
        else
            CheckForCollisionsStationary(nodes, dangerVectors);
    }

    public override void CheckForCollisionsMoving(GridNode[] nodes, Vector2Int[] dangerVectors)
    {
        int CheckNum = nodes.Length;

        GameObject[] bullets = new GameObject[CheckNum];
        GameObject[] enemies = new GameObject[CheckNum];

        for (int i = 0; i < CheckNum; i++)
        {
            // check nodes for a bullet
            if (nodes[i].HasObjectOfType<bullet>(ref bullets[i]))
            {
                // check if the bullet is heading towards the player's next position
                if (bullets[i] != null && (bullets[i].GetComponent<bullet>().m_direction == dangerVectors[i]))
                {
                    Hit(bullets[i], 1);
                    Debug.Log("bullet has hit me");
                }
            }

            // check nodes for enemy object
            if (nodes[i].HasObjectOfType<EnemyController>(ref enemies[i]))
            {
                // check if enemy is moving to occupy target position
                if (enemies[i] != null && (enemies[i].GetComponent<GridEntity>().Direction == dangerVectors[i]))
                {
                    switch (i)
                    {
                        /* Offsets & Nodes diagram
                         *
                         * P = player
                         * n = index in list
                         * ┌───┐
                         * │ n │ = nodes[n]
                         * └───┘
                         * ✢n✢ = dangerVectors[n]
                         *
                         *      ┌───┐
                         *      │↓1↓│
                         *      └───┘
                         *┌───┐ ┌───┐ ┌───┐
                         *│→0→│ │↓3↓│ │←2←│
                         *└───┘ └───┘ └───┘
                         *      ╔═══╗
                         *      ║↑P↑║
                         *      ╚═══╝
                         */

                        case 0:
                        case 1:
                        case 2:
                            {
                                // check if enemy is moving to occupy target position
                                if (enemies[i] != null && (enemies[i].GetComponent<GridEntity>().Direction == dangerVectors[i]))
                                {
                                    //Debug.Log("Enemy has hit me");
                                    Hit(enemies[i], 1);

                                    PushEntity(dangerVectors[i], TargetNode);
                                }
                                break;
                            }

                        case 3:
                            {
                                if (enemies[i] != null)
                                {
                                    Hit(enemies[i], 1);

                                    PushEntity(dangerVectors[i], CurrentNode);
                                }
                                break;
                            }
                    }
                }
                if (i == 3 && enemies[i] != null && (enemies[i].GetComponent<GridEntity>().Direction == Vector2Int.zero))

                {
                    Hit(enemies[i], 1);
                    TargetNode = CurrentNode;
                }
            }
        }
    }

    public override void CheckForCollisionsStationary(GridNode[] nodes, Vector2Int[] dangerVectors)
    {
        int CheckNum = nodes.Length;

        GameObject[] bullets = new GameObject[CheckNum];
        GameObject[] enemies = new GameObject[CheckNum];

        for (int i = 0; i < CheckNum; i++)
        {
            // check nodes for a bullet
            if (nodes[i].HasObjectOfType<bullet>(ref bullets[i]))
            {
                // check if the bullet is heading towards the player's next position
                if (bullets[i] != null && (bullets[i].GetComponent<bullet>().m_direction == dangerVectors[i]))
                {
                    Hit(bullets[i], 1);
                    Debug.Log("bullet has hit me");
                }
            }

            // check nodes for enemy object
            if (nodes[i].HasObjectOfType<EnemyController>(ref enemies[i]))
            {
                // check if enemy is moving to occupy target position
                if (enemies[i] != null && (enemies[i].GetComponent<GridEntity>().Direction == dangerVectors[i]))
                {
                    Debug.Log("Enemy has hit me");
                    Hit(enemies[i], 1);

                    TargetNode = null;

                    PushEntity(dangerVectors[i], CurrentNode);
                }
            }
        }
    }

    private void PushEntity(Vector2Int directionVector, GridNode referenceNode)
    {
        TargetNode = null;

        // check opposite tile to see if it is free
        if (referenceNode.GetNeighbour(directionVector).isTraversable)
        {
            TargetNode = referenceNode.GetNeighbour(directionVector);
            return;
        }
        else
        {
            Vector2Int checkVector;

            if (Direction == -directionVector || Direction == Vector2Int.zero)
            {
                // randomly check other adjacent tiles to see if they are free
                int checkDir = UnityEngine.Random.Range(0, 2);
                checkDir = (checkDir * 2) - 1;

                checkVector = new Vector2Int(checkDir * directionVector.y, checkDir * directionVector.x);
            }
            else
            {
                checkVector = -Direction;
            }

            if (CurrentNode.GetNeighbour(checkVector).isTraversable)
                TargetNode = referenceNode.GetNeighbour(checkVector);
            else if (CurrentNode.GetNeighbour(-checkVector).isTraversable)
                TargetNode = referenceNode.GetNeighbour(-checkVector);
        }

        // if no free space is available, kill entity
        if (TargetNode == null)
        {
            Debug.Log("I'm Dead");
            player_died?.Invoke(true);
            TargetNode = CurrentNode;
        }
    }

    private void ShootLogic()
    {
        if (m_hasShot && shotCooldown >= 3)
        {
            // shoot if: node had no bullets already, wont be occupied by the player or a wall next step
            if (m_shotDirection == Direction && !CurrentNode.GetNeighbour(m_shotDirection).HasObjectOfType<bullet>())
                Shoot(CurrentNode.GetNeighbour(m_shotDirection));
            else if (!CurrentNode.GetNeighbour(m_shotDirection).HasObjectOfType<bullet>() && !CurrentNode.HasObjectOfType<bullet>())
                Shoot(CurrentNode);
            m_hasShot = false;
            shotCooldown = -1;
            GameController.LevelStats().AddShot();
        }
    }

    private void Move()
    {
        CurrentNode.RemoveObject(gameObject);
        CurrentNode = TargetNode;
        CurrentNode.AddObject(gameObject);
        LeanTween.move(gameObject, m_gridRef.GridCoordToWorldCoord(CurrentNode.position), 0.1f);
        shotCooldown = (shotCooldown <= 3) ? shotCooldown + 1 : 3;
        GameController.LevelStats().AddMove();
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
            player_died?.Invoke(true);
            /*die();*/
        }
    }

    private void SetShot(Vector2Int in_direction)
    {
        if (in_direction == Vector2Int.zero)
            return;
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

        if (Mathf.Abs(directionf.x) > Mathf.Abs(directionf.y))
        {
            directionf.y = 0f;
        }
        else
        {
            directionf.x = 0f;
        }

        return Vector2Int.RoundToInt(directionf);
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