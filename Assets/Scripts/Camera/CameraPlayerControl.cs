using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerControl : MonoBehaviour
{
    public static CameraPlayerControl cameraPlayerControl;

    [Header("Follow Player")]
    public bool followPlayer;
    [SerializeField] Vector3 offSet, cameraPosition;
    [SerializeField] Transform playerPosition;
    [SerializeField] float smoothness;


    // Start is called before the first frame update
    void Start()
    {
        StartProgram();
    }
    void StartProgram()
    {
        if (cameraPlayerControl == null)
        {
            cameraPlayerControl = this;
        }

        playerPosition = GameObject.Find("Player").transform;

        offSet = transform.position - playerPosition.position;
        offSet += cameraPosition;

    }

    // Update is called once per frame
    void Update()
    {
        // SEGUI AL JUGADOR
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (followPlayer == true)
        {
            // determinar las posiciones
            Vector3 position0 = transform.position;
            Vector3 position1 = playerPosition.position + offSet;

            // seguir
            Vector3 seguir = Vector3.Lerp(position0, position1, smoothness * Time.deltaTime);
            transform.position = seguir;
        }
    }
 
}
