using BackEnd;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FindPW : LoginBase
{
    [SerializeField]
    private Image imageID;               // ID �ʵ� ���� ����
    [SerializeField]
    private TMP_InputField inputFieldID; // ID �ʵ� �ؽ�Ʈ ���� ����
    [SerializeField]
    private Image imageEmail;               // E-mail �ʵ� ���� ����
    [SerializeField]
    private TMP_InputField inputFieldEmail; // E-mail �ʵ� �ؽ�Ʈ ���� ����

    [SerializeField]
    private Button btnFindPW;               // "��й�ȣ ã��" ��ư (��ȣ�ۿ� ����/�Ұ���)

    public void OnClickFindPW()
    {
        // �Ű������� �Է��� InputField UI�� ����� Message ���� �ʱ�ȭ
        ResetUI(imageID, imageEmail);

        // �ʵ� ���� ����ִ��� üũ
        if (IsFieldDataEmpty(imageID, inputFieldID.text, "���̵�")) return;
        if (IsFieldDataEmpty(imageEmail, inputFieldEmail.text, "�̸���")) return;

        // ���� ���� �˻�
        if (!inputFieldEmail.text.Contains("@"))
        {
            GuideForIncorrectlyEnteredData(imageEmail, "���� ������ �߸��Ǿ����ϴ�.(ex. address@xx.xx)");
            return;
        }

        // "��й�ȣ ã��" ��ư�� ��ȣ�ۿ� ��Ȱ��ȭ
        btnFindPW.interactable = false;
        SetMessage("���� �߼����Դϴ�..");

        // �ڳ� ���� ��й�ȣ ã�� �õ�
        FindCustomPW();
    }

    private void FindCustomPW()
    {
        // ��й�ȣ�� �ʱ�ȭ�ϰ�, �ʱ�ȭ�� ��й�ȣ ������ �̸��Ϸ� �߼�
        Backend.BMember.ResetPassword(inputFieldID.text, inputFieldEmail.text, callback =>
        {
            // "��й�ȣ ã��" ��ư ��ȣ�ۿ� Ȱ��ȭ
            btnFindPW.interactable = true;

            // ���� �߼� ����
            if (callback.IsSuccess())
            {
                SetMessage($"{inputFieldEmail.text} �ּҷ� ������ �߼��Ͽ����ϴ�.");
            }
            // ���� �߼� ����
            else
            {
                string message = string.Empty;

                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 404:   // �ش� �̸����� ������ ���� ���
                        message = "�ش� �̸����� ����ϴ� ����ڰ� �����ϴ�.";
                        break;
                    case 429:   // 24�ð� �̳��� 5ȸ �̻� ���� �̸��� ������ ���̵�/��й�ȣ ã�⸦ �õ��� ���
                        message = "24�ð� �̳��� 5ȸ �̻� ���̵�/��й�ȣ ã�⸦ �õ��߽��ϴ�.";
                        break;
                    default:
                        // statusCode : 400 => ������Ʈ�� Ư�����ڰ� �߰��� ��� (�ȳ� ���� �̹߼� �� ���� �߻�)
                        message = callback.GetMessage();
                        break;
                }

                if (message.Contains("�̸���"))
                {
                    GuideForIncorrectlyEnteredData(imageEmail, message);
                }
                else
                {
                    SetMessage(message);
                }
            }
        });
    }
}
