using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacter : MonoBehaviour
{

    public void CenouraSkills(Animator Character, GameObject PlatformObj)
    {
      //  Character.SetTrigger("SwitchCenoura");   
        PlatformObj.gameObject.SetActive(true);
        
    }

    public void BatataSkills(Animator Character, GameObject PlatformObj)
    {
      //  Character.SetTrigger("SwitchBatata");
        PlatformObj.gameObject.SetActive(false);
    }

    public void NaboSkills(Animator Character, GameObject PlatformObj)
    {
      //  Character.SetTrigger("SwitchNabo");
        PlatformObj.gameObject.SetActive(false);
    }

    public void AlhoSkills(Animator Character, GameObject PlatformObj)
    {
      //  Character.SetTrigger("SwitchAlho");
        PlatformObj.gameObject.SetActive(false);
    }
}
