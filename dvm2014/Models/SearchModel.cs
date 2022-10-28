using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dvm2014.Models
{
    public class SearchModel
    {
        public SearchModel()
        {
            Ucesnici = new List<Ucesnici>();
        }
        public IList<Ucesnici> Ucesnici;
        public int SumaUcestvo {
            get { 
                int pom=0;
                if (Ucesnici != null)
                {
                    foreach (var u in Ucesnici)
                    {
                        pom += u.Ucestvo;
                    }
                }
                return pom;
            }
        }

        public bool PrikaziUcestvo { get; set; }

    }
}