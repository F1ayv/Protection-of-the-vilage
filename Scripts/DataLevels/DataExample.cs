using UnityEngine;

namespace DataLevels
{
    [CreateAssetMenu(fileName = "Level ", menuName = "DataForLevel", order = 51)]
    public class DataExample : ScriptableObject
    {
        [SerializeField] private int[] mobs;
        [SerializeField] private int[] startFastSpawn;
        [SerializeField] private int[] theEndFastSpawn;
        [SerializeField] private int[] ratioFastSpawn;
        
        public int[] Mobs
        {
            get { return mobs; }
        }


        public int[] StartFastSpawn
        {
            get { return startFastSpawn; }
        }

        public int[] TheEndFastSpawn
        {
            get { return theEndFastSpawn; }
        }

        public int[] RatioFastSpawn
        {
            get { return ratioFastSpawn; }
        }
    }
}
