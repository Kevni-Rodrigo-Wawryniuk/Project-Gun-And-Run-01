using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.XR;
using UnityEngine.Audio;
using System.Security.Cryptography;
using System.Numerics;

public class ControlPlayer : MonoBehaviour
{
    public static ControlPlayer controlPlayer;

    [Header("Subir Escaleras")]
    public bool ladder;
    [SerializeField] float forceLadder;
    [SerializeField] bool ladderDetect;
    [SerializeField] Transform positionDetectorLadder;
    [SerializeField] Vector2 sizeLadder;
    [SerializeField] LayerMask layerLadder;


    [Header("Collicion con balas")]
    public bool collicionBullets;
    [SerializeField] float timeCollisionBullet, forceBullet;
    [SerializeField] Vector3 positionBullet;
    [SerializeField] Transform positionPlayer;

    [Header("Scale Wall")]
    public bool scaleWall;
    [SerializeField] Transform positionScaleWall;
    [SerializeField] Vector2 sizeScaleWall;
    [SerializeField] LayerMask layerScaleWall;
    [SerializeField] float timeScaleWall, forceDown, forceX, forceY;

    [Header("Climb Handhold")]
    public bool climbHandhold;
    [SerializeField] Transform positionHandle;
    [SerializeField] Vector2 sizeHandle;
    [SerializeField] LayerMask layerHandle;
    [SerializeField] float timeHandle;
    [SerializeField] bool handleIdel;

    [Header("Dash")]
    public bool dash;
    [SerializeField] float forceDash, timeDash;

    [Header("Salto y Doble Salto")]
    public bool jump;
    public bool jump_1, jump_2;
    [SerializeField] int maxJumps, countJumps;
    [SerializeField] float forceJump;
    [SerializeField] Transform pointFloor;
    [SerializeField] Vector3 sizePointFloor;
    [SerializeField] LayerMask layerfloor;
    [SerializeField] AudioSource soundJump;

    [Header("Move")]
    public bool move;
    [SerializeField] float speed;
    [SerializeField] AudioSource soundMove;
    // direccion / true derecha / false izquierda
    [SerializeField] bool address;

