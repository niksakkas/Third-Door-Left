using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fmodPlayer : MonoBehaviour
{
void playSound(string path){
    FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
}
}
