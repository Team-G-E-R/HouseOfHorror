using System;
using UnityEngine;

namespace Common.Scripts
{
    [RequireComponent(typeof(Activator), typeof(CharacterController), typeof(Animator))]
    public class movement : MonoBehaviour
    {

        #region Inspector
        
        [SerializeField] private float speed = 100f;
        [SerializeField] private float sprintSpeedBonus = 50f;
        [SerializeField] private float _mass = 1f;

        [Header("Relations")]
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterController _charContr;

        private AudioSource _audioSource;
        [HideInInspector]
        public float _lastHorisontalInput = 0;
        [HideInInspector]
        public float _lastVerticalInput = 0;
        public SaveLoad.GameInfo GameData => SaveLoad.Instance.PlayerData;
        

        #endregion


        #region Fields

        private Vector3 _movement;
        private bool _movementLocked;
        private bool _sprintLocked;

        

        #endregion


        #region MonoBehaviour

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = GameData.Volume;
        }

        private void Update()
        {
            if (_movementLocked)
                return;
            float vertical = Input.GetAxisRaw("Vertical");
            float horizontal = Input.GetAxisRaw("Horizontal");
            if(vertical == 0 && horizontal == 0)
            {
                animator.SetFloat("Horizontal", _lastHorisontalInput/2);
                animator.SetFloat("Vertical", _lastVerticalInput/2);
                
            }
            else if ((Math.Abs(vertical) > 0.5f) | (Math.Abs(horizontal) > 0.5f))
            {
                animator.SetFloat("Horizontal", horizontal);
                animator.SetFloat("Vertical", vertical);
                _lastHorisontalInput = horizontal;
                _lastVerticalInput = vertical;
            }
            /* animator.SetFloat("Horizontal", horizontal);
            animator.SetFloat("Vertical", vertical);*/
            animator.SetFloat("Speed", _movement.sqrMagnitude);
            _movement = (transform.right * horizontal + transform.forward * vertical).normalized;
        }

        private void FixedUpdate()
        {
            if (_movementLocked)
                return;
            if (_movement != Vector3.zero)
            {
                float sprint = _sprintLocked == false && Input.GetKey(KeyCode.LeftShift) ? sprintSpeedBonus : 0;
                _charContr.Move(_movement * (speed + sprint) * Time.fixedDeltaTime);   
            }
            if (!IsGrounded())
            {
                _charContr.Move(-transform.up * _mass);
            }
        }

        private bool IsGrounded()
        {
            return Physics.Raycast(transform.position, -Vector3.up, 0.01f);
        }

        public void SetSprint(bool sprint) => _sprintLocked = !sprint;

        public void SetWalk(bool walk) 
        { 
            _movementLocked = !walk;
            GetComponent<Animator>().enabled = walk;
        }

        public void TurnOffMovement()
        {
            GetComponent<Activator>()._isInRange = false;
            _movementLocked = true;
            animator.SetFloat("Horizontal", _lastHorisontalInput / 2);
            animator.SetFloat("Vertical", _lastVerticalInput / 2);
        }

        public void StopPlayer()
        {

        }
        
        public void TurnOnMovement()
        {
            _movementLocked = false;
        }


        public void StepSoundPlay()
        {
            if (_audioSource.isPlaying)
            {
                return;
            }
            _audioSource.PlayOneShot(_audioSource.clip);
        }

        public void StepSoundStop()
        {
            _audioSource?.Stop();
        }
        #endregion
    }
}
