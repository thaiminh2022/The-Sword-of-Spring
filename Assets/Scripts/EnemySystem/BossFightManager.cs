using UnityEngine;
using TheSwordOfSpring.HealthSystemTM;
using System;
using TheSwordOfSpring.SceneSystem;
using Cinemachine;
using Redcode.Extensions;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
using System.Collections;
using TheSwordOfSpring.Misc;

namespace TheSwordOfSpring.EnemySystem
{

    public class BossFightManager : MonoBehaviour
    {
        [SerializeField] bool debugMode = false;

        [Header("Transform")]
        [SerializeField]
        private Transform boss;
        [SerializeField]
        private Transform player;

        [Header("Cameras")]
        [SerializeField]
        private CinemachineVirtualCamera mainCam;

        [SerializeField]
        private CinemachineVirtualCamera startCam;

        [SerializeField]
        private CinemachineVirtualCamera bossCam;

        [Header("Effects")]
        [SerializeField] Transform boomEffects;
        [SerializeField] Transform fireEffects;
        [SerializeField] GameObject bossStartMap;
        [SerializeField] GameObject propsHolder;

        [SerializeField] GameObject P2Volume;




        Vector2 initialPosition;
        HealthSystem enemyHealthSystem;
        HealthSystem playerHealthSystem;

        private bool halfHeal = false;
        private bool p2Invoked = false;


        private void Start()
        {
            initialPosition = Vector3.zero;
            HealthSystem.TryGetHealthSystem(boss.gameObject, out enemyHealthSystem);
            HealthSystem.TryGetHealthSystem(player.gameObject, out playerHealthSystem);

            enemyHealthSystem.OnDamaged += HealthSystem_OnDamaged;
            enemyHealthSystem.OnDead += HealthSystem_OnDead;


            if (debugMode)
            {
                boss.GetComponent<ITriggerable>().Trigger();

                return;
            }
            StartBoss();
            Invoke(nameof(BossComeDown), 5f);
            Invoke(nameof(ZoomToBoss), 9f);
            Invoke(nameof(TriggerBossFight), 11f);
            Invoke(nameof(BossPhase2), 36f);

        }
        private void HealthSystem_OnDamaged(object sender, EventArgs args)
        {
            if (enemyHealthSystem.GetHealthNormalized() < .5f && halfHeal == false)
            {
                // Heal character to full
                playerHealthSystem?.HealComplete();
                halfHeal = true;
            }

            if (enemyHealthSystem.GetHealthNormalized() < .25f && p2Invoked == false)
            {
                BossPhase2();
            }
        }
        private void HealthSystem_OnDead(object sender, EventArgs args)
        {
            StartCoroutine(BossDead());
        }

        private IEnumerator BossDead()
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject go = Instantiate(PSStaticHolder.Instance.Explosion_PS, boss.position, Quaternion.identity);
                yield return new WaitForSeconds(.5f);
            }

            yield return new WaitForSeconds(1f);
            P2Volume.SetActive(false);
            ScenesManager.SwitchScene(SceneNames.VictoryScene);
        }

        private void OnDestroy()
        {
            enemyHealthSystem.OnDamaged -= HealthSystem_OnDamaged;
        }

        //! Special effects 
        private void StartBoss()
        {
            boss.transform.SetPositionY(100f);

            startCam.gameObject.SetActive(true);

            mainCam.gameObject.SetActive(false);
            bossCam.gameObject.SetActive(false);

            boomEffects.gameObject.SetActive(false);
            fireEffects.gameObject.SetActive(false);
            P2Volume.SetActive(false);

            bossStartMap.LeanAlpha(0, .01f);
            propsHolder.LeanAlpha(0, .01f);

            enemyHealthSystem.SetInvincible(true);

        }
        private void BossComeDown()
        {
            boss.transform.LeanMoveLocal(initialPosition, 1f).setEaseInCirc().setOnComplete(() =>
            {
                // Boom and shit;
                boomEffects.gameObject.SetActive(true);
                fireEffects.gameObject.SetActive(true);

                bossStartMap.LeanAlpha(1, .2f).setEaseInOutCirc();
                propsHolder.LeanAlpha(1, .3f).setEaseInOutSine();
            });
        }
        private void ZoomToBoss()
        {
            SetBossCam();
        }
        private void TriggerBossFight()
        {
            SetMainCam();

            fireEffects.gameObject.LeanScale(Vector3.zero, 1f).setEaseOutCirc().setOnComplete(() =>
            {

                enemyHealthSystem.SetInvincible(false);
                boss.GetComponent<ITriggerable>().Trigger();
            });

        }

        private void BossPhase2()
        {
            if (p2Invoked == true)
            {
                return;
            }

            p2Invoked = true;

            boss.GetComponent<ITriggerable>().StopTrigger();
            boss.GetComponent<ITriggerP2>().TriggerP2(true);

            SetBossCam();

            enemyHealthSystem.SetInvincible(true);
            enemyHealthSystem.HealComplete();

            boss.LeanMoveLocal(initialPosition, 3f).setEaseLinear().setOnComplete(() =>
            {

                SetStartCam();
                StartCoroutine(BossPlayer2Start());
            });


        }
        IEnumerator BossPlayer2Start()
        {
            for (int i = 0; i < 50; i++)
            {
                Vector2 position = Random.insideUnitCircle * 10f;

                GameObject go = Instantiate(PSStaticHolder.Instance.Explosion_PS, position, Quaternion.identity);
                go.transform.localScale *= 3f;
                Destroy(go, .2f);

                yield return new WaitForSeconds(.15f);
            }


            P2Volume.SetActive(true);
            SetMainCam();
            enemyHealthSystem.SetInvincible(false);
            boss.GetComponent<ITriggerable>().Trigger();




        }

        private void SetStartCam()
        {
            mainCam.gameObject.SetActive(false);
            startCam.gameObject.SetActive(true);
            bossCam.gameObject.SetActive(false);

        }
        private void SetMainCam()
        {
            mainCam.gameObject.SetActive(true);
            startCam.gameObject.SetActive(false);
            bossCam.gameObject.SetActive(false);

        }
        private void SetBossCam()
        {
            mainCam.gameObject.SetActive(false);
            startCam.gameObject.SetActive(false);
            bossCam.gameObject.SetActive(true);

        }



    }
}