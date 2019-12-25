using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestSuite
    {
        [UnityTest]
        public IEnumerator TestDamagePlayer()
        {
            GameObject GO_player = CreatePlayer(0, 1, 0);
            Player player = GO_player.GetComponent<Player>();

            GameObject GO_damageZone =
                    MonoBehaviour.Instantiate(Resources.Load<GameObject>("Test/DamageZone 1"), new Vector3(0, 0, 0), Quaternion.identity);

            DamageZone damageZone = GO_damageZone.GetComponent<DamageZone>();
            damageZone.targetTag = TargetTag.Player;
            damageZone.damage = 1;

            yield return new WaitForSeconds(0.1f); 

            Assert.AreEqual(2, player.HP);
        }

        [UnityTest]
        public IEnumerator TestDeadPlyerStopTime()
        {
            GameObject GO_player = CreatePlayer(0, 1, 0);
            Player player = GO_player.GetComponent<Player>();

            GameObject GO_damageZone =
                    MonoBehaviour.Instantiate(Resources.Load<GameObject>("Test/DamageZone 1"), new Vector3(0, 0, 0), Quaternion.identity);

            DamageZone damageZone = GO_damageZone.GetComponent<DamageZone>();
            damageZone.damage = 3;

            yield return new WaitForSecondsRealtime(player.TimeToDeadScreen + 0.1f);

            Assert.AreEqual(0, Time.timeScale);
        }
               
        [UnityTest]
        public IEnumerator TestAddPlayerHP() {
            GameObject GO_player = CreatePlayer(0, 1, 0);
            Player player = GO_player.GetComponent<Player>();
            player.HP = 2;

            GameObject GO_bonus =
                MonoBehaviour.Instantiate(Resources.Load<GameObject>("Test/Bonus_HP 1"), new Vector3(0, 0, 0), Quaternion.identity);
            
            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(3, player.HP);
        }

        [UnityTest]
        public IEnumerator TestAddPlayerTime() {
            GameObject GO_player = CreatePlayer(0, 2, 0);
            Player player = GO_player.GetComponent<Player>();
            float startTimer = player.Timer;
            
            GameObject GO_bonus =
                MonoBehaviour.Instantiate(Resources.Load<GameObject>("Test/Bonus_Timer 1"), new Vector3(0, 0, 0), Quaternion.identity);

            yield return new WaitForSeconds(0.1f);

            Assert.IsTrue(player.Timer > startTimer);
        }

        [UnityTest]
        public IEnumerator TestDamageEnemy()
        {
            GameObject GO_enemy = CreateEnemy(0, 1, 0);
            Enemy enemy =  GO_enemy.GetComponent<Enemy>();

            GameObject GO_damageZone =
                    MonoBehaviour.Instantiate(Resources.Load<GameObject>("Test/DamageZone 1"), new Vector3(0, 0, 0), Quaternion.identity);

            DamageZone damageZone = GO_damageZone.GetComponent<DamageZone>();
            damageZone.targetTag = TargetTag.Enemy;

            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(2, enemy.HP);
        }
        
        [UnityTest]
        public IEnumerator TestDeadEnemy()
        {
            GameObject GO_enemy = CreateEnemy(0, 1, 0);

            GameObject GO_damageZone =
                    MonoBehaviour.Instantiate(Resources.Load<GameObject>("Test/DamageZone 1"), new Vector3(0, 0, 0), Quaternion.identity);

            DamageZone damageZone = GO_damageZone.GetComponent<DamageZone>();
            damageZone.targetTag = TargetTag.Enemy;
            damageZone.damage = 3;

            yield return new WaitForSeconds(1.1f);

            UnityEngine.Assertions.Assert.IsNull(GO_enemy);
        }


        private static GameObject CreatePlayer(float x, float y, float z)
        {
            GameObject GO_player = 
                MonoBehaviour.Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(x, y, z), Quaternion.identity);
            GO_player.AddComponent<Player>();
            GO_player.AddComponent<Rigidbody>();
            GO_player.tag = "Player";
            return GO_player;
        }
        private static GameObject CreateEnemy(float x, float y, float z)
        {
            GameObject GO_enemy = 
                MonoBehaviour.Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(x, y, z), Quaternion.identity);

            Enemy enemy = GO_enemy.AddComponent<Enemy>();
            GO_enemy.AddComponent<Rigidbody>();
            GO_enemy.tag = "Enemy";
            return GO_enemy;
        }

        [TearDown]
        public void Teardown()
        {
            Object[] allobjects = Object.FindObjectsOfType(typeof(GameObject));
            foreach (var obj in allobjects)
            {
                Object.Destroy(obj);
            }
        }
    }
}
