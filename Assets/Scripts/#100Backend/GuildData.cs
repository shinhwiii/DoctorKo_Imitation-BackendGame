using System.Collections.Generic;

public class GuildData
{
    public string guildName;                        // ��� �̸�
    public string guildInDate;                      // ��� inDate
    public string notice;                           // ��� �������� (��Ÿ ������)
    public int memberCount;                         // ��� �ο���
    public GuildMemberData master;                  // ��� ������
    public List<GuildMemberData> viceMasterList;    // �� ��� ������ ���
}
