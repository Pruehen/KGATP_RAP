using UnityEngine;

public class CamMove : MonoBehaviour
{
    [SerializeField] Transform Trf_Cam;

    [Range(0.1f, 10f)][SerializeField] float CamSpeed = 2;
    [Range(1, 50f)][SerializeField] float CamDistance = 10;
    [Range(5f, 85f)][SerializeField] float CamAngle_X = 45;
    [Range(0, 360)][SerializeField] float CamAngle_Y = 0;
    Transform _targetObejct;

    // Start is called before the first frame update
    void Start()
    {
        if(_targetObejct == null)
        {
            _targetObejct = Player.Instance.transform;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(Trf_Cam != null)
        {
            Trf_Cam.localPosition = new Vector3(0, 0, -CamDistance);
        }

        if(_targetObejct != null)
        {
            Vector3 targetVec = _targetObejct.position;
            Quaternion targetRot = Quaternion.Euler(CamAngle_X, CamAngle_Y, 0);
            this.transform.position = Vector3.Lerp(this.transform.position, targetVec, Time.deltaTime * CamSpeed);
            this.transform.rotation = targetRot;
        }
    }
}
