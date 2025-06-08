using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ProjectInstaller", menuName = "Installers/ProjectInstaller")]
public class ProjectInstaller : ScriptableObjectInstaller<ProjectInstaller>
{
    [SerializeField] private JumpConfig jumpConfig;
    [SerializeField] private LinearConfig linearConfig;
    
    public override void InstallBindings()
    {
        Container.Bind<JumpConfig>().FromInstance(jumpConfig).AsSingle();
        Container.Bind<LinearConfig>().FromInstance(linearConfig).AsSingle();
    }
}
