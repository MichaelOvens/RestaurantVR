using UnityEngine;

namespace Interaction.UI
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(BoxCollider))]
    public class UIBoxCollider : MonoBehaviour
    {
        public BoxCollider Collider { get; private set; }
        public RectTransform RectTransform { get; private set; }

        private float _previousWidth;
        private float _previousHeight;
        private Vector2 _previousPivot;

        private void Awake()
        {
            Collider = GetComponent<BoxCollider>();
            RectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            float width = RectTransform.rect.width;
            float height = RectTransform.rect.height;
            Vector2 pivot = RectTransform.pivot;

            if (!Mathf.Approximately(width, _previousWidth)
                || !Mathf.Approximately(height, _previousHeight)
                || !Mathf.Approximately(pivot.x, _previousPivot.x)
                || !Mathf.Approximately(pivot.y, _previousPivot.y))
            {
                Collider.center = new Vector3()
                {
                    x = Mathf.LerpUnclamped(width / 2f, -width / 2f, pivot.x),
                    y = Mathf.LerpUnclamped(height / 2f, -height / 2f, pivot.y)
                };

                Collider.size = new Vector3()
                {
                    x = width,
                    y = height,
                    z = 0.01f
                };
            }
            
            _previousWidth = width;
            _previousHeight = height;
            _previousPivot = pivot;
        }
    }
}
