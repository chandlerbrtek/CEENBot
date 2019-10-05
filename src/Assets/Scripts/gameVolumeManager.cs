using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameVolumeManager : MonoBehaviour
{

    public AudioSource branchSFX;
    public AudioSource[] houseSFX;

    void Start()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("DestructableTree"))
        {
            branchSFX.Play();
        }

        if (collision.gameObject.CompareTag("DestructableBuilding"))
        {
            int temp = Random.Range(0, 4);
            houseSFX[temp].Play();
        }
    }
}
