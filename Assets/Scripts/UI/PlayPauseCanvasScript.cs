using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPauseCanvasScript : MonoBehaviour
{
    public Image playImage;
    public Image pauseImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayPauseButton_Click()
    {
        bool isPaused = GameManagerController.Instance.PlayPause();

        this.playImage.gameObject.SetActive(isPaused);
        this.pauseImage.gameObject.SetActive(!isPaused);
    }
}
