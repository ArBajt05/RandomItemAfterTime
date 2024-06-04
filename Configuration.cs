using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomItemAfterTime
{
    public class Configuration : IRocketPluginConfiguration
    {
        public float TimeToGiveItem;
        public byte Amount;
        public ushort[] Items { get; set; }

        public void LoadDefaults()
        {
            TimeToGiveItem = 10f;
            Amount = 1;
            Items = new ushort[] { 81, 1050, 1322 }; 
        }
    }
}
