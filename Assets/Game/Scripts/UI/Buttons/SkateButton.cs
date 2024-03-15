using LazyFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkateButton : MonoBehaviour
{
    [Header("Library")]
    [SerializeField] SkateBoardLibrary library;

    [Header("References")]
    [SerializeField] Image skateImage;
    [SerializeField] Image noti;
    [SerializeField] Image lockIcon;

    [Header("Equip button")]
    [SerializeField] TMP_Text useText;

    [SerializeField] Sprite useSprite;
    [SerializeField] Sprite usedSprite;
    [SerializeField] Sprite buySprite;

    [SerializeField] Image button;
    [SerializeField] TMP_Text unlockText;
    int index;
    private void Start()
    {
        Subscribe();
    }
    private void OnDestroy()
    {
        Unsubscribe();
    }
    private void OnEnable()
    {
        CheckUnlock();
        CheckUse();
    }
    public void OnButtonCLick()
    {
        if (DataService.IsSkateEquiping(index) == false)
        {
            DataService.EquipSkate(index);
            NotificationService.RemoveSkateNotification(index);
        }
    }
    public void SetData(SkateBoardData data,int index)
    {
        skateImage.sprite = data.board;
        this.index = index;

        OnNotificationChange(null);
        CheckUnlock();
        CheckUse();
    }

    private void Lock()
    {
        lockIcon.gameObject.SetActive(true);
        skateImage.color=Color.black;

        button.gameObject.SetActive(false);

        unlockText.gameObject.SetActive(true);
        unlockText.text=$"Unlock at level {library.GetUnlockLevel(index)}";
    }
    private void Unlock()
    {
        button.gameObject.SetActive(true);
        button.sprite=buySprite;

        lockIcon.gameObject.SetActive(false);
        skateImage.color=Color.white;

        unlockText.gameObject.SetActive(false);
    }

    /// <summary>
    /// button check unlock on enable
    /// </summary>
    public void CheckUnlock()
    {
        if (DataService.IsSkateUnlock(index) == false)
        {
            Lock();
        }
        else
        {
            Unlock();
        }
    }

    private void CheckUse()
    {
        if(DataService.IsSkateEquiping(index) == true)
        {
            button.sprite = usedSprite;
            useText.text="Used";
        }
        else
        {
            button.sprite = useSprite;
            useText.text = "Use";
        }

    }
    private void OnNotificationChange(OnNotificationChange e)
    {
        if(NotificationService.IsSkateNoti(index) == true)
        {
            noti.gameObject.SetActive(true);
        }
        else
        {
            noti.gameObject.SetActive(false);
        }
    }
    private void OnSkateUse(OnSkateUse e)
    {
        CheckUnlock();
        CheckUse();
    }

    protected void Subscribe()
    {
        Event<OnNotificationChange>.Subscribe(OnNotificationChange);
        Event<OnSkateUse>.Subscribe(OnSkateUse);
    }
    protected void Unsubscribe()
    {
        Event<OnNotificationChange>.Unsubscribe(OnNotificationChange);
        Event<OnSkateUse>.Unsubscribe(OnSkateUse);
    }
}
