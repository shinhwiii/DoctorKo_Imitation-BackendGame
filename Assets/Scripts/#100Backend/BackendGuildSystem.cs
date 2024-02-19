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

            Debug.Log($"길드가 생성되었습니다. : {callback}");

            // 길드 생성에 성공했을 때 호출
            guildCreatePage.SuccessCreateGuild();
        });
    }

    private void ErrorLogCreateGuild(BackendReturnObject callback)
    {
        string message = string.Empty;

        switch (int.Parse(callback.GetStatusCode()))
        {
            case 403:   // Backend Consold에 설정한 조건을 만족하지 못했을 때
                message = "길드 생성을 위한 레벨이 부족합니다.";
                break;
            case 409:   // 중복된 길드명으로 생성 시도한 경우
                message = "이미 동일한 이름의 길드가 존재합니다.";
                break;
            default:
                message = callback.GetMessage();
                break;
        }

        ErrorLog(message, "Guild_Failed_Log", "ApplyGuild");
    }

    private void ErrorLog(string message, string behaviorType = "", string paramKey = "")
    {
        // 에러 내용을 Console View에 출력
        Debug.LogError($"{paramKey} : {message}");

        // 에러 내용을 UI로 출력
        textLog.FadeOut(message);

        // 에러 내용을 Backend Console에 저장
        Param param = new Param() { { paramKey, message } };
        // InsertLogV2(행동 유형, Key&Value)
        Backend.GameLog.InsertLogV2(behaviorType, param);
    }
}
