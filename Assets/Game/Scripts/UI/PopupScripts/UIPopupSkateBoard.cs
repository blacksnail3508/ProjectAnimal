using LazyFramework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupSkateBoard : UIPopupBase
{
    [SerializeField] SkateBoardLibrary library;

    [SerializeField] int selectedSkate;

    [Header("References")]
    [SerializeField] Image skateImage;
    [SerializeField] TMP_Text name_text;
    [SerializeField] TMP_Text description_text;

    [Header("Button Equip")]
    [SerializeField] Image equipButtonImage;
    [SerializeField] Sprite equipSprite;
    [SerializeField] Sprite equipingSprite;
    [SerializeField] TMP_Text equipText;

    bool isUnlocked = false;
    protected override void OnShow()
    {
        base.OnShow();
        selectedSkate = GameServices.SelectedSkate;

        skateImage.sprite = library.GetBoard(selectedSkate);
        name_text.text = skateImage.name;

        isUnlocked=DataService.IsSkateUnlock(selectedSkate);

        if(isUnlocked == true)
        {
            description_text.gameObject.SetActive(false);
        }
        else
        {
            description_text.gameObject.SetActive(true);
            description_text.text=$"Unlock at level {library.GetUnlockLevel(selectedSkate)}";
        }

        if (DataService.IsSkateEquiping(selectedSkate) == true)
        {
            equipButtonImage.sprite=equipingSprite;
            equipText.text="Equipped";
        }
        else
        {
            equipButtonImage.sprite=equipSprite;
            equipText.text="Equip";
        }
    }

    public void OnEquip()
    {
        if(DataService.IsSkateEquiping(selectedSkate))
        {
            return;
        }

        if (isUnlocked)
        {
            DataService.EquipSkate(selectedSkate);
        }
    }
}
