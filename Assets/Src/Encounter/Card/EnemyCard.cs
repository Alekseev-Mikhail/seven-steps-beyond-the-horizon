using Utils;

namespace Encounter.Card
{
    public class EnemyCard : BaseCard
    {
        public override void Play(TurnTakerData[] data)
        {
            base.Play(data);
            Ability.Play(data);
        }
    }
}