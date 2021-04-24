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

    private void Awake()
    {
        m_gridRef = GameObject.Find("Grid").GetComponent<TileGrid>();

        if (m_gridRef == null)
        {
            Debug.LogError("No grid could be found in scene, wrong name in hierarchy maybe?");
        }

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
        TargetNode = CurrentNode.GetNeighbour(direction);

        //hit logic
        // if (true)
        // {
        //     Hit(/*hit obj*/null, 0);
        // }

        //Shooting logic
        if (TargetNode != null && !TargetNode.isWall && !TargetNode.isHole)
        {
            if (m_hasShot)
            {
                // shoot if: node had no bullets already, wont be occupied by the player or a wall next step

                if (m_shotDirection == direction && !CurrentNode.GetNeighbour(m_shotDirection).HasObjectOfType<bullet>())  // TODO @me:
                    Shoot(CurrentNode.GetNeighbour(m_shotDirection));                                             //   fix this <3
                else if (!CurrentNode.GetNeighbour(m_shotDirection).HasObjectOfType<bullet>() && !CurrentNode.HasObjectOfType<bullet>())                     //  its broken
                    Shoot(CurrentNode);                                                                              //       thanks

                m_hasShot = false;
            }

            GameController.StepController().ApplyMove();
            GameController.Instance.stepController.BeginStep();
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
        // object specific
        if (obj.GetComponent<bullet>() != null)
        {
            obj.GetComponent<bullet>().BulletDestroy();
        }

        health -= damage;

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
}