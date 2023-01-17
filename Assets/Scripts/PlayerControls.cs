using System;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]

    [Header("비행기 화면 위치 조정")]
    [Tooltip("좌우 폭 고정")][SerializeField] private float clampedPosX;
    [SerializeField] private float clampedPosy;
    [SerializeField] private float xRange = 7f;
    [SerializeField] private float yRange = 4.5f;
    [SerializeField] float positionPitchFator = 15f;
    [SerializeField] float controlPitchFactor = 15f;
    [SerializeField] float positionYawFator = 15f;
    [SerializeField] float controlYawFactor = 15f;
    [SerializeField] float positionRollFator = 15f;
    [SerializeField] float controlRollFactor = 15f;

    [Header("비행기 조정")]
    [Tooltip("비행기 상하좌우 움직임 속도 조정")][SerializeField] private float controlSpeed = 10f;

    [Header("오브젝트 삽입")]
    [SerializeField] GameObject[] lasers;
    [SerializeField] AudioClip sfxLaser;

    AudioSource audioSource;

    float inputX;
    float inputY;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ProcessControl();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessFiring()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    private void SetLasersActive(bool isActive)
    {
        PlayerSoundLaser(isActive);

        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

    private void PlayerSoundLaser(bool isActive)
    {
        if(isActive)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(sfxLaser);
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localRotation.x * positionPitchFator;
        float pitchDueToControl = -inputY * controlPitchFactor;
        float yawDueToPosition = transform.localRotation.y * positionYawFator;
        float yawhDueToControl = -inputX * controlYawFactor;
        float rollhDueToPosition = transform.localRotation.z * positionRollFator;
        float rollDueToControl = -inputX * controlRollFactor;

        float pitch = pitchDueToPosition + pitchDueToControl;
        float yaw = yawDueToPosition + yawhDueToControl;
        float roll = rollhDueToPosition + rollDueToControl;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessControl()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        float offsetX = inputX * controlSpeed * Time.deltaTime;
        float offsetY = inputY * controlSpeed * Time.deltaTime;
        float rawPosX = transform.localPosition.x + offsetX;
        float rawPosY = transform.localPosition.y + offsetY;

        clampedPosX = Mathf.Clamp(rawPosX, -xRange, xRange);
        clampedPosy = Mathf.Clamp(rawPosY, -yRange, yRange);

        transform.localPosition = new Vector3(clampedPosX, clampedPosy, 0);
    }
}