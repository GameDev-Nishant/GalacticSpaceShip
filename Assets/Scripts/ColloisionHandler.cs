using UnityEngine;
using UnityEngine.SceneManagement;

public class ColloisionHandler : MonoBehaviour
{
    [SerializeField] float delayTime = 2f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;


   
    AudioSource audioSource;
    bool isTransiting = false;
    bool collisionDisabled = false;

    void Start()
    {
       audioSource= GetComponent<AudioSource>();
       
    }

     void Update()
     {
        RespondToDebugKeys();
     }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;             //To Toggle Collision
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransiting || collisionDisabled) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is freindly");
                break;
            case "Finish":
                StartSuccessSequence(); 
                break;
            default:
                StartCrashSequence();
                
                break;
        }

    }

         void StartCrashSequence()
         {
             isTransiting = true;
             audioSource.Stop();

             audioSource.PlayOneShot(crash);
             crashParticles.Play();      
             GetComponent<Movement>().enabled=false;
             Invoke("ReloadLevel", delayTime);      
         }
        
        void StartSuccessSequence()
        {
             isTransiting = true;
             audioSource.Stop();
             GetComponent<Movement>().enabled = false;
             Invoke("LoadNextLevel", delayTime);
             audioSource.PlayOneShot(success);
             successParticles.Play();
        }
        
        void LoadNextLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
            SceneManager.LoadScene(nextSceneIndex);
        }

        void ReloadLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
       
     

}
