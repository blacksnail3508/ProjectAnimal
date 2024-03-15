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
    [SerializeField] Button equipButton;

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

        Reload();

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
            Reload();
        }
    }
    private void Reload()
    {
        isUnlocked=DataService.IsSkateUnlock(selectedSkate);

        if (isUnlocked == true)
        {
            description_text.gameObject.SetActive(false);
            skateImage.color=Color.white;
            equipButton.gameObject.SetActive(true);
        }
        else
        {
            description_text.gameObject.SetActive(true);
            description_text.text=$"Unlock at level {library.GetUnlockLevel(selectedSkate)}";
            skateImage.color=Color.black;
            equipButton.gameObject.SetActive(false);
        }

        if (DataService.IsSkateEquiping(selectedSkate)==true)
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
}
