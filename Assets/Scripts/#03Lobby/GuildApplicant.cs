using TMPro;
using UnityEngine;

public class GuildApplicant : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textNickname;
    [SerializeField]
    private TextMeshProUGUI textLevel;

    private BackendGuildSystem backendGuildSystem;
    private GuildApplicantsPage guildApplicantsPage;
    private GuildMemberData guildMemberData;

    public void Setup(BackendGuildSystem guildSystem, GuildApplicantsPage applicantsPage, GuildMemberData memberData)
    {
        textNickname.text = memberData.nickname;
        textLevel.text = $"Lv. {memberData.level}";

        backendGuildSystem = guildSystem;
        guildApplicantsPage = applicantsPage;
        guildMemberData = memberData;
    }

    public void OnClickAppoveApplicant()
    {
        // �ش� ������ UI ������Ʈ ����
        guildApplicantsPage.Deactivate(gameObject);
        // �ش� ���� ��� ���� ��û ����
        backendGuildSystem.ApproveApplicant(guildMemberData.inDate);
    }

    public void OnClickRejectApplicant()
    {
        // �ش� ������ UI ������Ʈ ����
        guildApplicantsPage.Deactivate(gameObject);
        // �ش� ���� ��� ���� ��û ����
        backendGuildSystem.RejectApplicant(guildMemberData.inDate);
    }
}
