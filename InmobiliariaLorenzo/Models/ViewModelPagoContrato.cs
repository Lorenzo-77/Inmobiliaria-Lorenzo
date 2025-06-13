using System;
using InmobiliariaLorenzo.Models;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace InmobiliariaLorenzo.ViewModels;
public class ViewModelPagoContrato
 {

    public Pago pago { get; set; }

    public IEnumerable<Pago> lpago { get; set; }

 }

 

 