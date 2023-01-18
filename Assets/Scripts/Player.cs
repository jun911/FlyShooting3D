using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("FX, SFX")]
    [SerializeField] private GameObject fxExplotion;
    [SerializeField] private PlayableDirector masterTimeline;
    [SerializeField] private float timeForReloadLevel = 3f;

    private AudioSource audioSource;
    private Life life;
    GameObject parentGameObject;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        life = FindObjectOfType<Life>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
    }

    public void StartPlayerCrashSequence()
    {
        life.LoseLife();
        StopMovement();
        StopTimeline();
        PlayCrashEffect();
        HidePlayerShip();
        Invoke("ReloadLevel", 3f);
    }

    private void HidePlayerShip()
    {
        transform.Find("default").GetComponent<MeshRenderer>().enabled = false;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
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
        GameObject fx = Instantiate(fxExplotion, transform.position, Quaternion.identity);
        fx.transform.parent = parentGameObject.transform;
    }

    private void StopMovement()
    {
        gameObject.GetComponent<PlayerControls>().enabled = false;
    }
}