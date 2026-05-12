using UnityEngine;
using MikeNspired.XRIStarterKit;

public class NewEnemyTest : MonoBehaviour, IEnemy
{
    [Tooltip("Damage text prefab.")]
    [SerializeField]
    private DamageText damageText;
    [Tooltip("Spawn point for damage text.")]
    [SerializeField]
    private Transform damageTextSpawn;
    bool isDead = false;
    EnemyHealth enemyHealth;
    public void Die()// enemyHealth에서 체력이 0이하일시 enemyHealth측에서 실행
    {
        if (isDead) return;
        isDead = true;
        Instantiate(damageText, damageTextSpawn.position, Quaternion.identity, damageTextSpawn)
            .SetText("DEAD!!!!!!"); //글자 프리팹을 생성하고 DEAD!!!!!라는 문장으로 설정
    }
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.OnTakeDamage += OnEnemyTakeDamage; 
            //에너미헬스의 OnTakeDamage가 실행될때 OnEnemyTakeDamage가 실행되도록 연결시켜주는 부분
        }
    }
    private void OnEnemyTakeDamage(float x)
    {
        print("da");
        if (isDead) return;
        Instantiate(damageText, damageTextSpawn.position, Quaternion.identity, damageTextSpawn)
            .SetText($"{x}");
    }

}
