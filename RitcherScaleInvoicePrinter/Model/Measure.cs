using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RitcherScaleInvoicePrinter.Model
{
    public class Measure
    {
        // Informações de tempo e controle
        public DateTime EntryDate { get; set; }
        public TimeSpan EntryTime { get; set; }
        public DateTime ExitDate { get; set; }
        public TimeSpan ExitTime { get; set; }
        public string SlipNumber { get; set; }

        // Vehicle Details
        public string VehicleRegistration { get; set; }    // Registration
        public string VehicleNumber { get; set; }          // Number
        public string OperationType { get; set; }          // "Collect" or "Deliver"
        public double? DeliveryDistance { get; set; }      // Delivery Distance (Km)

        // Transporter Details
        public string TransporterName { get; set; }
        public string TransporterNumber { get; set; }
        public decimal? TariffKmTon { get; set; }

        // Driver Details
        public string DriverName { get; set; }
        public string DriverNumber { get; set; }

        // Customer Details
        public string CustomerName { get; set; }
        public string AccountNumber { get; set; }
        public string OrderNumber { get; set; }

        // Product Details
        public string ProductName { get; set; }
        public string ProductNumber { get; set; }
        public decimal? PricePerTon { get; set; }
        public decimal? DeliveryPricePerKm { get; set; }

        // Pesagens
        public int TareWeight { get; set; }        // Tare
        public int GrossWeight { get; set; }       // Gross
        public int NetWeight { get; set; }         // Nett
        public double? CurrentMass { get; set; }   // Current Mass (só para display, não salvo?)

        // Extras opcionais do CSV original
        public string MaterialCode { get; set; }           // Se necessário além de ProductNumber
        public string MaterialDescription { get; set; }    // Se diferente de ProductName
        public string DriverId { get; set; }
        public string DriverDocument { get; set; }
        public string CarrierId { get; set; }              // Se usado como identificador interno
        public string VehicleBrand { get; set; }
        public string LoadType { get; set; }               // Tipo de carga
        public string Notes { get; set; }
        public int VerifiedWeight { get; set; }
        public string AdditionalWeight1 { get; set; }
        public string AdditionalWeight2 { get; set; }
        public string LoadStatus { get; set; }
    }
}
