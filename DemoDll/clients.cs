//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DemoDll
{
    using System;
    using System.Collections.Generic;

    public partial class clients
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public clients()
        {
            this.service_client = new HashSet<service_client>();
        }

        public int id_clients { get; set; }
        public int id_gender { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public string secondname { get; set; }
        public System.DateTime birthday { get; set; }
        public System.DateTime registerday { get; set; }
        public string email { get; set; }
        public string phone { get; set; }

        private string FullName;
        public string fullname
        {
            get { return surname + " " + name + " " + secondname; }

        }

        public virtual genders genders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<service_client> service_client { get; set; }
    }
}
