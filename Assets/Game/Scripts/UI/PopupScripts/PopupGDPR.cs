using TMPro;
using UnityEngine;

public class PopupGDPR : MonoBehaviour
{
    [SerializeField] string GameName;
    [SerializeField] AdsManager AdsManager;
    [SerializeField] TMP_Text title;
    private void Awake()
    {
        if (AdsManager==null) AdsManager=FindObjectOfType<AdsManager>();        //check if user has agree to GDPR
        if (PlayerPrefs.HasKey("GDPR"))
        {
            AdsManager.SetConsent();
            this.gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        title.text=$"Thanks for playing \n {GameName}";
    }

    public void OnAgree()
    {
        PlayerPrefs.SetInt("GDPR" , 1);
        this.gameObject.SetActive(false);
        AdsManager.SetConsent();
    }
    public void OnDisagree()
    {
        PlayerPrefs.SetInt("GDPR" , 0);
        this.gameObject.SetActive(false);
        AdsManager.SetConsent();
    }
}
