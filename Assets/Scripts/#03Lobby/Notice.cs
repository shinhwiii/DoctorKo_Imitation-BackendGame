using TMPro;
using UnityEngine;

public class Notice : MonoBehaviour
{
    [SerializeField]
    private BackendGuildSystem backendGuildSystem;
    [SerializeField]
    private GameObject noticeBackground;                // �������� ���̴� ������Ʈ
    [SerializeField]
    private TextMeshProUGUI textNotice;                 // �������� ���̴� ��������
    [SerializeField]
    private TMP_InputField inputFileNotice;             // ��帶���Ϳ��� ���̴� �������� ������Ʈ
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
            textLog.FadeOut("�������� ������ ����ֽ��ϴ�.");
            return;
        }

        backendGuildSystem.SetNotice(notice);
    }
}
