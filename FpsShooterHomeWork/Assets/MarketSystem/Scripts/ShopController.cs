using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ShopController : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] VideoClip[] videoClips;


    public void ChooseVideoClips(int id = 0)
    {
        Debug.Log("dokandý vþdyo");
        videoPlayer.gameObject.SetActive(false);
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Stop();
        videoPlayer.clip = videoClips[id];
        videoPlayer.Play();
    }

    public void debug()
    {
        Debug.Log("dokandý");
    }
}
