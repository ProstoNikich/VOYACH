using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Gameplay settings:")]
    [SerializeField] int hp = 10;
    [SerializeField] int dmg = 5;
    [Header("Timer damage settings:")]
    [SerializeField] float timerDamage = 5.0f;
    [SerializeField] float timerActionDamage = 2.0f;
    [SerializeField] int damagePoision = 1;
    [SerializeField] float cooldownPoisionDamage = 1;

    bool activetedActionTimer = false;

    public int HP { get => hp; set => hp = value; }
    public int DMG { get => dmg; }
    private static Coroutine poisionCoroutine = null;

    void Start()
    {

    }

    void FixedUpdate()
    {
        
    }

    private void Update()
    {
        if (hp <= 0) StartCoroutine(Dead(0.1f));
        CheckTimer();        
        CheckAnim();

        if (Input.GetKeyDown(KeyCode.W)){
            Move(new Vector3(0, 0, 1));
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            Move(new Vector3(0, 0, -1));
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            Move(new Vector3(-1, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            Move(new Vector3(1, 0, 0));
        }
    }

    private void CheckAnim()
    {
        if (activetedActionTimer) Debug.LogError("Action PoisionDamage = null referens \n Скоро получишь урон"); //включаем анимацию
    }

    void CheckTimer()
    {
        timerDamage -= Time.deltaTime;
        if (timerDamage <= timerActionDamage) activetedActionTimer = true;
        else activetedActionTimer = false;
        if (poisionCoroutine == null && timerDamage <= 0.0f ) 
            poisionCoroutine = StartCoroutine(PosionDamage());
        else if (poisionCoroutine != null && timerDamage > 0) StopPoisionCoroutine();
    }

    private IEnumerator PosionDamage()
    {
        while (hp > 0)
        {
            DamageMy(damagePoision);
            yield return new WaitForSeconds(cooldownPoisionDamage);
        }
    }
    private void StopPoisionCoroutine()
    {
        if (poisionCoroutine != null)
        {
            poisionCoroutine = null;
            StopCoroutine(PosionDamage());
        }            
    }

    private IEnumerator Dead(float timeDeadAnim)
    {
        //...                                           //запускаем анимацию
        yield return new WaitForSeconds(timeDeadAnim);  //ждем
        Time.timeScale = 0;                             //Ставим игру на паузу
        //...                                           //Запускаем меню "смерти"
        //Destroy(this);                                  //Уничтожаем этот модуль управления персонажем
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy") CombatMove(transform.position, collision.gameObject.GetComponent<Enemy>());
    }


    void Move(Vector3 nextPosition)
    {
        if (!Physics.Raycast(transform.position, nextPosition, 1.0f))
            transform.Translate(nextPosition); //если никого нет переходим туда
        else
        {
            Collider[] targetObjects = Physics.OverlapBox(transform.position + nextPosition, new Vector3(0.1f, 0.1f, 0.1f));

            if (targetObjects.Length != 1)
                Debug.LogError("Ошибка в позиции назначения, найдено " + targetObjects.Length + " коллайдеров!!!");
            else if (targetObjects[0].tag == "Enemy")
                CombatMove(nextPosition, targetObjects[0].gameObject.GetComponent<Enemy>());
            else if (targetObjects[0].tag == "Bonus")
                targetObjects[0].gameObject.GetComponent<IBonus>().ActivateBonus(this);
        }
    }
    
    void CombatMove(Vector3 targetMove, Enemy enemy)
    {
        Figth(enemy);

        if (!Physics.Raycast(transform.position, targetMove, 1.0f) || enemy.HP <= 0)
            transform.Translate(targetMove); //если враг умер переходим туда
    }
    public void Figth(Enemy enemy)
    {
        hp -= enemy.DMG;
        enemy.DamageMy(dmg);
        //Запускаем анимацию получения урона если получили урон
    }
    public void DamageMy(int damage)
    {
        hp -= damage;
        //Запускаем анимацию получения урона если получили урон
    }

}





/* При совершении хода:
 * чекаем клетку в которую идем
 * если она пустая - переходим в неё
 * если это враг
 *      Смотрим дмг/хп
 *      Производим битву
 *      Если враг жив то персонаж остаётся на месте
 *      Иначе перемещаемся в выбранную клетку
 *      Производим анимацию у персонажа и врага
 * если это бонус - подбираем, добавляем эффекты производим анимацию
 * Завершаем ход.
 * Мир производит изменения (враги ходят, появляются\исчезают бонусы)
 */
