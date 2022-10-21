
public enum ESkillObjType
{
    None,
    FireBall,
    RangeSpell,
    FireBallImpact,
    Max
}
public enum ESkillType
{
    None = 0,
    FireBall,
    RangeSpell,
    Max
}
public enum EState
{
    None = 0,

    //----���� ����-----
    Hit,    // �ǰ�
    Dash,   // ������(�뽬)
    Death,  // ���
    Idle,   // ���
    Move,   // �̵�

    //----�÷��̾� ����-----
    Attack0, // ����
    Attack1, // ����
    Attack2, // ����
    Attack3, // ����

    //----���� ����-----
    MagicAttack0 = 21,  // ���� ����
    MagicAttack1,       // ���� ����
    Teleport,           // ���� �̵�
    SearchAttack,       // �߰� ����
    NormalAttack0,      // �⺻ ����
    NormalAttack1,
    NormalAttack2,

    Max
}