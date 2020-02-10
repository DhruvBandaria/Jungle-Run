using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winCollider : MonoBehaviour
{
    private SceneLoader sc;
    void Start()
    {
        sc = GetComponent<SceneLoader>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Collision");
            sc.LoadWinScene();
        }
    }
}
