using TMPro;
using UnityEngine;

public class GuildDefaultPage : MonoBehaviour
{
    [SerializeField]
    private BackendGuildSystem backendGuildSystem;
    [SerializeField]
    private TMP_InputField inputFieldGuildName;
    [SerializeField]
    private GuildPage guildPage;
    [SerializeField]
    private FadeEffect_TMP textLog;

    [SerializeField]
    private GameObject guildPrefab;
    [SerializeField]
    private Transform parentContent;

    private MemoryPool memoryPool;

    private void Awake()
    {
        memoryPool = new MemoryPool(guildPrefab, parentContent);

        OnClickRefresh();
    }

    public void OnClickSearchGuild()
    {
        string guildName = inputFieldGuildName.text;

        if (guildName.Trim().Equals(""))
        {
            return;
        }

        inputFieldGuildName.text = "";

        string inDate = backendGuildSystem.GetGuildInfoBy(guildName);

        if (inDate.Equals(string.Empty))
        {
            textLog.FadeOut("�������� �ʴ� ����Դϴ�.");
        }
        else
        {
            backendGuildSystem.GetGuildInfo(inDate);
        }
    }

    public void OnClickMyGuildInfo()
    {
        backendGuildSystem.GetMyGuildInfo();
    }

    public void SuccessMyGuildInfo()
    {
        bool isMaster = UserInfo.Data.nickname.Equals(backendGuildSystem.myGuildData.master.nickname);
        guildPage.Setup(backendGuildSystem.myGuildData.guildName, isMaster);
    }

    public void SuccessGuildInfo()
    {
        bool isMaster = UserInfo.Data.nickname.Equals(backendGuildSystem.otherGuildData.master.nickname);
        guildPage.Setup(backendGuildSystem.otherGuildData.guildName, isMaster, true);
    }

    public void OnClickRefresh()
    {
        backendGuildSystem.GetRandomGuildList();
    }

    public void Activate(GuildData guild)
    {
        GameObject item = memoryPool.ActivatePoolItem();
        item.GetComponent<Guild>().Setup(backendGuildSystem, guild);
    }

    public void DeactivateAll()
    {
        memoryPool.DeactivateAllPoolItems();
    }
}
