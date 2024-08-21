using UnityEngine;

namespace GameArchitect
{
    public class PlayerCamera : MonoBehaviour
    {
        public float RotationAngleX = 30f;
        public float Distance = 10f;
        public float OffsetY = 1f;

        [SerializeField] private Transform _following;

        public void Construct(GameObject following)
        {
            _following = following.transform;
        }

        void Start()
        {
            CameraTracking();
        }
        private void OnValidate()
        {
            CameraTracking();
        }

        private void LateUpdate()
        {
            CameraTracking();
        }

        private void CameraTracking()
        {
            if (_following == null)
                return;

            Quaternion rotation = Quaternion.Euler(RotationAngleX, 0, 0);
            Vector3 position = rotation * new Vector3(0, 0, -Distance) + FollowingPositionWithoutJumping();
            transform.rotation = rotation;
            transform.position = position;
        }


        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = _following.position;
            followingPosition.y += OffsetY;
            return followingPosition;
        }

        private Vector3 FollowingPositionWithoutJumping()
        {
            Vector3 followingPosition = new Vector3(_following.position.x, 0, _following.position.z);
            followingPosition.y += OffsetY;
            return followingPosition;
        }
    }
}