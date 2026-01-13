using System.Collections.Generic;

namespace Client 
{
    struct DamageContainerComponent 
    {
        public List<TakeDamageData> DamageData; 

        public float GetTotalDamage()
        {
            float totalDamage = 0f;

            if (DamageData == null) return 0f;

            foreach (var damage in DamageData)
            {
                totalDamage += damage.Value;
            }

            return totalDamage;
        }

        public void Clear()
        {
            DamageData?.Clear();
        }
    }
}
