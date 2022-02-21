using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DemoDll
{

    public class ViewModel
    {
        public List<services> service;
        public List<clients> clients;

        public ViewModel()
        {
            service = discountN();
            clients = BaseConnect.BaseModel.clients.ToList();

        }

        public ViewModel(int identif)
        {
            service = discountN();
            clients = BaseConnect.BaseModel.clients.ToList();
            foreach (services s in service)
            {
                if (identif == 0)
                {
                    s.visiblebtn = "Hidden";
                }

                else
                {
                    s.visiblebtn = "Visible";
                }
            }

        }


        public List<service_client> createSC()
        {

            BaseConnect.BaseModel = new Entities();
            List<service_client> sc = BaseConnect.BaseModel.service_client.ToList();
            foreach (services s in service)
            {
                foreach (service_client sc2 in sc)
                {
                    if (sc2.id_service == s.id_service)
                    {
                        sc2.FullNameService = s.name_service;
                    }
                }
            }

            foreach (clients cl in clients)
            {
                foreach (service_client sc2 in sc)
                {
                    if (sc2.id_clients == cl.id_clients)
                    {
                        
                        TimeSpan ts = sc2.date - DateTime.Now;
                        if (sc2.date > DateTime.Now && (Math.Truncate(Convert.ToDouble(ts.TotalHours))<48-DateTime.Now.Hour))
                        {
                            sc2.TimeStart = Math.Truncate(Convert.ToDouble(ts.TotalHours)) +  " часов " + ts.Minutes + " минут ";
                            if(ts.Days==0 && ts.Hours<1)
                            {
                                sc2.colortime = "Red";
                            }
                       
                        }
                        sc2.FullNameClient = cl.fullname;
                        sc2.PhoneEmail = cl.phone + " " + cl.email;
                    }
                }
            }
            List<service_client> newSc = sc.Where(x => x.TimeStart != null).ToList();
            newSc = newSc.OrderBy(x => x.date).ToList();
            return newSc;
        }


        List<services> discountN()
        {
            List<services> service = BaseConnect.BaseModel.services.ToList();

            foreach (services s in service)
            {
                s.newcost = s.cost;
                if (s.discount > 0)
                {
                    s.Visible = "Visible";
                    s.VisibleD = "Visible";
                    s.Decor = "Strikethrough";
                    s.green = "LightGreen";
                    s.newcost = Convert.ToDecimal(Convert.ToDouble(s.cost) - Convert.ToDouble(s.cost) * s.discount);

                }
                else
                {
                    s.green = "none";
                    s.Visible = "Collapsed";
                    s.VisibleD = "Hidden";
                }

            }
           
            return service ;
        }
    }
}

