//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdivinaQue.Host.DatabaseAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Participate
    {
        public int IdPlayer { get; set; }
        public int score { get; set; }
        public int IdGame { get; set; }
    
        public virtual Game Game { get; set; }
        public virtual Players Players { get; set; }
    }
}
