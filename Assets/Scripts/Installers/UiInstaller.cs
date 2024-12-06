using UnityEngine;
using Zenject;

public class UiInstaller : MonoInstaller<UiInstaller>
{
    private const string TransformGroup = "UI";
    [SerializeField]
    private AddMoneyPopup addMoneyPopupPrefab;

    public override void InstallBindings()
    {
        Container.BindFactory<AddMoneyPopup, AddMoneyPopup.Factory>()
            .FromComponentInNewPrefab(addMoneyPopupPrefab)
            .UnderTransformGroup(TransformGroup);

        Container.DeclareSignal<ShowAddMoneyPopupSignal>();

        Container.Bind<ShowAddMoneyPopupCommand>().AsTransient();

        Container.BindSignal<ShowAddMoneyPopupSignal>()
            .ToMethod<ShowAddMoneyPopupCommand>(cmd => cmd.Execute)
            .FromResolve();
    }
}