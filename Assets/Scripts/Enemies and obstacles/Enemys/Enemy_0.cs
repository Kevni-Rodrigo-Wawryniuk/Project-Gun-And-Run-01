using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_0 : MonoBehaviour
{
    public static Enemy_0 enemy_0;

    [Header("Componentes")]
    Rigidbody2D rgb;
    Animator anim;

    [Header("Patrullaje")]
    public bool patrullajes;
    [SerializeField] public Transform positionDetectorFloor;
    [SerializeField] float distanceFloor, speed;
    [SerializeField] float timeP, endTimeP;
    [SerializeField] bool direccion;
    [SerializeField] LayerMask layerFloor;

    // Start is called before the first frame update
    void Start()
    {
        StartProgram();
    }
    private void StartProgram()
    {
        if (enemy_0 == null)
        {
            enemy_0 = this;
        }
        rgb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Patrullar();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(positionDetectorFloor.position, Vector2.down * distanceFloor);
    }

    //      PATRULLAR       //
    private void Patrullar()
    {
        if (patrullajes == true)
        {

            if (direccion == true)
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                transform.localEulerAngles = new Vector3(0, 180, 0);
            }
            if (timeP < endTimeP)
            {
                timeP += 1 * Time.deltaTime;
            }
            if (!DetectorFloor())
            {
                rgb.velocity = new Vector2(0, rgb.velocity.y);
            }
            if (DetectorFloor() && timeP >= endTimeP)
            {
                anim.SetBool("walk", true);
                if (direccion == true)
                {
                    rgb.velocity = new Vector2(speed, rgb.velocity.y);
                }
                else
                {
                    rgb.velocity = new Vector2(-speed, rgb.velocity.y);
                }
            }
            else
            {
                anim.SetBool("walk", false);
            }

        }
    }
    public void CambioDireccion()
    {
        if (!DetectorFloor())
        {
            timeP = 0;
            direccion = !direccion;
        }
    }
    private bool DetectorFloor()
    {
        RaycastHit2D hit = Physics2D.Raycast(positionDetectorFloor.position, Vector2.down, distanceFloor, layerFloor);

        return hit.collider != null;
    }
    //                      //
}
