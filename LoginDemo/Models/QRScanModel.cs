using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LoginDemo.Models
{
    public class QRScanModel: INotifyPropertyChanged
    {
        private bool _isScan;
        public bool IsScan
        {
            get { return _isScan; }
            set {
                if (_isScan!=value)
                {
                    _isScan = value;
                    if (PropertyChanged!=null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("IsScan"));
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public QRScanModel()
        {
            CreateTime = DateTime.Now;
        }
        public Guid Id { get; set; }
        public string Qr { get; set; }
        public DateTime QrGenerateTime { get; set; }
        public DateTime ExpiredTime { get; set; }
        public DateTime CreateTime { get; private set; }
        public System.Timers.Timer Timer { get; set; }
        public bool IsComfirm { get; set; }
    }
}