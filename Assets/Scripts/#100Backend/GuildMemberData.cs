public class GuildMemberData
{
    public string nickname;         // �г���
    public string inDate;           // �ش� ������ inDate
    public string level;            // �ش� ������ level

    public override string ToString()
    {
        string result = string.Empty;
        result += $"nickname : {nickname}\n";
        result += $"inDate : {inDate}\n";
        result += $"level : {level}\n";

        return result;
    }
}
