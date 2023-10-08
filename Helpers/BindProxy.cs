using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleSSH.Helpers
{
    public class BindProxy : Freezable
    {
        protected override Freezable CreateInstanceCore() => new BindProxy();
        public object Data
        {
            get => GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
            nameof(Data),
            typeof(object),
            typeof(BindProxy));
    }
}
