using TMPro;
using UnityEngine;
using Zenject;

public class DeadAnimalCountView : MonoBehaviour
{
   [SerializeField] private FoodChainType type;
   [SerializeField] private TMP_Text counterText;
   [Inject] private GameModel gameModel;

   private void Awake()
   {
      gameModel.OnAnimalEat += (_,_) => Redraw();
   }

   private void OnEnable() => Redraw();

   private void Redraw()
   {
      var count = type == FoodChainType.Predator ? gameModel.DeadPredators : gameModel.DeadPreys;
      counterText.text = $"Dead {type}: {count}";
   }
}
