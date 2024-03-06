#if UNITY_EDITOR
using LazyFramework;
using TMPro;
using UnityEngine;

public class CreatorLevelButton : ButtonBase
{
    [SerializeField] MapCreator creator;
    [SerializeField] TMP_Text levelText;
    int index;
    public void SetData(int index)
    {
        this.index=index;
        levelText.text=(index+1).ToString();
    }
    public override void OnClick()
    {
        base.OnClick();
        SelectLevel();
    }

    private void SelectLevel()
    {
        creator.LoadLevel(index);
    }
}
#endif