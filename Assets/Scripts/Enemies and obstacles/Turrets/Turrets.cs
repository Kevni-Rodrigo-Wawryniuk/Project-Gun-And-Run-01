using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Audio;
using System.Security.Cryptography;
using Unity.Mathematics;


public class Turrets : MonoBehaviour
{
    public static Turrets turrets;

    [Header("Check Zone")]
    public bool checkZone;
    [SerializeField] Transform rotatorPosition, playerPosition, rotator;
    [SerializeField] float radioVision;
    [SerializeField] LayerMask layerPlayer;

    [Header("Disparos De Prueba")]
    public bool tryshot;
    [SerializeField] Transform positionShot, directionShot;
    [SerializeField] GameObject tryBullet;
    [SerializeField] float forceShot;
    [SerializeField] Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        StartProgram();
    }
    private void StartProgram()
    {
        if (turrets == null)
        {
            turrets = this;
        }
        checkZone = true;
    }
    // Update is called once per frame
    void Update()
    {
        LookPlayer();
        TryShot();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(rotator.position, radioVision);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position, direction);

    }

    ///////////////////  BUSCAR AL JUGADOR //////////////////////
    private bool DetectPlayer()
    {
        RaycastHit2D hit = Physics2D.CircleCast(rotator.position, radioVision, UnityEngine.Vector2.zero, 0, layerPlayer);

        return hit.collider != null;
    }
    private void LookPlayer()
    {
        if (checkZone == true)
        {
            if (DetectPlayer())
            {
                playerPosition = GameObject.FindGameObjectWithTag("PositionPlayer").transform;

                float angle = Mathf.Atan2(playerPosition.position.y - rotatorPosition.position.y, playerPosition.position.x - rotatorPosition.position.x);
                float gradeAngle = (180 / Mathf.PI) * angle;

                rotator.rotation = UnityEngine.Quaternion.Euler(0, 0, gradeAngle);
            }
        }
    }
    private void TryShot()
    {
        if (tryshot == true)
        {
            if (DetectPlayer())
            {
                if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    GameObject bullet = Instantiate(tryBullet, positionShot.position, quaternion.identity);

                    direction = playerPosition.position - positionShot.position;

                    bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * forceShot, direction.y * forceShot);
                }
            }
        }
    }

    ////////////////////////////////////////////////////////////
}
