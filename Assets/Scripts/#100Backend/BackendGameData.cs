using BackEnd;
using UnityEngine;
using UnityEngine.Events;

public class BackendGameData
{
    [System.Serializable]
    public class GameDataLoadEvent : UnityEvent { }
    public GameDataLoadEvent onGameDataLoadEvent = new GameDataLoadEvent();

    private static BackendGameData instance = null;
    public static BackendGameData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BackendGameData();
            }

            return instance;
        }
    }

    private UserGameData userGameData = new UserGameData();
    public UserGameData UserGameData => userGameData;

    private string gameDataRowInDate = string.Empty;

    /// <summary>
    /// �ڳ� �ܼ� ���̺� ���ο� ���� ���� �߰�
    /// </summary>
    public void GameDataInsert()
    {
        // ���� ������ �ʱⰪ���� ����
        userGameData.Reset();

        // ���̺� �߰��� �����ͷ� ����
        Param param = new Param()
        {
            { "level", userGameData.level },
            { "experience", userGameData.experience },
            { "gold", userGameData.gold },
            { "jewel", userGameData.jewel },
            { "heart", userGameData.heart }
        };

        GameDataInsert(Constants.USER_DATA_TABLE, param);
    }

    private void GameRankDataInsert()
    {
        userGameData.dailyBestScore = 0;

        Param rankParam = new Param()
        {
            { "dailyBestScore", userGameData.dailyBestScore }
        };

        GameDataInsert(Constants.USER_RANK_DATA_TABLE, rankParam);
    }

    private void GameDataInsert(string tableName, Param param)
    {
        // ù ��° �Ű������� �ڳ� �ܼ��� "���� ���� ����" �ǿ� ������ ���̺� �̸�
        Backend.GameData.Insert(tableName, param, callback =>
        {
            // ���� ���� �߰��� �������� ��
            if (callback.IsSuccess())
            {
                // ���� ������ ������
                gameDataRowInDate = callback.GetInDate();

                Debug.Log($"���� ���� ������ ���Կ� �����߽��ϴ�. : {callback}");

                onGameDataLoadEvent?.Invoke();
            }
            // �������� ��
            else
            {
                Debug.LogError($"���� ���� ������ ���Կ� �����߽��ϴ�. : {callback}");
            }
        });
    }

    /// <summary>
    /// �ڳ� �ܼ� ���̺��� ���� ������ �ҷ��� �� ȣ��
    /// </summary>
    public void GameDataLoad()
    {
        Backend.GameData.GetMyData(Constants.USER_DATA_TABLE, new Where(), callback =>
        {
            // ���� ���� �ҷ����⿡ �������� ��
            if (callback.IsSuccess())
            {
                Debug.Log($"���� ���� ������ �ҷ����⿡ �����߽��ϴ�. : {callback}");

                // JSON ������ �Ľ� ����
                try
                {
                    LitJson.JsonData gameDataJson = callback.FlattenRows();

                    // �޾ƿ� �������� ������ 0�̸� �����Ͱ� ���� ��
                    if (gameDataJson.Count <= 0)
                    {
                        Debug.LogWarning("�����Ͱ� �������� �ʽ��ϴ�.");

                        // ���� ������ ������ ���� ���� ����
                        GameDataInsert();
                    }
                    else
                    {
                        // �ҷ��� ���� ������ ������
                        gameDataRowInDate = gameDataJson[0]["inDate"].ToString();
                        // �ҷ��� ���� ������ userGameData ������ ����
                        userGameData.level = int.Parse(gameDataJson[0]["level"].ToString());
                        userGameData.experience = int.Parse(gameDataJson[0]["experience"].ToString());
                        userGameData.gold = int.Parse(gameDataJson[0]["gold"].ToString());
                        userGameData.jewel = int.Parse(gameDataJson[0]["jewel"].ToString());
                        userGameData.heart = int.Parse(gameDataJson[0]["heart"].ToString());

                        onGameDataLoadEvent?.Invoke();
                    }
                }
                // JSON ������ �Ľ� ����
                catch (System.Exception e)
                {
                    // ���� ������ �ʱⰪ���� ����
                    userGameData.Reset();
                    // try-catch ���� ���
                    Debug.LogError(e);
                }
            }
            // �������� ��
            else
            {
                Debug.LogError($"���� ���� ������ �ҷ����⿡ �����߽��ϴ�. : {callback}");
            }
        });

        Backend.GameData.GetMyData(Constants.USER_RANK_DATA_TABLE, new Where(), callback =>
        {
            // ���� ���� �ҷ����⿡ �������� ��
            if (callback.IsSuccess())
            {
                Debug.Log($"���� ��ŷ ������ �ҷ����⿡ �����߽��ϴ�. : {callback}");

                // JSON ������ �Ľ� ����
                try
                {
                    LitJson.JsonData gameDataJson = callback.FlattenRows();

                    // �޾ƿ� �������� ������ 0�̸� �����Ͱ� ���� ��
                    if (gameDataJson.Count <= 0)
                    {
                        Debug.LogWarning("��ŷ �����Ͱ� �������� �ʽ��ϴ�.");

                        GameRankDataInsert();
                    }
                }
                // JSON ������ �Ľ� ����
                catch (System.Exception e)
                {
                    // try-catch ���� ���
                    Debug.LogError(e);
                }
            }
            // �������� ��
            else
            {
                Debug.LogError($"���� ��ŷ ������ �ҷ����⿡ �����߽��ϴ�. : {callback}");
            }
        });
    }

    /// <summary>
    /// �ڳ� �ܼ� ���̺� �ִ� ���� ������ ����
    /// </summary>
    public void GameDataUpdate(UnityAction action = null)
    {
        if (userGameData == null)
        {
            Debug.LogError("�������� �ٿ�ްų� ���� ������ �����Ͱ� �������� �ʽ��ϴ�." +
                           "Insert Ȥ�� Load�� ���� �����͸� �������ּ���.");
            return;
        }

        Param param = new Param()
        {
            { "level", userGameData.level },
            { "experience", userGameData.experience },
            { "gold", userGameData.gold },
            { "jewel", userGameData.jewel },
            { "heart", userGameData.heart }
        };

        // ���� ������ ������(gameDataRowIndate)�� ������ ���� �޽��� ���
        if (string.IsNullOrEmpty(gameDataRowInDate))
        {
            Debug.LogError("������ inDate ������ ���� ���� ���� ������ ������ �����߽��ϴ�.");
        }
        // ���� ������ �������� ������ ���̺� ����Ǿ� �ִ� �� �� inDate Į���� ����
        // �����ϴ� ������ owner_inDate�� ��ġ�ϴ� row�� �˻��Ͽ� �����ϴ� UpdateV2() ȣ��
        else
        {
            Debug.Log($"{gameDataRowInDate}�� ���� ���� ������ ������ ��û�մϴ�.");

            Backend.GameData.UpdateV2(Constants.USER_DATA_TABLE, gameDataRowInDate, Backend.UserInDate, param, callback =>
            {
                if (callback.IsSuccess())
                {
                    Debug.Log($"���� ���� ������ ������ �����߽��ϴ�. : {callback}");

                    action?.Invoke();

                    onGameDataLoadEvent?.Invoke();
                }
                else
                {
                    Debug.LogError($"���� ���� ������ ������ �����߽��ϴ�. : {callback}");
                }
            });
        }
    }
}
