using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fmodPlayer : MonoBehaviour
{
public void playSound(string path){
    FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
}
}
