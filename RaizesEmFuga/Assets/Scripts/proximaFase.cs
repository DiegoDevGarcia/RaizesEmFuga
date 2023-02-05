using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class proximaFase : MonoBehaviour
{
    public string faseParaCarregar;
    public bool batatatrue;
    public bool cenouratrue;



   private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Batata"))
        {
            batatatrue = true;

            Debug.Log(batatatrue);
        }

        if (collision.gameObject.CompareTag("Cenoura"))
        {
            cenouratrue = true;
            Debug.Log(cenouratrue);
        }

        if(batatatrue & cenouratrue)
        {
            LoadScene();
        }
    }

   /* private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Batata"))
        {

            if (collision.gameObject.CompareTag("Cenoura"))
            {
                Debug.Log("asdsadsad");
            }
            
        }

        
    }*/
    


    public void LoadScene()
    {

        SceneManager.LoadScene(faseParaCarregar);
    }
}
