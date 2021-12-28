using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{

    private void PlaySound(AudioClip _sound)
    {
        SoundManager.instance.PlaySound(_sound);
    }

}