    [Header("Components")]
    private Rigidbody2D rgb;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        StartProgram();
    }

    void StartProgram()
    {
        if (controlPlayer == null)
        {
            controlPlayer = this;
        }

        rgb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
    }
    // FixedUpdate is called thirty per frame
    void FixedUpdate()
    {
        // subir escaleras
        LadderUp();
        // Enganchar en un asidero
        ClimbHandle();
        // Escalar los muros con saltos
        ScaleWall();
        // MOVIMIENTO DE IZQUIERDA A DERECHA
        Moviment();
        // DASH
        UseDash();
    }

    // Update is called once per frame
    void Update()
    {
        // SALTOS
        Jumps();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (collicionBullets == true)
        {
            if (other.collider.CompareTag("BulletCanyon"))
            {
                positionBullet = other.collider.transform.position;

                StartCoroutine(CollisionBullet());
            }
        }
    }

    /////////////////////////////////// DIBUJAR RADIOS DE COLICION ////////////////////////
    private void OnDrawGizmos()
    {
        // detectar el suelo
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pointFloor.position, sizePointFloor);

        // colgarce de esquinas
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(positionHandle.position, sizeHandle);

        // rojo es para la funcion de ataque
        Gizmos.color = Color.red;

        // escalar muros 
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(positionScaleWall.position, sizeScaleWall);

        // subir escaleras
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(positionDetectorLadder.position, sizeLadder);
    }
    ///////////////////////////////////////////////////////////////////////////////////////

    //      SUBIR ESCALERAS     //
    private bool DetectLadder()
    {
        RaycastHit2D hit = Physics2D.BoxCast(positionDetectorLadder.position, sizeLadder, 0, Vector2.zero, 0, layerLadder);

        return hit.collider != null;
    }
    private void LadderUp()
    {
        if (ladder == true)
        {
            if (DetectLadder())
            {
                if (ladderDetect == false)
                {
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        ladderDetect = !ladderDetect;
                    }
                }

                if (ladderDetect == true)
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        rgb.velocity = new Vector2(rgb.velocity.x, forceLadder);
                        anim.SetBool("Ladder", true);
                        anim.SetBool("LadderStop", false);
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        rgb.velocity = new Vector2(rgb.velocity.x, -forceLadder);
                        anim.SetBool("Ladder", true);
                        anim.SetBool("LadderStop", false);
                    }
                    else
                    {
                        rgb.velocity = new Vector2(rgb.velocity.x, 0);
                        rgb.gravityScale = 0;
                        anim.SetBool("LadderStop", true);
                    }
                }
                else
                {
                    rgb.gravityScale = 1;
                    anim.SetBool("Ladder", false);
                    anim.SetBool("LadderStop", false);
                }
            }
            else
            {
                rgb.gravityScale = 1;
                ladderDetect = false;
                anim.SetBool("Ladder", false);
                anim.SetBool("LadderStop", false);
            }
        }
    }
    //                          //


    //      COLICION CON BALAS      //
    private IEnumerator CollisionBullet()
    {
        if (positionPlayer.position.x > positionBullet.x)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);

            rgb.AddForce(new Vector2(-forceBullet, rgb.velocity.y), ForceMode2D.Impulse);
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);

            rgb.AddForce(new Vector2(forceBullet, rgb.velocity.y), ForceMode2D.Impulse);
        }

        move = false;
        jump = false;
        dash = false;
        collicionBullets = false;

        anim.SetTrigger("Hurt");

        yield return new WaitForSeconds(timeCollisionBullet);

        move = true;
        jump = true;
        dash = true;
        collicionBullets = true;
    }
    //                              //


    ///////////////////////////////// COLGARCE DE LIANAS /////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////////////


    ///////////////////////////////////////  ESCALAR LOS MUROS CON SALTOS //////////////////
    private bool DetectWall()
    {
        RaycastHit2D hit;

        hit = Physics2D.BoxCast(positionScaleWall.position, sizeScaleWall, 0, Vector2.zero, 0, layerScaleWall);

        return hit.collider != null;
    }
    private void ScaleWall()
    {
        if (scaleWall == true)
        {

            if (DetectWall() && !DetectFloor() && !DetectHandle())
            {

                rgb.velocity = new Vector2(rgb.velocity.x, -forceDown);
                anim.SetBool("ScaleWall", true);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartCoroutine(JumpWall());
                }

            }
            else
            {
                anim.SetBool("ScaleWall", false);
            }
        }
    }
    private IEnumerator JumpWall()
    {

        move = false;
        jump = false;
        dash = false;
        scaleWall = false;
        climbHandhold = false;

        anim.SetBool("ScaleWall", false);

        // derecha
        if (address == true)
        {
            rgb.AddForce(new Vector2(-forceX, forceY), ForceMode2D.Impulse);
            Invoke("GirarIzquierda", 0.1f);
        }

        // izquierda
        if (address == false)
        {
            rgb.AddForce(new Vector2(forceX, forceY), ForceMode2D.Impulse);
            Invoke("GirarDerecha", 0.1f);
        }

        yield return new WaitForSeconds(timeScaleWall);

        move = true;
        jump = true;
        dash = true;
        scaleWall = true;
        climbHandhold = true;

    }

    // grirar en direccion contraria al muro
    private void GirarDerecha()
    {
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }
    private void GirarIzquierda()
    {

        transform.localEulerAngles = new Vector3(0, 180, 0);
    }
    //////////////////////////////////////////////////////////////////////////////////////


    /////////////////////////////////// MOVIMIENTO DE IZQUIERDA A DERECHA ////////////////////////////////////////////
    private void Moviment()
    {
        if (move == true)
        {
            // derecha
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                // direccion
                address = true;
                // animacion de movimiento 'Move'
                anim.SetBool("Move", true);
                // mover al personaje
                rgb.velocity = new Vector2(speed, rgb.velocity.y);
                // girar al jugador 
                transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            // izquierda
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                // direccion
                address = false;
                // animacion de movimiento 'Move'
                anim.SetBool("Move", true);
                // mover al personaje
                rgb.velocity = new Vector2(-speed, rgb.velocity.y);
                // girar al jugador
                transform.localEulerAngles = new Vector3(0, 180, 0);
            }
            // detener
            else
            {
                // animacion de movimiento 'Move'
                anim.SetBool("Move", false);
                // parar personaje
                rgb.velocity = new Vector2(0, rgb.velocity.y);
            }
        }
    }
    public void SoundMove()
    {
        soundMove.Play();
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    /////////////////////////////////// SALTOS ////////////////////////////////////////////
    private bool DetectFloor()
    {

        RaycastHit2D hit;

        hit = Physics2D.BoxCast(pointFloor.position, sizePointFloor, 0, Vector2.zero, 0, layerfloor);

        return hit.collider != null;
    }
    private void Jumps()
    {
        if (jump == true)
        {
            if(jump_1 == true && jump_2 == false){
                maxJumps = 1;
            }
            if(jump_1 == false && jump_2 == true){
                maxJumps = 2;
            }
            if (countJumps == maxJumps && !DetectFloor())
            {
                anim.SetBool("Fall", true);
            }
            else if (countJumps == 0 && !DetectFloor())
            {
                anim.SetBool("Fall", true);
            }
            if (DetectWall() || DetectHandle())
            {
                anim.SetBool("Fall", false);
            }

            if (DetectFloor())
            {
                anim.SetBool("Fall", false);
                // volver a tener saltos
                countJumps = maxJumps;
            }

            if (Input.GetKeyDown(KeyCode.Space) && countJumps == 1)
            {
                // gravedad
                rgb.gravityScale = 1;
                // saltar 
                // rgb.AddForce(new Vector2(rgb.velocity.x, forceJump));
                rgb.AddForce(new Vector2(rgb.velocity.x, forceJump), ForceMode2D.Impulse);
                // animacion de salto
                anim.SetInteger("Jump", 2);
                anim.SetBool("Move", false);
                anim.SetBool("Fall", false);
                // restar la cantidad de saltos
                countJumps -= 1;
                // sonido de salto
                soundJump.Play();
            }

            if (Input.GetKeyDown(KeyCode.Space) && countJumps == 2)
            {
                // gravedad
                rgb.gravityScale = 1;
                // saltar 
                rgb.AddForce(new Vector2(rgb.velocity.x, forceJump), ForceMode2D.Impulse);
                // animacion de salto
                anim.SetInteger("Jump", 1);
                anim.SetBool("Move", false);
                anim.SetBool("Fall", false);
                // restar la cantidad de saltos
                countJumps -= 1;
                // sonido de salto
                soundJump.Play();
            }

            if (countJumps == maxJumps)
            {
                // animacion de salto
                anim.SetInteger("Jump", 0);
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////


    /////////////////////////////////// DASH ////////////////////////////////////////////////////////////////////////
    private IEnumerator Dash()
    {
        // desactivar
        dash = false;
        move = false;
        jump = false;

        rgb.gravityScale = 0;
        rgb.velocity = new Vector2(rgb.velocity.x, 0);

        // Usar el dash
        if (transform.eulerAngles.y == 0)
        {
            rgb.AddForce(new Vector2(forceDash, 0), ForceMode2D.Impulse);
        }
        if (transform.eulerAngles.y == 180)
        {
            rgb.AddForce(new Vector2(-forceDash, 0), ForceMode2D.Impulse);
        }

        // animar dash
        anim.SetTrigger("Dash");
        anim.SetBool("Fall", false);

        yield return new WaitForSeconds(timeDash);

        rgb.gravityScale = 1;
        rgb.velocity = new Vector2(0, rgb.velocity.y);

        // activar
        dash = true;
        move = true;
        jump = true;
    }
    private void UseDash()
    {
        if (dash == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                StartCoroutine(Dash());
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    ////////////////////////////////// AGARRE EN ESQUINAS //////////////////////////////////////////////////////////
    private bool DetectHandle()
    {
        RaycastHit2D hit;

        hit = Physics2D.BoxCast(positionHandle.position, sizeHandle, 0, Vector2.zero, 0, layerHandle);

        return hit.collider != null;
    }
    private void ClimbHandle()
    {
        //  ARREGLAR EL CODIGO 
        // QUE SE AGARRE Y UTILIZE BIEN LAS ANIMACIONES
        if (climbHandhold == true)
        {

            if (DetectHandle() || DetectWall() && DetectHandle())
            {

                anim.SetBool("Fall", false);

                if (handleIdel == false)
                {
                    anim.SetInteger("Handel", 1);
                }
                if (handleIdel == true)
                {
                    anim.SetInteger("Handel", 2);
                }

                countJumps = 0;
                rgb.gravityScale = 0;

                rgb.velocity = new Vector2(rgb.velocity.x, 0);


                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartCoroutine(JumpHandle());
                    soundJump.Play();
                }

            }
            else
            {
                anim.SetInteger("Handel", 0);
                if (ladderDetect == false)
                {
                    rgb.gravityScale = 1;
                }
            }
        }
    }
    private IEnumerator JumpHandle()
    {
        scaleWall = false;
        climbHandhold = false;
        jump = false;

        anim.SetInteger("Handel", 0);
        anim.SetInteger("Jump", 1);

        rgb.gravityScale = 1;

        rgb.AddForce(new Vector2(rgb.velocity.x, forceJump), ForceMode2D.Impulse);

        yield return new WaitForSeconds(timeHandle);

        climbHandhold = true;
        jump = true;
        scaleWall = true;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
