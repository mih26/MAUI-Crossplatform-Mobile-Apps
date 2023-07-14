using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R52_Last_Evidence_App.ViewModels
{
    public class DeviceViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int _id;
        public int DeviceId
        {
            get => this._id;
            set
            {
                this._id = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(DeviceId)));
            }
        }
        private string _name;
        public string DeviceName
        {
            get => this._name;
            set
            {
                this._name = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(DeviceName)));
            }
        }
        private DateTime _released;
        public DateTime ReleaseDate
        {
            get => this._released;
            set
            {
                this._released = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(ReleaseDate)));
            }
        }
        private decimal _price;
        public decimal Price
        {
            get => this._price;
            set
            {
                this._price = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(Price)));
            }
        }
        private bool _onSale;
        public bool OnSale
        {
            get => this._onSale;
            set
            {
                this._onSale = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(OnSale)));
            }
        }
        private string _picture;
        public string Picture
        {
            get => this._picture;
            set
            {
                this._picture = value;
              
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(Picture)));
            }
        }
        private string _remotePicture;
        public string RemotePicture
        {
            get => this._remotePicture;
            set
            {
                this._remotePicture = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(RemotePicture)));
            }
        }
    }
}
