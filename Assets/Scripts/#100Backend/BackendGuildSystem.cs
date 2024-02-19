using BackEnd;
using UnityEngine;

public class BackendGuildSystem : MonoBehaviour
{
    [SerializeField]
    private FadeEffect_TMP textLog;
    [SerializeField]
    private GuildCreatePage guildCreatePage;

    public void CreateGuild(string guildName, int goodsCount = 1)
    {
        Backend.Guild.CreateGuildV3(guildName, goodsCount, callback =>
        {
            if (!callback.IsSuccess())
            {
                ErrorLogCreateGuild(callback);
                return;
            }

            Debug.Log($"��尡 �����Ǿ����ϴ�. : {callback}");

            // ��� ������ �������� �� ȣ��
            guildCreatePage.SuccessCreateGuild();
        });
    }

    private void ErrorLogCreateGuild(BackendReturnObject callback)
    {
        string message = string.Empty;

        switch (int.Parse(callback.GetStatusCode()))
        {
            case 403:   // Backend Consold�� ������ ������ �������� ������ ��
                message = "��� ������ ���� ������ �����մϴ�.";
                break;
            case 409:   // �ߺ��� �������� ���� �õ��� ���
                message = "�̹� ������ �̸��� ��尡 �����մϴ�.";
                break;
            default:
                message = callback.GetMessage();
                break;
        }

        ErrorLog(message, "Guild_Failed_Log", "ApplyGuild");
    }

    private void ErrorLog(string message, string behaviorType = "", string paramKey = "")
    {
        // ���� ������ Console View�� ���
        Debug.LogError($"{paramKey} : {message}");

        // ���� ������ UI�� ���
        textLog.FadeOut(message);

        // ���� ������ Backend Console�� ����
        Param param = new Param() { { paramKey, message } };
        // InsertLogV2(�ൿ ����, Key&Value)
        Backend.GameLog.InsertLogV2(behaviorType, param);
    }
}
