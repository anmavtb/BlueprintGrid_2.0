public class CameraViewButton : GenericButton
{
    protected override void Behaviour()
    {
        GameCamera.Instance.ChangeView();
        CameraChangeView.Instance.SwitchButtons();
    }
}