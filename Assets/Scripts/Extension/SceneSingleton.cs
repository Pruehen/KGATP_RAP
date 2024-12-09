using UnityEngine;

//�̱��� ���׸� Ŭ����. �̱������� ����� Ŭ�������� ��ӹ޾Ƽ� ���. �ٸ� ������ �Ѿ�� �������� ����.
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
                    Debug.LogWarning($"�̱��� ���� ����{nameof(T)}");
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