using System.Collections;
using TMPro;
using UnityEngine;

public class ScaleEffect : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 10f)]
    private float effectTime;                   // ũ�� Ȯ��/��� �Ǵ� �ð�
    private TextMeshProUGUI effectText;         // ũ�� Ȯ��/��� ȿ���� ���Ǵ� �ؽ�Ʈ

    private void Awake()
    {
        effectText = GetComponent<TextMeshProUGUI>();
    }

    public void Play(float start, float end)
    {
        StartCoroutine(Process(start, end));
    }

    private IEnumerator Process(float start, float end)
    {
        float current = 0f;
        float percent = 0f;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / effectTime;

            effectText.fontSize = Mathf.Lerp(start, end, percent);

            yield return null;
        }
    }
}
