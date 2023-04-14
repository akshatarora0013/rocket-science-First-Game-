using UnityEngine;
using UnityEngine.SceneManagement;


public class Collisionhandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip collision;

    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem collisionParticle;

    AudioSource myaudioSource;

    bool isTransitioning = false;
    bool collisiondisable = false;

    void Start() {
        myaudioSource = GetComponent<AudioSource>();
    }  
    
    void Update() {
        RespondToDebugKeys();
    }

    void OnCollisionEnter(Collision other) {
        if(isTransitioning || collisiondisable){
            return;
        }

        switch (other.gameObject.tag){
            case "Friendly":
                Debug.Log("This is friendly");
                break;
            case "Fuel":
                Debug.Log("This is a fuel Station");
                break;
            case "Finish":
                // Debug.Log("This is the end of the level");
                successSequence();
                break;
            default:
                // Debug.Log("Sorry! you blew up");
                crashSequence();
                break;
        }    
    }

    void crashSequence(){
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        myaudioSource.Stop();
        collisionParticle.Play();
        myaudioSource.PlayOneShot(collision);
        Invoke("ReloadLevel" , levelLoadDelay);
    }

    void successSequence(){
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        myaudioSource.Stop();
        successParticle.Play();
        myaudioSource.PlayOneShot(success);
        Invoke("LoadNextLevel" , levelLoadDelay);
    }

    void ReloadLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void RespondToDebugKeys(){
        if(Input.GetKey(KeyCode.L)){
            LoadNextLevel();
        }
        if(Input.GetKeyDown(KeyCode.C)){
            collisiondisable = !collisiondisable;
        }
    }
}
