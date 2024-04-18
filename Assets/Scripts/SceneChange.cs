using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public float radius = 5;
    void Update() {
        Collider[] cols = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider col in cols) {
            if(col.transform.gameObject.CompareTag("Player")) {
                SceneManager.LoadScene("CaveCutscene");
            }
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}