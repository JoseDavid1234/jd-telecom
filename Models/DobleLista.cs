using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDTelecomunicaciones.Models
{
    public class DobleLista<T, S>
    {
        public List<T> ModelosT { get; set; }
        public List<S> ModelosS { get; set; }

    public DobleLista(List<T> modelosT, List<S> modelosS)
    {
        ModelosT = modelosT;
        ModelosS = modelosS;
    }
    }
}