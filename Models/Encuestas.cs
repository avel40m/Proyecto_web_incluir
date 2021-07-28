//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Inluirme_Proyecto.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Encuestas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Encuestas()
        {
            this.Preguntas = new HashSet<Preguntas>();
        }
    
        public int id { get; set; }
        public Nullable<int> Empresa_id { get; set; }
        public Nullable<int> Universidad_id { get; set; }
        public Nullable<int> Usuario_id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public Nullable<double> clasificacion { get; set; }
    
        public virtual Empresas Empresas { get; set; }
        public virtual Universidades Universidades { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Preguntas> Preguntas { get; set; }
    }
}