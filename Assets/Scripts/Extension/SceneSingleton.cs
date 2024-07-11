using UnityEngine;

//싱글톤 제네릭 클래스. 싱글톤으로 사용할 클래스에서 상속받아서 사용. 다른 씬으로 넘어가면 유지되지 않음.
public class SceneSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                //DontDestroyOnLoad(instance.gameObject);

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<T>();
                    Debug.LogWarning($"싱글톤 씬에 없음{nameof(T)}");
                }
            }

            return instance;
        }
    }
}
public class GlobalSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<T>();
                }
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }
}