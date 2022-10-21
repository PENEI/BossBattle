
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

    //----공용 상태-----
    Hit,    // 피격
    Dash,   // 구르기(대쉬)
    Death,  // 사망
    Idle,   // 대기
    Move,   // 이동

    //----플레이어 상태-----
    Attack0, // 공격
    Attack1, // 공격
    Attack2, // 공격
    Attack3, // 공격

    //----몬스터 상태-----
    MagicAttack0 = 21,  // 마법 공격
    MagicAttack1,       // 마법 공격
    Teleport,           // 순간 이동
    SearchAttack,       // 추격 공격
    NormalAttack0,      // 기본 공격
    NormalAttack1,
    NormalAttack2,

    Max
}