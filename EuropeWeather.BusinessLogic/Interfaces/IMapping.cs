using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuropeWeather.BusinessLogic.Interfaces
{
    public interface IMapping<TFrom, TTo>
    {
        TTo To(TFrom source);

        TFrom From(TTo destination);
    }
}
