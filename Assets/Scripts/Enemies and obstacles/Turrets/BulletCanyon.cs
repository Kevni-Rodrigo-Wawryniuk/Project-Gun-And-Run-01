using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCanyon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.CompareTag("Player")){
            Destroy(this.gameObject);
        }
        if(other.collider.CompareTag("Ground")){
            Destroy(this.gameObject);
        }
    }
}
