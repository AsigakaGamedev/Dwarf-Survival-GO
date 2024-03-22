using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asigaka.UI
{
    public class UIEffectsManager : MonoBehaviour, IInitListener, IUpdateListener, IDeinitListener
    {
        [SerializeField] private Transform effectsContent;
        [SerializeField] private SerializedDictionary<string, RectTransform> effectTargets;

        [Header("Movable Effects")]
        [SerializeField] private UIMovableEffect movableEffectPrefab;

        [Space]
        [ReadOnly, SerializeField] private List<UIMovableEffect> activeMovableEffects;

        private ObjectPoolingManager poolingManager;
        private Camera mainCamera;

        public static UIEffectsManager Instance;

        public void OnInitialize()
        {
            Instance = this;

            poolingManager = ServiceLocator.GetService<ObjectPoolingManager>();
            mainCamera = Camera.main;

            activeMovableEffects = new List<UIMovableEffect>();
        }

        public void OnUpdate()
        {
            for (int i = 0; i < activeMovableEffects.Count; i++)
            {
                UIMovableEffect effect = activeMovableEffects[i];
                if (effect.UpdateMove(effectTargets[effect.EndTargetKey].position))
                {
                    activeMovableEffects.RemoveAt(i);
                    effect.gameObject.SetActive(false);
                }
            }
        }

        public void OnDeinitialize()
        {
            Instance = null;
        }

        public void ShowMovableEffect(Vector3 spawnPos, string targetKey, Sprite effectSprite)
        {
            UIMovableEffect newEffect = poolingManager.GetPoolable(movableEffectPrefab);
            newEffect.transform.SetParent(effectsContent);
            newEffect.transform.localScale = Vector3.one;
            newEffect.SetData(mainCamera.WorldToScreenPoint(spawnPos), targetKey, 9, effectSprite);
            activeMovableEffects.Add(newEffect);
        }
    }
}