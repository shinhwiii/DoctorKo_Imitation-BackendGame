using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;            // ������ �� �����Ǵ� �Ȼ�ü ������
    [SerializeField]
    private float attackRate = 0.1f;                // ���� �ӵ�

    private float lastAttacktime = 0;               // ������ ���ݽð� üũ

    public void WeaponAction()
    {
        // ������ �������κ��� attackRate �ð���ŭ ������ ���� ����
        if (Time.time - lastAttacktime > attackRate)
        {
            // ���� �÷��̾� ��ġ(transform.position)�� �߻�ü ������Ʈ�� ����
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // �����ֱⰡ �Ǿ�� ������ �� �ֵ��� �ϱ� ���� ���� ���ݽð� ����
            lastAttacktime = Time.time;
        }
    }
}
