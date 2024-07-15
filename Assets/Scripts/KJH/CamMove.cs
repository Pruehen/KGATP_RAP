using UnityEngine;

public class CamMove : MonoBehaviour
{
    [SerializeField] Transform targetObejct;

    Vector3 localPos;
    // Start is called before the first frame update
    void Start()
    {
        localPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(targetObejct != null)
        {
            Vector3 targetVec = targetObejct.position + localPos;
            this.transform.position = Vector3.Lerp(this.transform.position, targetVec, Time.deltaTime * 2);            
        }
    }
}
