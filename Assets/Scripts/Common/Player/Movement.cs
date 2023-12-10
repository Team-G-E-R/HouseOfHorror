using UnityEngine;

namespace Common.Scripts
{
    [RequireComponent(typeof(Activator))]
    public class movement : MonoBehaviour
    {

        #region Inspector
        
        [SerializeField] private float speed = 100f;
        [SerializeField] private float sprintSpeedBonus = 50f;

        [Header("Relations")]
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody physicsBody = null;

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

            _movement = (transform.right * horizontal + transform.forward * vertical).normalized;
        }

        private void FixedUpdate()
        {
            if (_movementLocked)
                return;

            float sprint = _sprintLocked == false && Input.GetKey(KeyCode.LeftShift) ? sprintSpeedBonus : 0;
            physicsBody.velocity = _movement * (speed + sprint) * Time.fixedDeltaTime;
            physicsBody.AddForce(Physics.gravity * (physicsBody.mass * physicsBody.mass));
        }

        public void SetSprint(bool sprint) => _sprintLocked = !sprint;
        public void SetWalk(bool walk) => _movementLocked = !walk;

        public void TurnOffMovement()
        {
            GetComponent<Activator>().enabled = false;
            this.enabled = false;
        }
        
        public void TurnOnMovement()
        {
            GetComponent<Activator>().enabled = true;
            this.enabled = true;
        }
        
        #endregion
    }
}
