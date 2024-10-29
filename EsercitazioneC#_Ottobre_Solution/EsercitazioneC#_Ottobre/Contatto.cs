using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EsercitazioneC__Ottobre
{
    public class Contatto
    {
        public string Nominativo;
        public string Telefono;

        private Contatto(string nominativo, string telefono)
        {
            Nominativo = nominativo;
            Telefono = telefono;
        }

        public static Contatto creaContatto(string nominativo, string telefono, List<Contatto> listContatti)
        {
            if (nominativo.Length > 0 && telefono.Length > 0)
            {
                // Verifico che l'oggetto non sia già contenuto nella lista.
                foreach (var contatto in listContatti)
                {
                    if (contatto.Nominativo == nominativo && contatto.Telefono == telefono)
                        return null;
                }
                return new Contatto(nominativo, telefono);
            }
            return null;
        }

        public override string ToString()
        {
            return $"{Nominativo} - {Telefono}";
        }
    }
}
