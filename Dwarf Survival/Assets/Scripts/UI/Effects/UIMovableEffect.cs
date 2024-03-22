using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Asigaka.UI
{
    public class UIMovableEffect : PoolableObject
    {
        [SerializeField] private Image img;
        [SerializeField] private RectTransform rect;

        private float moveSpeed;
        private string endTargetKey;

        public string EndTargetKey { get => endTargetKey; }

        public void SetData(Vector3 startPos, string endTargetKey, float moveSpeed, Sprite sprite)
        {
            rect.anchoredPosition = startPos;
            this.endTargetKey = endTargetKey;
            this.moveSpeed = moveSpeed;
            img.sprite = sprite;
        }

        /// <returns>Если долетел - true</returns>
        public bool UpdateMove(Vector3 endPos)
        {
            Vector3 moveDir = endPos - rect.position;
            moveDir.Normalize();
            rect.position += moveDir * moveSpeed;

            if (Vector3.Distance(rect.position, endPos) <= 4)
            {
                return true;
            }

            return false;
        }
    }
}