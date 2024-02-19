using TMPro;
using UnityEngine;

public class Notice : MonoBehaviour
{
    [SerializeField]
    private BackendGuildSystem backendGuildSystem;
    [SerializeField]
    private GameObject noticeBackground;                // 길드원에게 보이는 오브젝트
    [SerializeField]
    private TextMeshProUGUI textNotice;                 // 길드원에게 보이는 공지사항
    [SerializeField]
    private TMP_InputField inputFileNotice;             // 길드마스터에게 보이는 공지사항 오브젝트
    [SerializeField]
    private FadeEffect_TMP textLog;

    public void Setup(bool isMaster = false)
    {
        textNotice.text = backendGuildSystem.myGuildData.notice;
        inputFileNotice.text = backendGuildSystem.myGuildData.notice;

        noticeBackground.SetActive(!isMaster);
        inputFileNotice.gameObject.SetActive(isMaster);
    }

    public void OnClickNoticeRegistration()
    {
        string notice = inputFileNotice.text;

        if (notice.Trim().Equals(""))
        {
            textLog.FadeOut("공지사항 내용이 비어있습니다.");
            return;
        }

        backendGuildSystem.SetNotice(notice);
    }
}
