using BackEnd;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Login : LoginBase
{
    [SerializeField]
    private Image imageID;                      // ID �ʵ� ���� ����
    [SerializeField]
    private TMP_InputField inputfieldID;        // ID �ؽ�Ʈ ���� ����
    [SerializeField]
    private Image imagePW;                      // PW �ʵ� ���� ����
    [SerializeField]
    private TMP_InputField inputfieldPW;        // PW �ؽ�Ʈ ���� ����

    [SerializeField]
    private Button btnLogin;                    // �α��� ��ư (��ȣ�ۿ� ����/�Ұ���)

    /// <summary>
    /// "�α���" ��ư�� ������ �� ȣ��
    /// </summary>
    public void OnClickLogin()
    {
        // �Ű������� �Է��� InputField UI�� ����� Message ���� �ʱ�ȭ
        ResetUI(imageID, imagePW);

        // �ʵ� ���� ����ִ��� üũ
        if (IsFieldDataEmpty(imageID, inputfieldID.text, "���̵�")) return;
        if (IsFieldDataEmpty(imagePW, inputfieldPW.text, "��й�ȣ")) return;

        // �α��� ��ư�� ��Ÿ���� ���ϵ��� ��ȣ�ۿ� ��Ȱ��ȭ
        btnLogin.interactable = false;

        // ������ �α����� ��û�ϴ� ���� ȭ�鿡 ����ϴ� ���� ������Ʈ
        // ex) �α��� ���� �ؽ�Ʈ ���, ��Ϲ��� ������ ȸ�� ��
        StartCoroutine(nameof(LoginProcess));

        // �ڳ� ���� �α��� �õ�
        ResponseToLogin(inputfieldID.text, inputfieldPW.text);
    }

    /// <summary>
    /// �α��� �õ� �� �����κ��� ���޹��� message�� ������� ���� ó��
    /// </summary>
    private void ResponseToLogin(string ID, string PW)
    {
        // ������ �α��� ��û (�񵿱�)
        Backend.BMember.CustomLogin(ID, PW, callback =>
        {
            StopCoroutine(nameof(LoginProcess));

            // �α��� ����
            if (callback.IsSuccess())
            {
                SetMessage($"{inputfieldID.text}�� ȯ���մϴ�.");

                // Lobby Scene���� �̵�
                Utils.LoadScene(SceneNames.Lobby);
            }
            // �α��� ����
            else
            {
                // �α��ο� �������� ���� �ٽ� �α����� �ؾ��ϱ� ������ "�α���" ��ư ��ȣ�ۿ� Ȱ��ȭ
                btnLogin.interactable = true;

                string message = string.Empty;

                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 401:   // �������� �ʴ� ���̵�, �߸��� ��й�ȣ
                        message = callback.GetMessage().Contains("customID") ? "�������� �ʴ� ���̵��Դϴ�." : "�߸��� ��й�ȣ�Դϴ�.";
                        break;
                    case 403:   // ���� or ����̽� ����
                        message = callback.GetMessage().Contains("user") ? "���ܴ��� �����Դϴ�." : "���ܴ��� ����̽��Դϴ�.";
                        break;
                    case 410:   // Ż�� ������
                        message = "Ż�� �������� �����Դϴ�.";
                        break;
                    default:
                        message = callback.GetMessage();
                        break;
                }

                // StatusCode 401���� "�߸��� ��й�ȣ �Դϴ�."�� ��
                if (message.Contains("��й�ȣ"))
                {
                    GuideForIncorrectlyEnteredData(imagePW, message);
                }
                else
                {
                    GuideForIncorrectlyEnteredData(imageID, message);
                }
            }
        });
    }

    private IEnumerator LoginProcess()
    {
        float time = 0;

        while (true)
        {
            time += Time.deltaTime;

            SetMessage($"�α��� ���Դϴ�... {time:F1}");

            yield return null;
        }
    }
}
