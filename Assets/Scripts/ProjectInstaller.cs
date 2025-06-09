using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ProjectInstaller", menuName = "Installers/ProjectInstaller")]
public class ProjectInstaller : ScriptableObjectInstaller<ProjectInstaller>
{
    [SerializeField] private JumpConfig jumpConfig;
    [SerializeField] private LinearConfig linearConfig;
    [SerializeField] private SpawnConfig spawnConfig;
    
    public override void InstallBindings()
    {
        Container.Bind<SpawnConfig>().FromInstance(spawnConfig).AsSingle();
        Container.Bind<JumpConfig>().FromInstance(jumpConfig).AsSingle();
        Container.Bind<LinearConfig>().FromInstance(linearConfig).AsSingle();

        Container.Bind<GameModel>().AsSingle();
        
        Container.Bind<FoodChainController>().AsSingle();
        Container.Bind<ObjectPool<AnimalView>>().AsSingle();
    }
}
