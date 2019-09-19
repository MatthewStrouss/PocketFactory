using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenuCanvasScript : MonoBehaviour
{
    [SerializeField] private CancelCanvasScript XButton;
    [SerializeField] private OkCancelCanvasScript OkCancelCanvasScript;
    [SerializeField] private AboutCanvasScript AboutCanvasScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        this.XButton.Activate(this.Deactivate);
        this.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void ResetProgressButton_Clicked()
    {
        this.OkCancelCanvasScript.Activate(
            "Are you absolutely sure you want to delete your progress? This cannot be undone.",
            this.AcceptResetProgress,
            this.CancelResetProgress
            );
    }

    public void AcceptResetProgress()
    {
        this.OkCancelCanvasScript.Activate(
            "Just thought I would ask you again since THIS CAN NOT BE UNDONE",
            this.AcceptAcceptResetProgress,
            this.CancelResetProgress
            );
    }

    public void AcceptAcceptResetProgress()
    {
        File.Delete(Path.Combine(Application.persistentDataPath, "PlayerSave.json.bak.old"));
        File.Delete(Path.Combine(Application.persistentDataPath, "PlayerSave.json"));
        SceneManager.LoadScene(0);
    }

    public void CancelResetProgress()
    {

    }

    public void AboutButton_Clicked()
    {
        this.AboutCanvasScript.Activate();
        this.Deactivate();
    }
    
    public void SettingsButton_Clicked()
    {

    }

    public void DonateButton_Clicked()
    {

    }
}
