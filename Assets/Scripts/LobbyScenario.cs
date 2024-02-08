using UnityEngine;

public class LobbyScenario : MonoBehaviour
{
    [SerializeField]
    private UserInfo user;

    private void Awake()
    {
        user.GetUserInfoFromBackend();
    }
}
