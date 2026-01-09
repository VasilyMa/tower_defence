using UnityEngine;
using UnityEngine.SceneManagement;

namespace Statement
{
    public class InitState : State
    { 
        public static new InitState Instance
        {
            get
            {
                return (InitState)State.Instance;
            }
        }
#if UNITY_EDITOR
        [UnityEngine.SerializeField] UnityEditor.SceneAsset TargetScene;
#endif
        [SerializeField] private string targetSceneName; 
        public override void Awake()
        {
        }
        public override void Start()
        { 
            UIModule.Initialize();

            ConfigModule.Initialize(this, onConfigLoaded); 
        } 

        public override void Update()
        {

        }
        public override void FixedUpdate()
        {

        }
        public override void LateUpdate()
        {
            
        }

        void onConfigLoaded()
        { 

        }

        void onPlayerLoaded(bool result)
        {
            if (result)
            {
                SceneManager.LoadScene(1);
            }
            else
            { 
                Application.Quit();
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (TargetScene)
            {
                targetSceneName = TargetScene.name;
            }
        }
#endif
    }
}