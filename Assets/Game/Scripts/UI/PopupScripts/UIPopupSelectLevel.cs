using LazyFramework;
using UnityEngine;
public class UIPopupSelectLevel : UIPopupBase
{
    [Header("Popup Select")]
    [SerializeField] LevelButton buttonPrefab;
    [SerializeField] LevelAsset levelAsset;
    [SerializeField] GameObject buttonRoot;

    protected override void OnShow()
    {
        base.OnShow();
    }
    private void Start()
    {
        CreateLevelButtons();
    }
    private void CreateLevelButtons()
    {
        for (int i = 0; i<levelAsset.listLevel.Count; i++)
        {
            var button = Instantiate(buttonPrefab , buttonRoot.transform);
            button.SetData(i);
        }
    }
    public void ShowLastMenu()
    {
        DisplayService.ShowLastMenu();
    }
    public override void Hide(float? fadeTime = 0.25F)
    {
        base.Hide(fadeTime);
    }
}
