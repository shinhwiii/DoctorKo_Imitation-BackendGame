using TMPro;
using UnityEngine;

public class Friend : FriendBase
{
    [SerializeField]
    private TextMeshProUGUI textLevel;

    public override void Setup(BackendFriendSystem friendSystem, FriendPageBase friendPage, FriendData friendData)
    {
        base.Setup(friendSystem, friendPage, friendData);

        textLevel.text = friendData.level;
        textTime.text = System.DateTime.Parse(friendData.lastLogin).ToString();
    }

    public void OnClickDeleteFriend()
    {
        // ģ�� UI ������Ʈ ����
        friendPageBase.Deactivate(gameObject);
        // ģ�� ���� (Backend Console)
        backendFriendSystem.BreakFriend(friendData);
    }
}
