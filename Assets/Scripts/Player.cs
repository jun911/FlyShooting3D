using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("FX, SFX")]
    [SerializeField] ParticleSystem fxExplotion;
    [SerializeField] AudioClip sfxExplotion;

    [SerializeField] PlayableDirector masterTimeline;
    [SerializeField] float timeForReloadLevel = 3f;


    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartPlayerCrashSequence()
    {
        StopMovement();
        StopTimeline();
        PlayCrashEffect();
        HidePlayerShip();
        Invoke("ReloadLevel", 3f);
    }

    private void HidePlayerShip()
    {
        transform.Find("default").GetComponent<MeshRenderer>().enabled = false;
    }

    private void StopTimeline()
    {
        masterTimeline.Stop();
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void PlayCrashEffect()
    {
        fxExplotion.Play();
        audioSource.volume = 0.1f;
        audioSource.PlayOneShot(sfxExplotion);
    }

    void StopMovement()
    {
        gameObject.GetComponent<PlayerControls>().enabled = false;
    }
}