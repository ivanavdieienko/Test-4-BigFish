using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    [SerializeField]
    private GameSettings settings;

    [SerializeField]
    private GameSounds sounds;

    public override void InstallBindings()
    {
        Container.BindInstance(settings).AsSingle();
        Container.BindInstance(sounds).AsSingle();
    }
}