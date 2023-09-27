using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDTelecomunicaciones.Models
{
    public class ModeloConListas<T, S>
    {

    public T ModeloT { get; set; }
    public List<S> ModelosS { get; set; }

    public ModeloConListas(T modeloT, List<S> modelosS)
    {
        ModeloT = modeloT;
        ModelosS = modelosS;
    }
}
}