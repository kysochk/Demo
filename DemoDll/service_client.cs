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
    
    public partial class service_client
    {
        public int id { get; set; }
        public int id_service { get; set; }
        public System.DateTime date { get; set; }
        public int id_clients { get; set; }
    
        public virtual clients clients { get; set; }
        public virtual services services { get; set; }
    }
}
