using UnityEngine;

public class CamMove : MonoBehaviour
{
    [Range(0.1f, 10f)][SerializeField] float CamSpeed = 2;
    [Range(1, 50f)][SerializeField] float CamDistance = 10;

    Transform _targetObejct;

    Vector3 _viewDir;
    // Start is called before the first frame update
    void Start()
    {
        _viewDir = this.transform.position.normalized;
        if(_targetObejct == null)
        {
            _targetObejct = Player.Instance.transform;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(_targetObejct != null)
        {
            Vector3 targetVec = _targetObejct.position + _viewDir * CamDistance;
            this.transform.position = Vector3.Lerp(this.transform.position, targetVec, Time.deltaTime * CamSpeed);            
        }
    }
}
