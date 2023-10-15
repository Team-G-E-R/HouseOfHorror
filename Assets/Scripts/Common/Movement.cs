using UnityEngine;

namespace Common.Scripts
{
    public class movement : MonoBehaviour
    {

        #region Inspector

        [SerializeField] private KeyCode _sprintKey = KeyCode.LeftShift;
        [SerializeField] private float speed = 100f;
        [SerializeField] private float sprintSpeedBonus = 50f;

        [Header("Relations")]
        public Animator animator;

        [SerializeField]
        private Rigidbody physicsBody = null;

        [SerializeField]
        private SpriteRenderer spriteRenderer = null;

        #endregion


        #region Fields

        private Vector3 _movement;
        private bool _movementLocked;
        private bool _sprintLocked;

        #endregion


        #region MonoBehaviour

        private void Update()
        {
            float vertical = Input.GetAxisRaw("Vertical");
            float horizontal = Input.GetAxisRaw("Horizontal");

            animator.SetFloat("Horizontal", horizontal);
            animator.SetFloat("Vertical", vertical);
            animator.SetFloat("Speed", _movement.sqrMagnitude);

            //if (horizontal > 0)
            //    spriteRenderer.flipX = false;
            //else if (horizontal < 0)
            //    spriteRenderer.flipX = true;

            _movement = new Vector3(horizontal, 0, vertical).normalized;
        }

        private void FixedUpdate()
        {
            if (_movementLocked)
                return;

            float sprint = _sprintLocked == false && Input.GetKey(_sprintKey) ? sprintSpeedBonus : 0;
            physicsBody.velocity = _movement * (speed + sprint) * Time.fixedDeltaTime;
        }

        public void SetSprint(bool sprint) => _sprintLocked = !sprint;
        public void SetWalk(bool walk) => _movementLocked = !walk;

        #endregion
    }
}
