using UnityEngine;

public class RotationFaceIconViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("RotationFaceIconView").GetComponent<RotationFaceIconView>();
        Debug.Assert(View != null);
        Presenter = new RotationFaceIconPresenter();
        Debug.Assert(Presenter != null);
    }
}
