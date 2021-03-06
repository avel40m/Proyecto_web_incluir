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
    using System.ComponentModel.DataAnnotations;

    public partial class Empresas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Empresas()
        {
            this.Capacitaciones = new HashSet<Capacitaciones>();
            this.Encuestas = new HashSet<Encuestas>();
        }
    
        public int id { get; set; }

        [Required(ErrorMessage = "El campo nombre es requerido")]
        [Display(Name = "Ingrese su nombre")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "El campo provincia es requerido")]
        [Display(Name = "Ingrese la provincia")]
        public string provincia { get; set; }

        [Required(ErrorMessage = "El campo ciudad es requerido")]
        [Display(Name = "Ingrese ciudad")]
        public string ciudad { get; set; }

        [Required(ErrorMessage = "El campo domicilio es requerido")]
        [Display(Name = "Ingrese su domicilio")]
        public string domicilio { get; set; }

        [Required(ErrorMessage = "El campo correo electronico es requerido")]
        [Display(Name = "Ingrese su correo electronico")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Correo electronico invalido")]
        public string mail { get; set; }

        [Required(ErrorMessage = "El campo usuario es requerido")]
        [Display(Name = "Ingrese su usuario")]
        public string usuario { get; set; }
        [Required(ErrorMessage = "El campo contraseña es requerido")]
        [Display(Name = "Ingrese su contraseña")]
        [RegularExpression(@"((?=.*\d)(?=.*[a-zA-Z]).{8,50})", ErrorMessage = "Ingrese solo letras y numeros, minimo de 8 caracteres")]
        public string contra { get; set; }

        [Required(ErrorMessage = "El campo cuit es requerido")]
        [Display(Name = "Numero de su C.U.I.T")]
        [RegularExpression("[0-9]{8}", ErrorMessage = "Solo ingrese numero y debe tener 8 caracteres")]
        public Nullable<int> cuit { get; set; }

        [Required(ErrorMessage = "El campo telefono es requerido")]
        [Display(Name = "Ingrese el telefono")]
        [RegularExpression("[0-9]{6,8}", ErrorMessage = "Solo ingrese numero y debe tener entre 6 y 8 caracteres")]
        public Nullable<int> telefono { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Capacitaciones> Capacitaciones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Encuestas> Encuestas { get; set; }
    }
}
