using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintButtonPrefabScript : MonoBehaviour
{
    [SerializeField] private Text buttonText;
    [SerializeField] private Image folderImage;
    [SerializeField] private Image blueprintImage;
    
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

    }

    public void Activate(string name, BlueprintTypeEnum blueprintTypeEnum)
    {
        this.buttonText.text = name;

        this.folderImage.gameObject.SetActive(blueprintTypeEnum == BlueprintTypeEnum.FOLDER);
        this.blueprintImage.gameObject.SetActive(blueprintTypeEnum == BlueprintTypeEnum.BLUEPRINT);
    }

    public void Deactivate()
    {

    }
}

public enum BlueprintTypeEnum
{
    FOLDER,
    BLUEPRINT
}