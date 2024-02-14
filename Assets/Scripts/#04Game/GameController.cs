using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onGameOver;          // ���ӿ��� �Ǿ��� �� ȣ���� �޼ҵ� ��� �� ����
    [SerializeField]
    private DailyRankRegister dailyRank;
    private int score = 0;

    public bool IsGameOver { set; get; } = false;
    public int Score
    {
        set => score = Mathf.Max(0, value);
        get => score;
    }

    public void GameOver()
    {
        // �ߺ� ó�� ���� �ʵ��� bool ������ ����
        if (IsGameOver) return;

        IsGameOver = true;

        // ���ӿ��� �Ǿ��� �� ȣ���� �޼ҵ���� ����
        onGameOver.Invoke();

        // ���� ���� ������ �������� ��ŷ ������ ����
        dailyRank.Process(score);
    }
}
