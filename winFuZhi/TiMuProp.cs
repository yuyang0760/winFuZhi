using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winFuZhi
{

    public class TiMuProp : INotifyPropertyChanged
    {
        public TiMuProp(string tiHao, string laiYuan)
        {
            TiHao = tiHao;
            LaiYuan = laiYuan;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string TiHao { get; set; }
        public string LaiYuan { get; set; }
    }

}
